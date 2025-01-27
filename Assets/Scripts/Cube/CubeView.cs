using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cube 
{
    public class CubeView : MonoBehaviour 
    {
        [SerializeField] private List<TextMeshProUGUI> cubeNum;
        [SerializeField] private CubeInfo cubeInfo;
        [SerializeField] private MeshRenderer meshRenderer;

        public void Init() 
        {
            if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetNewParam(int currIntOfArr) 
        {
            int currNum = (int)Mathf.Pow(2, currIntOfArr + 1);
            int i = 0;
            while (i < cubeNum.Count) {
                cubeNum[i].text = currNum.ToString();
                i++;
            }
            if (currIntOfArr > cubeInfo.colorsOfCube.Count - 1) {
                currIntOfArr = cubeInfo.colorsOfCube.Count - 1;
            }
            meshRenderer.material = cubeInfo.colorsOfCube[currIntOfArr];
        }
    }
}