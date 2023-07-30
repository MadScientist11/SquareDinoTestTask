using UnityEngine;

namespace Game.Source
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}