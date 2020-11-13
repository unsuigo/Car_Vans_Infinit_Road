using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    
    public class WorldTileManager : MonoBehaviour
    {
        [SerializeField] GameObject[] _tilePrefabs;
        [SerializeField] float _zSpawn = 0;
        [SerializeField] float _tileLength = 80;
        [SerializeField] int _numberOfTiles = 3;
        private List<GameObject> _activeTiles = new List<GameObject>();

        [SerializeField] Transform _cameraTransform;

        private GameObject[] _poolTiles;
        private int _nextIndex = 0;
        void Start()
        {

            FillTilesPool();
            for (int i = 0; i < _numberOfTiles; i++)
            {
               
                    SpawnTile(i);

                _nextIndex++;
            }
            
        }
        void Update()
        {
            if(_cameraTransform.position.z - _tileLength > _zSpawn -(_numberOfTiles * _tileLength))
            {
                if (_nextIndex > _poolTiles.Length-1)
                {
                    _nextIndex = 0;
                }
                // Debug.Log("Spawn");
                SpawnTile(_nextIndex);
                _nextIndex++;
                // Debug.Log("_nextIndex " + _nextIndex);

               
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
            
        }
        
        
        public void SpawnTile(int tileIndex)
        {
            GameObject go = _poolTiles[tileIndex];
            go.SetActive(true);
            // Debug.Log("SpawnTile " + tileIndex);

            var spawnPosition = transform.position;
            spawnPosition.z +=  _zSpawn;
            go.transform.position = spawnPosition;
            
            // GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
            _activeTiles.Add(go);
            // Debug.Log("activeTiles added " + activeTiles.Count);

            _zSpawn += _tileLength;

            foreach (var tile in _activeTiles)
            {
                if (!tile.activeSelf)
                {
                    tile.SetActive(true);
                    Debug.Log("WTF ");

                }
            }
        }

        private void DeleteLastTile()
        {
            // Destroy(activeTiles[0]);
            var tile = _activeTiles[0];
            _activeTiles.RemoveAt(0);
            // Debug.Log("activeTiles removed " + activeTiles.Count);

            tile.SetActive(false);
        }
        
    }   
}
