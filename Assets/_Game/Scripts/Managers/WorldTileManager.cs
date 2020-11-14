using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Utils;
using UnityEngine;

namespace Game
{
    
    public class WorldTileManager : SingletonT<WorldTileManager>
    {
        [SerializeField] Transform _cameraTransform;
        [SerializeField] GameObject[] _tilePrefabs;
        [SerializeField] float _zSpawn = 0;
        [SerializeField] float _tileLength = 80;
        [SerializeField] int _numberOfActiveTiles = 3;
        private List<GameObject> _activeTiles;

        private GameObject[] _poolTiles;
        private int _nextIndex = 0;

        private bool _isWorldExist;

        private void Start()
        {
            FillTilesPool();
        }

        void Update()
        {
            if (!_isWorldExist)
            {
                return;
            }
            if(_cameraTransform.position.z - _tileLength > _zSpawn -(_activeTiles.Count * _tileLength))
            {
                DeleteLastTile();
            }
            
        }

        private void FillTilesPool()
        {
            _poolTiles = new GameObject[_tilePrefabs.Length];
            
            for (int i = 0; i < _poolTiles.Length; i++)
            {
                GameObject go = Instantiate(_tilePrefabs[i], transform);
                go.SetActive(false);
                _poolTiles[i] = go;
            }
            Debug.Log("FillTilesPool " + _poolTiles.Length);

        }
        
        
        public void SpawnTile()
        {
            Debug.Log("SpawnTile " + _nextIndex);
            GameObject go = _poolTiles[_nextIndex];
            go.SetActive(true);
            var spawnPosition = transform.position;
            spawnPosition.z +=  _zSpawn;
            go.transform.position = spawnPosition;
            
            Debug.Log("spawnPosition " + spawnPosition);

            _activeTiles.Add(go);
            Debug.Log("activeTiles added " + _activeTiles.Count);
           
            for (int i = 0; i < _activeTiles.Count; i++)
            {
                Debug.Log("1-WTF " + _activeTiles[i].activeSelf);
                if (!_activeTiles[i].activeSelf)
                {
                    _activeTiles[i].SetActive(true);
                    Debug.Log("WTF " + _activeTiles[i].activeSelf);
            
                }
            }
            _zSpawn += _tileLength;
            _nextIndex++;
            if (_nextIndex > _poolTiles.Length-1)
            {
                _nextIndex = 0;
            }
        }

        private void DeleteLastTile()
        {
            // Destroy(activeTiles[0]);
            if (_activeTiles.Count > 0)
            {
                var tile = _activeTiles[0];
                _activeTiles.RemoveAt(0);
                tile.SetActive(false);
                Debug.Log("activeTiles removed at 0  activity is " + tile.activeSelf);
            }
            
            SpawnTile();
            if (_activeTiles.Count < _numberOfActiveTiles)
            {
                Debug.Log("spawn again WTF count" + _activeTiles.Count);

                SpawnTile();
            }
        }

        public void ResetWorld()
        {
            DeleteWorld();
            DOVirtual.DelayedCall(0.05f, BuilWorld);
        }

        private void DeleteWorld()
        {
            if (!_isWorldExist)
            {
                return;
            }
            
            _isWorldExist = false;
            Debug.Log("Destroy world !!!!!!!!!");

            if (_poolTiles.Length > 0)
            {
                foreach (var tile in _poolTiles)
                {
                    tile.SetActive(false);
                    tile.transform.position = Vector3.zero;
                }
            }
            
            _zSpawn = 0;
            _nextIndex = 0;
            
        }

        private void BuilWorld()
        {
            _activeTiles = new List<GameObject>();
            
            for (int i = 0; i < _numberOfActiveTiles; i++)
            {
                SpawnTile();
            }

            _isWorldExist = true;
        }
        
    }   
}
