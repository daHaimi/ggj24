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

    private Vector3 _forward, _right;
    private Rigidbody _rigidbody;

    void Start()
    {
        Input = GetComponent<InputController>();

        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Interact();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var rightMovement = _right * MoveSpeed * Time.deltaTime * Input.Horizontal;
        var upMovement = _forward * MoveSpeed * Time.deltaTime * Input.Vertical;
        var heading = Vector3.Normalize(rightMovement + upMovement);
        //transform.forward = heading;
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
