using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField] GameObject[] _managers;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        for (int i = 0; i < _managers.Length; i++)
        {
            Instantiate(_managers[i], transform);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
