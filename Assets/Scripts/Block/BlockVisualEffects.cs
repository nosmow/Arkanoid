using System.Collections;
using UnityEngine;

public class BlockVisualEffects : MonoBehaviour
{
    [Header("Material Settings")]
    [SerializeField] private Material changeMaterial;
    [SerializeField] private float timeChangeMaterial = 0.2f;
    [SerializeField] private ParticleSystem particles;

    private new Renderer renderer;
    private Material baseMaterial;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        baseMaterial = renderer.material;
    }
    private void Start()
    {
       
    }

    public void ChangeMaterialTemporarily()
    {
        StartCoroutine(CoroutineChangeMaterial());
    }

    // Coroutine to change block material for a few seconds
    private IEnumerator CoroutineChangeMaterial()
    {
        renderer.material = changeMaterial;
        yield return new WaitForSeconds(timeChangeMaterial);
        renderer.material = baseMaterial;
    }

    public void PlayDestroyEffects()
    {
        if (particles != null)
        {
            particles.GetComponent<Renderer>().material = renderer.material;
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }
    private void OnEnable()
    {
        renderer.material = baseMaterial;

    }
}
