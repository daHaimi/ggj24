using UnityEditor.Animations;
using UnityEngine;

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

    public float MoveSpeed = 7f;
    public float JumpForce = 500f;
    public float InteractionRadius = 3;
    public Transform PlayerFigure;

    public AnimatorController idleAnimation;
    public AnimatorController runAnimation;

    private Camera _cam;
    private Vector3 _forward, _right, _oldPos;
    private bool _wasMovingBefore;
    private Rigidbody _rigidbody;
    private Animator _animator;
    
    void Start()
    {
        Input = GetComponent<InputController>();

        _cam = Camera.main;
        _forward = _cam.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
        _rigidbody = PlayerFigure.GetComponent<Rigidbody>();
        _animator = PlayerFigure.GetComponent<Animator>();
        _animator.runtimeAnimatorController = idleAnimation;
        _oldPos = PlayerFigure.position;
    }

    void Update()
    {
        Interact();
        if (Input.Moving && !_wasMovingBefore)
        {
            _animator.runtimeAnimatorController = runAnimation;
            _wasMovingBefore = true;
        } else if (!Input.Moving && _wasMovingBefore)
        {
            _animator.runtimeAnimatorController = idleAnimation;
            _wasMovingBefore = false;
        }

        _cam.transform.position += PlayerFigure.position - _oldPos;
        _oldPos = PlayerFigure.position;
    }

    void FixedUpdate()
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

        _rigidbody.AddForce((heading * MoveSpeed) - _rigidbody.velocity, ForceMode.VelocityChange);
    }

    private void Interact()
    {
        if(Input.Interact)
        {
            var colliders = Physics.OverlapSphere(transform.position, InteractionRadius);

            Interactable? interactable = null;
            foreach (Collider collider in colliders)
                if (collider.CompareTag(GlobalDataSo.TAG_INTERACTABLE))
                    interactable = collider.gameObject.GetComponent<Interactable>();

            interactable?.Interact();
        }
    }
}
