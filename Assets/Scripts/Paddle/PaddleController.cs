using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        // Restores position when changing level
        FindAnyObjectByType<LevelManager>().OnChangedLevel += RestartPosition;
    }

    public void RestartPosition()
    {
        transform.position = startPosition;
    }

    public void IncreaseSizeX(float sizeX)
    {
        GetComponent<PaddleMovement>().ChangedLimits(sizeX);

        transform.localScale = new Vector3(transform.localScale.x + sizeX, transform.localScale.y, transform.localScale.z); 
    }
}
