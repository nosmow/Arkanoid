using UnityEngine;

public class BallController : MonoBehaviour
{ 
    [Header("Damage Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float offsetSpawnY;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    #region Methods

    private void EnterTheLimit()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (player != null)
        {
            player.TakeDamage(damage);

            ResetValues(player.transform);
        }
    }

    public void ResetValues(Transform transform)
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.transform.parent = transform;
        rb.transform.position = new Vector3(transform.position.x, -offsetSpawnY, transform.position.z);
        GameManager.Instance.isMoving = false;

    }

    #endregion

    // Increases the speed of the ball every time it collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EnterTheLimit();
        }
    }
}