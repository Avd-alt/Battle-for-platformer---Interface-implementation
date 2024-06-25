using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField] private FirstAidKit _prefabFirstAidKit;
    [SerializeField] private Transform[] _spawnPointsFirstAidKit;
    [SerializeField] private Coin _prefabCoin;
    [SerializeField] private Transform[] _spawnPointsCoin;

    private void Start()
    {
        Spawn(_prefabCoin,_spawnPointsCoin);
        Spawn(_prefabFirstAidKit,_spawnPointsFirstAidKit);
    }

    private void Spawn<T>(T prefab, Transform[] points) where T : MonoBehaviour
    {
        foreach(var spawnPoint in points)
        {
            Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}