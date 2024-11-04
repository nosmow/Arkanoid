using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;

    Rigidbody rb;

    private GameObject obj;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Add a constant force to make the power up fall
        rb.linearVelocity = Vector3.down * powerUpData.fallSpeed; 
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
        obj = null;
        gameObject.SetActive(false);
    }

    #endregion

    // Activate the Mesh
    private void OnEnable()
    {
        SetVisibility(true);
    }

    private void OnDisable()
    {
        if (obj != null)
        {
            rb.linearVelocity = Vector3.zero;
            powerUpData.RemoveEffect(obj);
        }
    }

    // Check for collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            obj = other.gameObject;
            TriggerPowerUp(other.gameObject);
            SetVisibility(false);

            // Apply the sound effect
            AudioManager.Instance.PlayPowerUpHitSound();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            SetVisibility(false);
        }
    }
}
