using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneValidator : MonoBehaviour
{ 
    [SerializeField] private int _mainMenuSceneIndex = 0;

    private void Awake()
    {
        var existingInstaller = FindObjectOfType<SceneValidator>();
        if (existingInstaller != null && existingInstaller != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != _mainMenuSceneIndex)
        {
            Debug.LogWarning($"Игра запущена не с {_mainMenuSceneIndex}. Перемещение на сцену меню.");
            SceneManager.LoadScene(_mainMenuSceneIndex);
        }
    }
}