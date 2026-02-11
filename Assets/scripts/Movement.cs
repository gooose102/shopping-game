using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    public float turnSpeed = 200f;
    public float boostedTurnMultiplier = 2f;
    public float brakeDeceleration = 40f;
    public float driftFactor = 0.9f; // 0 = full drift, 1 = no drift

    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = input * maxSpeed;

        if (Input.GetKey(KeyCode.Space))
        {
            // Decelerate when holding Space
            velocity = Vector2.MoveTowards(
                velocity,
                Vector2.zero,
                brakeDeceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            // Normal acceleration/deceleration
            velocity = Vector2.MoveTowards(
                velocity,
                targetVelocity,
                (input != Vector2.zero ? acceleration : deceleration) * Time.fixedDeltaTime
            );
        }

        // Drift: blend velocity direction with facing only if moving
        if (Input.GetKey(KeyCode.Space) && velocity.magnitude > 0.01f)
        {
            Vector2 forward = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad),
                                          Mathf.Sin(rb.rotation * Mathf.Deg2Rad));
            velocity = Vector2.Lerp(velocity, forward * velocity.magnitude, 1f - driftFactor);
        }

        rb.velocity = velocity;

        // Rotate based on input, independent of velocity
        if (input != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            float currentTurnSpeed = turnSpeed;

            if (Input.GetKey(KeyCode.Space))
                currentTurnSpeed *= boostedTurnMultiplier;

            rb.rotation = Mathf.MoveTowardsAngle(
                rb.rotation,
                targetAngle,
                currentTurnSpeed * Time.fixedDeltaTime
            );
        }
    }
}
