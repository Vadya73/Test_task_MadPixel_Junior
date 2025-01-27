using System.Collections.Generic;
using ADv;
using Cube;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mangers.Menu 
{
    public class MenuManager : MonoBehaviour 
    {
        [SerializeField] private MenuUIManager menuUIManager;
        [SerializeField] private List<MenuCube> menuCubesList;
        
        [field: SerializeField] private int LevelSceneIndex = 1;

        private List<GameObject> collisionCube = new();

        private void Awake() 
        {
            Init();
            menuUIManager.Init();
        }
        
        private void Init()
        {
            for (var i = 0; i < menuCubesList.Count; i++) 
                menuCubesList[i].FoundManager(this);
        }
        private void Update() 
        {
            if (collisionCube.Count > 0)
            {
                MenuCube localCub = collisionCube[0].GetComponent<MenuCube>();
                localCub.currIntOfArr++;
                localCub.SetNewParam();
                int i = 1;
                do 
                {
                    collisionCube[i].gameObject.SetActive(false);
                    i++;
                }
                while (i < collisionCube.Count);
                collisionCube.Clear();
            }
        }

        public void CollisionCubeAdd(GameObject settableGameObject) => collisionCube.Add(settableGameObject);

        public void PlayGame()
        {
            ServiceLocator.GetService<Advertisement>().ShowInterstitialADv();
            ChangeScene(LevelSceneIndex);
        }

        private void ChangeScene(int levelIndex)
        {
            ServiceLocator.GetService<MusicManager>().SaveMusicState();
            SceneManager.LoadSceneAsync(levelIndex);
        }

        public void ChangeLanguage() => ServiceLocator.GetService<Localization.Localization>().SetNextLanguage();
    }
}