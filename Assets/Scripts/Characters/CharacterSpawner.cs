using System.Collections;
using _Scripts.Level;
using Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private SpawnerByArea[] _spawners;
        [Space] 
        [SerializeField] private int _charactersCountOnStart = 10;
        [Space]
        [SerializeField] private bool _intervalSpawn = true;
        [SerializeField] private int _charactersSpawnInterval = 20;
        [SerializeField] private int _charactersSpawnCount = 2;

        private void Start()
        {
            for (int i = 0; i < _charactersCountOnStart; i++)
                SpawnCharacter();

            if (_intervalSpawn)
                LevelManager.Instance.OnStartGame += () =>  StartCoroutine(IntervalSpawnCharacter());
        }

        private void SpawnCharacter()
        {
            if (_spawners.Length == 0)
            {
                Debug.LogWarning("NO SPAWNERS !!!");
                return;
            }
        
            Vector3 randomPosition = _spawners[Random.Range(0, _spawners.Length)].GetRandomPosition();

            Instantiate(_characterPrefab, randomPosition, Quaternion.identity);
        }

        private IEnumerator IntervalSpawnCharacter()
        {
            while (true)
            {
                yield return new WaitForSeconds(_charactersSpawnInterval);

                for (int i = 0; i < _charactersSpawnCount; i++)
                    SpawnCharacter();
            }
        }
    }
}
