using Freya;
using UnityEngine;

namespace Game.Source.LevelLogic
{
    public class LevelBoundingBox : MonoBehaviour
    {
        [SerializeField] private Vector3 _scale;
        private Box3D _boundingBox;

        private void Start()
        {
            _boundingBox = new Box3D()
            {
                center = transform.position,
                extents = _scale * 0.5f
            };
        }

        public bool Contains(Vector3 point)
        {
            return _boundingBox.Contains(point);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, _scale);
        }
    }
}
