using UnityEngine;

namespace Game.Source.LevelLogic
{
    public class WayPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(Position, 0.1f);
        }
    }
}