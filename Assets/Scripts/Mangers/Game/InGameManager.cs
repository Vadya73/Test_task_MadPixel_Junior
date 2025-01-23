using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.CubeNS;
using Game.Audio;
namespace Game {
    public class InGameManager :MonoBehaviour {

        public InGameUIManager inGameUIManager;
        public AudioSwitcher audioSwitcher;
        [Header("MainCube")]
        [SerializeField] GameObject cubePrefab;
        [SerializeField] private Vector3 startPosition = new Vector3(0, 0.5f, 6.20f);
        
        public GameObject CubeGO {
            get { return cube; }
        }
        private GameObject cube;
        private Cube cubeCube;

        [SerializeField] AudioSource mergeAudio;
        public float TimeToChange {
            get { return timeBetweenChangeCube; }
        }
        [SerializeField] private float timeBetweenChangeCube = 1f;
        [Header("Start Generation Prefabs")]
        [SerializeField] List<Vector3> startPositionCubes;

        [Header("Boards")]
        [SerializeField] Vector3 boards;

        [HideInInspector] public List<GameObject> collisionCube;

        public int Score {
            set { 
                score += value; 
                if(score > GetScore()) {
                    PlayerPrefs.SetInt("Score", score);
                }
                inGameUIManager.inGameUi.SetScore(score, GetScore());
            }
            get { return score; }
        }
        private int score = 0;


        private int GetScore() { return PlayerPrefs.GetInt("cubeScore", 0); }
        public Vector3 Boards {
            get { return boards; }
        }
        private void Awake() {
            Init();
        }
        private void Init() {
            inGameUIManager.Init();
            NewCube();
            GenerateOtherCubs();

            inGameUIManager.inGameUi.SetScore(score, GetScore());
        }
        
        public void NewCube() {
            cube = InstantiateGameObjects(cubePrefab, startPosition);
            cubeCube = cube.GetComponent<Cube>();
            cubeCube.FoundManager(this);
            cubeCube.Init();
        }

        private void GenerateOtherCubs() {
            int i = 0;
            while (i < startPositionCubes.Count) {
                if (RandomBool()) {
                    GameObject gameObject = InstantiateGameObjects(cubePrefab, startPositionCubes[i]);
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    Cube localCube = gameObject.GetComponent<Cube>();
                    localCube.FoundManager(this);
                    localCube.Init();
                    localCube.IsPushed = true;
                }
                i++;
            }
        }

        private GameObject InstantiateGameObjects(GameObject gameObject, Vector3 vector) {
            return Instantiate(gameObject, vector, Quaternion.identity);
        }

        public void RechargeCubeCoroutine() => StartCoroutine(RechargeCube()); 

        private IEnumerator RechargeCube() {
            inGameUIManager.inputManager.Waintig = true;
            yield return new WaitForSeconds(timeBetweenChangeCube);
            NewCube();
            inGameUIManager.inputManager.Waintig = false;
        }
        private bool RandomBool() { return Random.value > 0.5f;}

        private void Update() {
            if(collisionCube.Count > 0) {
                Cube localCub = collisionCube[0].GetComponent<Cube>();
                localCub.currIntOfArr++;
                localCub.SetNewParam();
                Score = localCub.currNum;
                localCub.IsCollision = false;
                mergeAudio.Play();

                int i = 1;
                do {
                    collisionCube[i].gameObject.SetActive(false);
                    i++;
                }
                while (i < collisionCube.Count);
                collisionCube.Clear();
            }
        }


        public void GameOver() {
            isGameOver = true;
            inGameUIManager.GameOver();
        }

        public bool IsGameOver {
            get { return isGameOver; }
        }
        private bool isGameOver = false;
        public void RestartGame() => ChangeScene(0);
        public void BackToMenu() => ChangeScene(0);
        private void ChangeScene(int i) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + i);
    }
}