using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class FollowPaddle : MonoBehaviour
{
    private Transform leader;

    private void Start()
    {
        leader = FindAnyObjectByType<PaddleController>().transform;
    }

    // Follows the paddle only in the horizontal position
    private void Update()
    {
        if (!GamePlayManager.Instance.isBallMoving)
        {
            transform.position = new Vector3(leader.position.x, transform.position.y, transform.position.z);
        }
    }
}
