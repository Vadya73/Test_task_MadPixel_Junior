using UnityEngine;
using YG;

namespace ADv
{
    [DefaultExecutionOrder(-100)]
    public class AdvertisementInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var existingInstaller = FindObjectOfType<AdvertisementInstaller>();
            if (existingInstaller != null && existingInstaller != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            Advertisement advertisement = new Advertisement();
            ServiceLocator.RegisterService(advertisement);
        }
    }
}