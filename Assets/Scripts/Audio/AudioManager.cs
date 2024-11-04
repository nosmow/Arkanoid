using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioData audioData;

    private void Start()
    {
       PlayBackgroundMusic(SceneManager.GetActiveScene().buildIndex);
    }

    #region Music Management

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic(scene.buildIndex);
    }

    // Change background music when changing scenes or levels
    public void PlayBackgroundMusic(int index)
    {
        if (audioData != null && index < audioData.backgroundMusic.Length)
        {
            musicSource.clip = audioData.backgroundMusic[index];
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    #endregion

    // Activate sound effects for the ball and power ups
    #region SFX Management

    public void PlayBallHitSound()
    {
        sfxSource.PlayOneShot(audioData.ballHitSound);
    }

    public void PlayBallLostSound()
    {
        sfxSource.PlayOneShot(audioData.ballLostSound);
    }

    public void PlayPowerUpHitSound()
    {
        sfxSource.PlayOneShot(audioData.powerUpHitSound);
    }

    public void PlayBlockDestroySound()
    { 
        sfxSource.PlayOneShot(audioData.blockDestroySound);
    }

    #endregion
}
