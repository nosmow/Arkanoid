using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Block : MonoBehaviour
{
    [Header("Block Settings")]
    [SerializeField] private int blockType;
    [SerializeField] private int resistance; 
    [SerializeField] private bool isRock;

    private BlockVisualEffects effects;
    private BlockScore scoreManager;

    #region GET / SET
    
    public void SetBlockType(int type) { blockType = type; }

    public int GetBlockType() { return blockType; }

    #endregion

    private void Start()
    {
        if (isRock) return;

        effects = GetComponent<BlockVisualEffects>();
        scoreManager = GetComponent<BlockScore>();
    }

    #region Methods

    // Decreases resistance
    private void DecreaseResistance()
    {
        if (resistance > 1)
        {
            effects?.ChangeMaterialTemporarily();
        }

        resistance--;
    }

    // Method to destroy the block
    private void DestroyBlock()
    {
        // Only applies if it is not rock type
        if (resistance <= 0)
        {
            effects?.PlayDestroyEffects();
            scoreManager?.AddScore();
            gameObject.SetActive(false);
        }
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !isRock)
        {
            DecreaseResistance();
            DestroyBlock();
        }
    }
}
