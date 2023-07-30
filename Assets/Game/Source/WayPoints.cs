using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Source
{
    public class WayPoints : MonoBehaviour, IEnumerable<Transform>
    {
        [SerializeField] private List<Transform> _wayPoints;
        
        public Transform this[int index] => _wayPoints[index];

        public IEnumerator<Transform> GetEnumerator()
        {
            return ((IEnumerable<Transform>)_wayPoints).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}