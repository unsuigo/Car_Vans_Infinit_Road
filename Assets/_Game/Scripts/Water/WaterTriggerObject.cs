using System.Linq;
using DG.Tweening;
using UnityEngine;

public class WaterTriggerObject : MonoBehaviour
{

   [SerializeField] private string[] _tags;
   private void OnTriggerEnter(Collider other)
   {
      if (_tags.Contains(other.tag))
      {
         DestroyObject(other.gameObject);
      }
   }

   void DestroyObject(GameObject o)
   {
      o.transform.DOKill();
      Destroy(o, 0.5f);
   }
}
