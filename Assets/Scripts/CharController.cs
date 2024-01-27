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
        _rigidbody = PlayerFigure.GetComponentInParent<Rigidbody>();
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

        if (Input.Laugh)
        {
            // Todo: Mit sinnvollerem Inhalt füllen
            Debug.Log("All your Base are belong to us!");
        }
        
        _cam.transform.position += _rigidbody.position - _oldPos;
        _oldPos = _rigidbody.position;
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

    private void Interact()
    {
        if(Input.Interact)
        {
            var colliders = Physics.OverlapSphere(PlayerFigure.position, InteractionRadius);

            Interactable? interactable = null;
            foreach (Collider collider in colliders)
                if (collider.CompareTag(GlobalDataSo.TAG_INTERACTABLE))
                    interactable = collider.gameObject.GetComponent<Interactable>();

            interactable?.Interact();
        }
    }
}
