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

    private int currentResistance;

    #region GET / SET
    
    public void SetBlockType(int type) { blockType = type; }

    public int GetBlockType() { return blockType; }

    #endregion

    private void Start()
    {
        if (isRock) return;

        currentResistance = resistance;

        effects = GetComponent<BlockVisualEffects>();
        scoreManager = GetComponent<BlockScore>();
    }

    #region Methods

    // Decreases resistance
    private void DecreaseResistance()
    {
        if (currentResistance > 1)
        {
            effects?.ChangeMaterialTemporarily();
        }

        currentResistance--;
    }

    // Method to destroy the block
    private void DestroyBlock()
    {
        // Only applies if it is not rock type
        if (currentResistance <= 0)
        {
            effects?.PlayDestroyEffects();
            scoreManager?.AddScore();
            GamePlayManager.Instance.SetDestroyedBlocks(gameObject.transform);
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

    // Restore Resistance
    private void OnEnable()
    {
        currentResistance = resistance;
    }
}
