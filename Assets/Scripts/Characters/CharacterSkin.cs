using UnityEngine;

namespace Characters
{
    public class CharacterSkin : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _meshes;
        [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshes;

        public void SetColor(Color color)
        {
            foreach (var mesh in _meshes)
            {
                Material newMat = mesh.material;
                newMat.color = color;
            
                mesh.material = newMat;
            }

            foreach (var mesh in _skinnedMeshes)
            {
                Material newMat = mesh.material;
                newMat.color = color;
            
                mesh.material = newMat;
            }

        }
    }
}
