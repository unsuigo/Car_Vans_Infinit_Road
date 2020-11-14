using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    
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
    private bool _isPlaying = false;
    void Start()
    {
       
    }

    private void OnEnable()
    {
        CarGameManager.sessionWinAction += OnStopGame;
        CarGameManager.sessionStartAction += OnStartGame;
        CarGameManager.sessionFailAction += OnStopGame;
    }

    private void OnDisable()
    {
        CarGameManager.sessionWinAction -= OnStopGame;
        CarGameManager.sessionStartAction -= OnStartGame;
        CarGameManager.sessionFailAction -= OnStopGame;
    }

    void Update()
    {
        if (_player == null || !_isPlaying)
        {
            return;
        }
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


    private void SpawnVan()
    {
        var van = _poolVans[_nextIndex];
        van.SetActive(true);
        var spawnPosition = van.transform.position;
        var posZ = _playerPosZ + _zSpawn;
        spawnPosition.z = posZ;
        van.transform.position = spawnPosition;
        _activeVans.Add(van);
        
        _nextIndex++;
        if (_nextIndex >= _poolVans.Length-1)
        {
            _nextIndex = 0;
        }
    }
    
    private void DeleteLastVan()
    {
        var van = _activeVans[0];
        _activeVans.RemoveAt(0);
        van.SetActive(false);
        
        SpawnVan();
    }

    public void LastVanRichedCamera()
    {
        DeleteLastVan();
    }

    private void OnStartGame()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _playerPosZ = _player.position.z;
        _isPlaying = true;
        FillVansPool();
        SpawnVan();
    }
    private void OnStopGame()
    {
        _activeVans.Clear();
        foreach (var van in _poolVans)
        {
            Destroy(van);
        }

        _isPlaying = false;
    }
}
   
}