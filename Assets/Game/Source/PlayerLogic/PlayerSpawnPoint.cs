using System;
using UnityEditor;
using UnityEngine;

namespace Game.Source
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(Position, 0.05f);
        }
    }
}