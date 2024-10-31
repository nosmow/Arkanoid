using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private int resistance; 
    [SerializeField] private bool isRock;
    [SerializeField] private ParticleSystem particles;

    [Header("Material Settings")]
    [SerializeField] private Material changeMaterial;
   // [SerializeField] private float timeChangeMaterial;

    private new Renderer renderer;
    private Material baseMaterial;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        baseMaterial = renderer.material;
    }

    #region Methods

    // Coroutine to change block material for a few seconds
    private IEnumerator CoroutineChangeMaterial()
    {
        renderer.material = changeMaterial;
        yield return new WaitForSeconds(0.2f);
        renderer.material = baseMaterial;
    }

    // Decreases resistance
    private void DecreaseResistance()
    {
        if (resistance > 1)
        {
            StartCoroutine(CoroutineChangeMaterial());
        }

        resistance--;
    }

    // Method to destroy the block
    private void DestroyBlock()
    {
        // Only applies if it is not rock type
        if (resistance <= 0)
        {
            particles.GetComponent<Renderer>().material = renderer.material;
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!isRock)
            {
                DecreaseResistance();
                DestroyBlock();
            }
        }
    }
}
