using System;
using UnityEngine;


namespace Game
{
    
    public abstract class MiniGames : MonoBehaviour
    {
        private event Action _onWinEvent;
        public event Action onWinEvent;
        public event Action onFailEvent;
        
        public virtual void Init()
        {
            
        }
        
        public virtual void Fail()
        {
            onFailEvent?.Invoke();
        }
        
        public virtual void Win()
        {
            onWinEvent?.Invoke();

        }

        private void OnWinEvent()
        {
            
        }
        
        private void OnFailEvent()
        {
            
        }

    }
}
