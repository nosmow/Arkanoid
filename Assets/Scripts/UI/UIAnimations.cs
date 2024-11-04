using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.enabled = true;
    }

    private void OnDisable()
    {
        animator.enabled = false;
    }

    private void Update()
    {
        if (GamePlayManager.Instance.isBallMoving)
        {
            gameObject.SetActive(false);
        }
    }
}
