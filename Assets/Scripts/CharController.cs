using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class CharController : MonoBehaviour
{
    #region singleton
    public static CharController Instance;

    public CharController()
    {
        Instance = this;
    }
    #endregion

    public InputController Input;
    public Laughing Laughing;

    public float MoveSpeed = 7f;
    public float JumpForce = 500f;
    public float InteractionRadius = 3;
    public Transform PlayerFigure;

    public Transform InteractionMarker;

    public AnimatorController idleAnimation;
    public AnimatorController runAnimation;

    private Camera _cam;
    private Vector3 _forward, _right, _oldPos;
    private bool _wasMovingBefore;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Laughing _laughing;

    private Vector3 _currentInteractionMarkerSpot;

    void Start()
    {
        Input = GetComponent<InputController>();
        Laughing = GetComponent<Laughing>();

        InteractionMarker.SetParent(null);

        _cam = Camera.main;
        _forward = _cam.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
        _rigidbody = PlayerFigure.GetComponentInParent<Rigidbody>();
        _animator = PlayerFigure.GetComponent<Animator>();
        _animator.runtimeAnimatorController = idleAnimation;
        _oldPos = PlayerFigure.position;
        _laughing = GetComponent<Laughing>();
    }

    void Update()
    {
        Interact();
        if (Input.Moving && !_wasMovingBefore)
        {
            _animator.runtimeAnimatorController = runAnimation;
            _wasMovingBefore = true;
        }
        else if (!Input.Moving && _wasMovingBefore)
        {
            _animator.runtimeAnimatorController = idleAnimation;
            _wasMovingBefore = false;
        }

        if (Input.Laugh)
        {
            _laughing.StartLaughing();
        }
        else
        {
            _laughing.StopLaughing();
        }

        _cam.transform.position += _rigidbody.position - _oldPos;
        _oldPos = _rigidbody.position;

        // visual effect for interaction marker
        InteractionMarker.Rotate(Vector3.one * Time.deltaTime * 10);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var rightMovement = _right * (MoveSpeed * Time.deltaTime * Input.Horizontal);
        var upMovement = _forward * (MoveSpeed * Time.deltaTime * Input.Vertical);
        var heading = Vector3.Normalize(rightMovement + upMovement);
        if (heading.magnitude > .001)
        {
            PlayerFigure.rotation = Quaternion.LookRotation(heading);
        }
        var v = (heading * MoveSpeed) - _rigidbody.velocity;
        v.y = 0;
        _rigidbody.AddForce(v, ForceMode.VelocityChange);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Interact()
    {
        var colliders = Physics.OverlapSphere(PlayerFigure.position, InteractionRadius)
            .OrderBy(x => Vector3.Distance(x.transform.position, PlayerFigure.position));
        GameObject? colliderGo = null;
        foreach (Collider collider in colliders)
            if (collider.CompareTag(GlobalDataSo.TAG_INTERACTABLE))
                colliderGo = collider.gameObject;

        UpdateInteractionMarker(colliderGo?.transform.position ?? null);

        if (Input.Interact)
        {
            Interactable? interactable = null;
            interactable = colliderGo?.gameObject.GetComponent<Interactable>();
            interactable?.Interact();
        }
    }

    private void UpdateInteractionMarker(Vector3? position)
    {

        if (position == null)
        {
            InteractionMarker.gameObject.LeanCancel();
            InteractionMarker.gameObject.LeanScale(Vector3.zero, 0.25f);
            InteractionMarker.gameObject.LeanMove(PlayerFigure.transform.position + new Vector3(0, 10, 0), 0.25f);
        }
        else if (position != _currentInteractionMarkerSpot)
        {
            InteractionMarker.gameObject.LeanCancel();
            InteractionMarker.gameObject.LeanScale(Vector3.one * 0.5f, 0.25f);
            InteractionMarker.gameObject.LeanMove(position.Value + new Vector3(0, 1, 0), 0.25f)
                .setEaseOutSine();
        }
    }
}
