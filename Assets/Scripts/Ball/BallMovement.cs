using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float minAxisOffsetDegrees = 10f;
    [SerializeField] private float ballSpeed = 10f;
    [SerializeField] private float ballSpeedIncrease = 0.05f;

    [Header("Start Direction")]
    [SerializeField] private float xStartDir = 4f;
    [SerializeField] private float yStartDir = 6f;

    [Header("Random Adjustment Range")]
    [SerializeField] private float minAdjustment = 1f;
    [SerializeField] private float maxAdjustment = 5f;

    private Rigidbody rb;
    private float minOffset;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isMoving)
        {
            StartMoving();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isMoving)
        {
            MaintainConstantSpeed();
        }
    }

    #region Methods

    // Method for starting the ball movement
    private void StartMoving()
    {
        rb.isKinematic = false;

        // Set the initial speed of the ball
        rb.linearVelocity = new Vector3(xStartDir, yStartDir, 0).normalized * ballSpeed;

        // Calculate the minimum allowable offset using the minimum deflection angle in radians
        minOffset = Mathf.Sin(minAxisOffsetDegrees * Mathf.Deg2Rad) * ballSpeed;

        gameObject.transform.parent = null;

        GameManager.Instance.isMoving = true;
    }

    // Ensures that the ball speed is always equal to ballSpeed
    private void MaintainConstantSpeed()
    {
        if (rb.linearVelocity.magnitude < ballSpeed || rb.linearVelocity.magnitude > ballSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * ballSpeed;
        }
    }

    // Method to adjust the ball speed if it drops below the minimum on any axis
    private void TweakVelocityIfNeeded()
    {
        Vector3 velocity = rb.linearVelocity;
        bool pathAltered = false;

        // Check and adjust the speed on the X and Y axis
        velocity.x = CheckAndAdjustAxis(velocity.x, ref pathAltered);
        velocity.y = CheckAndAdjustAxis(velocity.y, ref pathAltered);

        // If the address has changed, normalize and apply the new speed
        if (pathAltered)
        {
            rb.linearVelocity = velocity.normalized * ballSpeed;
        }
    }

    // Method to check and adjust an axis if it is less than the minimum allowed
    private float CheckAndAdjustAxis(float axisValue, ref bool pathAltered)
    {
        if (Mathf.Abs(axisValue) < minOffset)
        {
            // Generate a random adjustment and change the axis value
            float randomAdjustment = Random.Range(minAdjustment, maxAdjustment) * Mathf.Sign(Random.Range(-1f, 1f));
            pathAltered = true; // Indicates that the direction of the ball has changed
            return axisValue + randomAdjustment;
        }

        return axisValue;
    }

    #endregion

    // Increases the speed of the ball every time it collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        ballSpeed += ballSpeedIncrease;

        if (GameManager.Instance.isMoving)
        {
            TweakVelocityIfNeeded();
        }
    }
}
