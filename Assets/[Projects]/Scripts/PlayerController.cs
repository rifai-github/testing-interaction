using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private Animator animator;

    private float smoothTransition;

    private Vector3 initialPosition;
    Rigidbody rb;

    private void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetPosition()
    {
        rb.MovePosition(initialPosition);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movement = new(InputManager.GetMovement().x, 0, InputManager.GetMovement().y);
        smoothTransition = Mathf.Lerp(smoothTransition, movement.magnitude, Time.fixedDeltaTime * moveSpeed);
        if (smoothTransition < 0.0005f)
            smoothTransition = 0.0005f;

        if (animator != null)
            animator.SetFloat("movement", smoothTransition);

        if (movement != Vector3.zero)
        {
            rb.MovePosition(Vector3.Lerp(transform.position, transform.position + movement, Time.fixedDeltaTime * moveSpeed));
            transform.forward = Vector3.Slerp(transform.forward, movement, Time.fixedDeltaTime * moveSpeed);
        }
    }
}
