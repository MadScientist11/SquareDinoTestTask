using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Game.Source.PlayerLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private Vector3 _currentDestination;

        private WayPoints _wayPoints;

        [Inject]
        public void Construct(WayPoints wayPoints)
        {
            _wayPoints = wayPoints;
        }

        private void Start()
        {
            //Set Speed..
           SetDestination(_wayPoints[0].position);
        }

        public void SetDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
        }
    }
}
