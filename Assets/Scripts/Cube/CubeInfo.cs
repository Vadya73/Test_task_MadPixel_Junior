using System.Collections.Generic;
using UnityEngine;

namespace Cube 
{
    [CreateAssetMenu(menuName = "Cube state")]
    public class CubeInfo : ScriptableObject 
    {
        public List<Material> colorsOfCube;
    }
}