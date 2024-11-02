using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public void IncreaseSizeX(float sizeX)
    {
        transform.localScale = new Vector3(transform.localScale.x + sizeX, transform.localScale.y, transform.localScale.z); 
    }
}
