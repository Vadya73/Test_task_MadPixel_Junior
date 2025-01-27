using UnityEngine;

namespace Localization
{
    [DefaultExecutionOrder(-100)]
    public class LocalizationInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var existingInstaller = FindObjectOfType<LocalizationInstaller>();
            if (existingInstaller != null && existingInstaller != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            Localization localization = new Localization();
            ServiceLocator.RegisterService(localization);
        }
    }
}