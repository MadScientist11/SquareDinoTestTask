using UnityEngine;

namespace Game.Source.EnemyLogic
{
    public class EnemyDeath : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Hit");
        }
    }
}
