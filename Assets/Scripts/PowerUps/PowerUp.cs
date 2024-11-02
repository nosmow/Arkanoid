using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Add a constant force to make the power up fall
        rb.AddForce(Vector3.down * powerUpData.fallSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange); 
    }

    #region Methods

    // Activate the power up
    private void TriggerPowerUp(GameObject target)
    {
        powerUpData.ApplyEffect(target);
        StartCoroutine(RemovePowerUpAfterDuration(target));
    }

    // Turns the object render on or off
    private void SetVisibility(bool isVisible)
    {
        GetComponent<Renderer>().enabled = isVisible;
    }

    #endregion

    #region Coroutines

    // Turn off the power up after a while
    private IEnumerator RemovePowerUpAfterDuration(GameObject target)
    {
        SetVisibility(false);

        yield return new WaitForSeconds(powerUpData.durationTime);
        
        powerUpData.RemoveEffect(target);
        SetVisibility(true);
        gameObject.SetActive(false);
    }

    #endregion

    // Activate the Mesh
    private void OnEnable()
    {
        SetVisibility(true);
    }

    // Check for collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerPowerUp(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            SetVisibility(false);
        }
    }
}
