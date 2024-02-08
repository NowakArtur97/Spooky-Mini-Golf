using UnityEngine;

public class InLineSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private int _numberOfPrefabsToSpawn = 50;
    [SerializeField] private Vector2 _spawnDirection = Vector2.right;
    [SerializeField] private float _maxHorizontalOffset = 1.0f;
    [SerializeField] private float _maxVecrticalOffset = 1.0f;

    private void Start()
    {
        for (int i = 0; i < _numberOfPrefabsToSpawn; i++)
        {
            Vector2 position = GetPosition(i);
            GameObject spawnedGameObject = SpawnPrefab(position);
            spawnedGameObject.transform.parent = gameObject.transform;
        }
    }

    private GameObject SpawnPrefab(Vector2 position) => Instantiate(_prefabToSpawn, position, Quaternion.identity);

    private Vector2 GetPosition(int index)
    {
        Vector2 offset = new Vector2(_maxHorizontalOffset * index, index % 2 == 0 ? _maxVecrticalOffset : 0);
        return _startPosition + offset;
    }
}
