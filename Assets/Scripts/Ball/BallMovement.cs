using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Vector3 initialVelocity;
    [SerializeField] private float velocityMultiplayer;
    [SerializeField] private float velocityDelta;
    [SerializeField] private float minVelocity;

    private Rigidbody rb;
    private bool isBallMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBallMoving)
        {
            Launch();
        }
    }

    private void Launch()
    {
        transform.parent = null;
        rb.linearVelocity = initialVelocity;
        isBallMoving = true;
    }

    private void VelocityFix()
    {
        

        if (Mathf.Abs(rb.linearVelocity.x) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            rb.linearVelocity += new Vector3(velocityDelta, 0f);
        }

        if (Mathf.Abs(rb.linearVelocity.y) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            rb.linearVelocity += new Vector3(0f, velocityDelta);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            rb.linearVelocity *= velocityMultiplayer;
        }

        VelocityFix();
    }
}
