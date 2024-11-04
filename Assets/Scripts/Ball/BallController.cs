using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{ 
    [SerializeField] private int damage = 1;

    private int currentDamage;

    private Rigidbody rb;

    private float startPositionY;

    #region GET / SET
    
    public void SetDamage(int newDamage)
    {
        if (currentDamage <= damage)
        {
            currentDamage = newDamage;
        }
        else
        {
            currentDamage = damage;
        }
    }

    public int GetDamage() { return currentDamage; }
    
    #endregion

    private void Start()
    {
        currentDamage = damage;

        rb = GetComponent<Rigidbody>();
        startPositionY = transform.position.y;
        FindAnyObjectByType<LevelManager>().OnChangedLevel += ResetValues;
        GamePlayManager.Instance.OnBlocksAreGone += ResetValues;
    }

    #region Methods

    private void EnterTheLimit()
    {
        if (GamePlayManager.Instance != null)
        {
            GamePlayManager.Instance.TakeDamage(damage);

            ResetValues();
        }
    }

    public void ResetValues()
    {
       Transform paddle = GameObject.FindWithTag("Paddle").transform;


        if (rb != null)
        {

            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            transform.position = new Vector3(paddle.position.x, startPositionY, paddle.position.z);
            StartCoroutine(WaitForStart());
        }
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(0.01f);
        rb.isKinematic = true;
        GamePlayManager.Instance.isBallMoving = false;
    }

    public void IncreaseSize(float size)
    {
        transform.localScale = new Vector3(transform.localScale.x + size, transform.localScale.y + size, transform.localScale.z + size);
    }

    #endregion

    // Increases the speed of the ball every time it collides with an object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            EnterTheLimit();

            // Apply the sound effect
            AudioManager.Instance.PlayBallLostSound();
        }
        else
        {
            // Apply the sound effect
            AudioManager.Instance.PlayBallHitSound();
        }
    }
}