using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanPoolCreator : MonoBehaviour
{
    [SerializeField] GameObject[] _vanPrefabs;
    [SerializeField] float _zSpawn = 0;
    [SerializeField] int _numberOfVans = 1;
    
    private List<GameObject> _activeVans = new List<GameObject>();
    private Transform _player;
    private GameObject[] _poolVans;
    private float _playerPosZ;
    private int _nextIndex = 0;
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _playerPosZ = _player.position.z;

        FillVansPool();
        SpawnVan(0);
    }

    void Update()
    {
        _playerPosZ = _player.position.z;
    }
    
    private void FillVansPool()
    {
        _poolVans = new GameObject[_vanPrefabs.Length];
            
        for (int i = 0; i < _poolVans.Length; i++)
        {
            GameObject go = Instantiate(_vanPrefabs[i], transform);
            go.SetActive(false);
            _poolVans[i] = go;
        }
    }

    private void ControllPlayerPos(Transform van)
    {
        var vanPosZ = van.position.z;
        if (vanPosZ < _playerPosZ - 10f)
        {
            DeleteLastVan();
        }
    }


    private void SpawnVan(int poolIndex)
    {
        var van = _poolVans[poolIndex];
        van.SetActive(true);
        var spawnPosition = van.transform.position;
        var posZ = _playerPosZ + _zSpawn;
        spawnPosition.z = posZ;
        van.transform.position = spawnPosition;
        _activeVans.Add(van);
        _nextIndex++;
        if (poolIndex >= _poolVans.Length-1)
        {
            _nextIndex = 0;
        }
    }
    
    private void DeleteLastVan()
    {
        var van = _activeVans[0];
        _activeVans.RemoveAt(0);
        van.SetActive(false);
        
        SpawnVan(_nextIndex);
    }

    public void LastVanRichedCamera()
    {
        DeleteLastVan();
        Debug.Log("LastVanRichedCamera " );
        
    }
}
