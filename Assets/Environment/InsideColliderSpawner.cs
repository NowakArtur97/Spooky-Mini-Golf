using UnityEngine;

public class InsideColliderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private int _numberOfPrefabsToSpawn = 50;
    [SerializeField] private float _topOffset = 2.0f;
    [SerializeField] private float _bottomOffset = 2.0f;
    [SerializeField] private float _rightOffset = 2.0f;
    [SerializeField] private float _leftOffset = 2.0f;

    private BoxCollider2D _myBoxCollider2D;

    private Vector2 _cubeSize;
    private Vector2 _cubeCenter;

    private void Awake()
    {
        _myBoxCollider2D = GetComponent<BoxCollider2D>();

        Transform cubeTrans = _myBoxCollider2D.GetComponent<Transform>();
        _cubeCenter = cubeTrans.position;

        _cubeSize.x = _myBoxCollider2D.size.x;
        _cubeSize.y = _myBoxCollider2D.size.y;
    }


    private void Start()
    {
        for (int i = 0; i < _numberOfPrefabsToSpawn; i++)
        {
            GameObject spawnedGameObject = SpawnPrefab();
            spawnedGameObject.transform.parent = gameObject.transform;
        }
    }

    private GameObject SpawnPrefab() => Instantiate(_prefabToSpawn, GetRandomPosition(), Quaternion.identity);

    private Vector2 GetRandomPosition()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-_cubeSize.x + _leftOffset, _cubeSize.x + _rightOffset),
            Random.Range(-_cubeSize.y + _bottomOffset, _cubeSize.y + _topOffset));

        return _cubeCenter + randomPosition;
    }
}
