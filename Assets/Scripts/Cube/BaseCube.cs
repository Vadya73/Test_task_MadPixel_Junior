using UnityEngine;

namespace Cube 
{
    public class BaseCube : MonoBehaviour 
    {
        protected new Rigidbody rigidbody;
        [SerializeField] protected CubeView cubeView;

        public int currNum;
        public int currIntOfArr;

        public int maxRandomStartInt = 2;
        [Space] [Header("Move")]
        private float horizontalSpeed = 1300;
        private float verticalSpeed = 300;
        [Header("Music")]
        [SerializeField] private AudioSource dropAudio;
        
        public bool IsCollision { set => isCollision = value; }
        private bool isCollision = false;
        public Vector3 Position => transform.position;
        private void Awake() 
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        public virtual void Init() 
        {
            cubeView.Init();

            GenerateNum();
            SetNewParam();
        }


        protected void GenerateNum()
        {
            currIntOfArr = Random.Range(0, maxRandomStartInt);
        }

        public void SetNewParam() 
        {
            cubeView.SetNewParam(currIntOfArr);
            currNum = (int)Mathf.Pow(2, currIntOfArr + 1);
        }

        public virtual void MoveForward() 
        {
            Move(Vector3.forward, horizontalSpeed);
            dropAudio.Play();
        }

        public void MoveToSide(Vector3 vector)
        {
            rigidbody.velocity = vector;
        }

        protected void MoveUp()
        {
            Move(Vector3.up, verticalSpeed);
        }

        protected void Move(Vector3 vector, float speed)
        {
            rigidbody.AddForce(vector * speed);
        }
        
        protected virtual void OnCollisionEnter(Collision collision) 
        {
            if (collision.gameObject.TryGetComponent<Cube>(out Cube otherCube)) 
            {
                if (otherCube.currIntOfArr == currIntOfArr && !isCollision) 
                {
                    if (isCollision) return;
                    isCollision = true;
                    rigidbody.constraints = RigidbodyConstraints.None;
                    SetValueToManagerList();
                    MoveUp();
                }
            }
        }

        protected virtual void SetValueToManagerList() { }
    }
}