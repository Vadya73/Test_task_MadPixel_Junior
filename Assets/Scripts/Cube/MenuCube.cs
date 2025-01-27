using Mangers.Menu;
using UnityEngine;

namespace Cube 
{
    public class MenuCube : BaseCube 
    {
        [SerializeField] MenuManager menuManager;
        protected override void SetValueToManagerList() 
        {
            menuManager.CollisionCubeAdd(this.gameObject);
        }

        public void FoundManager(MenuManager menu) 
        {
            menuManager = menu;
        }

        public override void Init() 
        {
            base.Init();
        }
        private void Start() 
        {
            Init();
        }

        protected override void OnCollisionEnter(Collision collision) 
        {
            if (collision.gameObject.TryGetComponent<MenuCube>(out MenuCube otherCube)) 
            {
                if (otherCube.currIntOfArr == currIntOfArr) 
                {
                    rigidbody.constraints = RigidbodyConstraints.None;
                    SetValueToManagerList();
                    MoveUp();
                }
            }
        }
    }
}