using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float fallMultiplier = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isJumped;

    public bool isGameOver = false;

    [SerializeField] private Animator animator;

    private int currentLane = 1;
    private float[] lanes = { -2f, 0f, 2f };
    public float laneChangeSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isGameOver) return;

        if (isGrounded) animator.SetBool("Run", true);

        HandleJump();
        HandleLaneChange();
    }

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.velocity += Vector3.down * fallMultiplier * Time.fixedDeltaTime;
        }

        Vector3 targetPosition = new Vector3(lanes[currentLane], transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("Run", false);
            animator.SetTrigger("Jump");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void HandleLaneChange()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane > 0) currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane < 2) currentLane++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            animator.SetBool("Run", false);
            animator.SetBool("Death", true);

            isGameOver = true;
        }
    }
}
