using UnityEngine;

namespace Game.Source.EnemyLogic
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public void Enable()
        {
            foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
            {
                rb.velocity = rb.angularVelocity = Vector3.zero;
                rb.isKinematic = false;
            }

            if (_animator != null)
                _animator.enabled = false;
        }

        public void Disable()
        {
            foreach (Rigidbody rb in transform.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = true;
            }
        }
    }
}