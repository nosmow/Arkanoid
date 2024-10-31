using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            particles.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
