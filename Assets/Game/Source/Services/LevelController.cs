using Game.Source.GameFSM;
using Game.Source.LevelSystem;
using Game.Source.PlayerLogic;
using Game.Source.Services.Factories;

namespace Game.Source.Services
{
    public interface ILevelController
    {
        void InitializeLevel();
        void NextLocation();
        Location CurrentLocation { get; }
        void ScanCurrentLocation();
        void OnNewLocationReached();
    }

    public class LevelController : ILevelController
    {
        public Location CurrentLocation { get; private set; }

        private int _currentLocationIndex = -1;
        
        private readonly Level _level;
        private readonly IGameFactory _gameFactory;
        private readonly GameStateMachine _gameStateMachine;

        public LevelController(GameStateMachine gameStateMachine, IGameFactory gameFactory, Level level)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _level = level;
        }

        public void InitializeLevel()
        {
            CreateEnemies();
        }

        public void OnNewLocationReached()
        {
            CurrentLocation = _level.Locations[_currentLocationIndex];
            ScanCurrentLocation();
        }

        public void NextLocation()
        {
            _currentLocationIndex++;
           if (_currentLocationIndex > _level.Locations.Count - 1)
           {
               _gameStateMachine.SwitchState(GameFlow.CompleteLevel);
               return;
           }
            
           MovePlayerToLocationWaypoint();
        }

        public void ScanCurrentLocation()
        {
            if (CurrentLocation.LocationEnemies.Count == 0) 
                NextLocation();
        }

        private void MovePlayerToLocationWaypoint()
        {
            _gameFactory.Player.GetComponent<PlayerMovement>()
                .SetDestination(_level.Locations[_currentLocationIndex].LocationWayPoint.Position);
        }

        private void CreateEnemies()
        {
            foreach (Location levelLocation in _level.Locations)
            {
                levelLocation.PopulateLocationWithEnemies();
            }
        }
    }
}