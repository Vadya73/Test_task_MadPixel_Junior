using System.Collections;
using System.Collections.Generic;
using ADv;
using Audio;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Mangers.Game
{
    public class InGameManager : MonoBehaviour
    {
        public InGameUIManager inGameUIManager;

        [Header("MainCube")]
        [SerializeField] GameObject cubePrefab;
        [SerializeField] private Vector3 startPosition = new(0, 0.5f, 6.20f);

        public GameObject CubeGO => cube;
        private GameObject cube;
        private Cube.Cube cubeCube;

        [SerializeField] AudioSource mergeAudio;
        public float TimeToChange => timeBetweenChangeCube;
        [SerializeField] private float timeBetweenChangeCube = 1f;

        [Header("Start Generation Prefabs")]
        [SerializeField] List<Vector3> startPositionCubes;

        [Header("Boards")]
        [SerializeField] Vector3 boards;

        [HideInInspector] public List<GameObject> collisionCube;

        public int Score
        {
            set
            {
                score += value;

                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt("HighScore", highScore);
                    YG2.saves.highScore = this.highScore;
                }

                inGameUIManager.inGameUi.SetScore(score, highScore);
            }
            get => score;
        }
        private int score = 0;

        private int highScore;

        public Vector3 Boards => boards;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            this.highScore = YG2.saves.highScore;

            highScore = PlayerPrefs.GetInt("HighScore", 0);

            inGameUIManager.Init();
            NewCube();
            GenerateOtherCubs();

            inGameUIManager.inGameUi.SetScore(score, highScore);
        }

        public void NewCube()
        {
            cube = InstantiateGameObjects(cubePrefab, startPosition);
            cubeCube = cube.GetComponent<Cube.Cube>();
            cubeCube.FoundManager(this);
            cubeCube.Init();
        }

        private void GenerateOtherCubs()
        {
            int i = 0;
            while (i < startPositionCubes.Count)
            {
                if (RandomBool())
                {
                    GameObject gameObject = InstantiateGameObjects(cubePrefab, startPositionCubes[i]);
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    Cube.Cube localCube = gameObject.GetComponent<Cube.Cube>();

                    localCube.FoundManager(this);
                    localCube.Init();
                    localCube.IsPushed = true;
                }
                i++;
            }
        }

        private GameObject InstantiateGameObjects(GameObject gameObject, Vector3 vector)
        {
            return Instantiate(gameObject, vector, Quaternion.identity);
        }

        public void RechargeCubeCoroutine() => StartCoroutine(RechargeCube());

        private IEnumerator RechargeCube()
        {
            inGameUIManager.inputManager.Waiting = true;
            yield return new WaitForSeconds(timeBetweenChangeCube);
            NewCube();
            inGameUIManager.inputManager.Waiting = false;
        }

        private bool RandomBool() => Random.value > 0.5f;

        private void Update()
        {
            if (collisionCube.Count > 0)
            {
                Cube.Cube localCub = collisionCube[0].GetComponent<Cube.Cube>();
                localCub.currIntOfArr++;
                localCub.SetNewParam();
                Score = localCub.currNum;
                localCub.IsCollision = false;
                mergeAudio.Play();

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

        public void GameOver()
        {
            isGameOver = true;
            inGameUIManager.GameOver();
        }

        public bool IsGameOver => isGameOver;
        private bool isGameOver = false;

        public void RestartGame() => ChangeScene(SceneManager.GetActiveScene().buildIndex);

        public void BackToMenu()
        {
            ServiceLocator.GetService<Advertisement>().ShowInterstitialADv();
            ChangeScene(0);
        }

        private void ChangeScene(int i) => SceneManager.LoadScene(i);

        [ContextMenu("reset high score")]
        private void ResetHighScore()
        {
            highScore = 0;
            YG2.saves.highScore = 0;
            inGameUIManager.inGameUi.SetScore(score, 0);
        }
    }
}
