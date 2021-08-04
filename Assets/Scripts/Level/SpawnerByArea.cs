using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Level
{
    public class SpawnerByArea : MonoBehaviour
    {
        [SerializeField] private Vector2 _area = Vector2.one;
        [SerializeField] private bool _showGizmos = true;

        public Vector3 GetRandomPosition()
        {
            Vector3 randomOffset = new Vector3(Random.Range(-_area.x, _area.x),0, Random.Range(-_area.y, _area.y)) / 2;

            return transform.position + randomOffset;
        }
        
        private void OnDrawGizmos()
        {
            if (!_showGizmos)
                return;
            
            Gizmos.color = Color.blue;
            
            Vector3 cubeSize = new Vector3(_area.x, 1, _area.y);
            Gizmos.DrawWireCube(transform.position, cubeSize);
        }
    }
}
