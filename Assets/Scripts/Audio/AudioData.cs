using UnityEngine;

// ScriptableObject to store the level audio
[CreateAssetMenu(fileName = "New AudioData", menuName = "Audio/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    public AudioClip[] backgroundMusic;

    public AudioClip ballHitSound;
    public AudioClip ballLostSound;
    public AudioClip powerUpHitSound;
    public AudioClip blockDestroySound;
}
