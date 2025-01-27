using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))] [DefaultExecutionOrder(-100)]
    public class MusicManagerInstaller : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            var existingInstaller = FindObjectOfType<MusicManagerInstaller>();
            if (existingInstaller != null && existingInstaller != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        
            if (audioSource == null)
            {
                Debug.LogError("AudioSource не найден на объекте MusicManagerInitializer!");
                return;
            }

            MusicManager musicManager = new MusicManager(audioSource);
            ServiceLocator.RegisterService(musicManager);

            musicManager.LoadMusicState();
        }

        private void OnApplicationQuit()
        {
            ServiceLocator.GetService<MusicManager>().SaveMusicState();
        }
    }
}