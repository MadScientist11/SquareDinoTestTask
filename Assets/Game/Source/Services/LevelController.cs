using Game.Source.GameFSM;
using Game.Source.LevelLogic;
using Game.Source.PlayerLogic;

namespace Game.Source.Services
{
    public interface ILevelController
    {
        void InitializeLevel();
        void NextLocation();
        Location CurrentLocation { get; }
    }

    public class LevelController : ILevelController
    {
        public Location CurrentLocation { get; private set; }

        private int _currentLocationIndex = 0;
        
        private readonly Level _level;
        private readonly IGameFactory _gameFactory;
        private GameStateMachine _gameStateMachine;

        public LevelController(GameStateMachine gameStateMachine, IGameFactory gameFactory, Level level)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _level = level;
        }

        public void InitializeLevel()
        {
            CurrentLocation = _level.Locations[_currentLocationIndex];
            CreateEnemies();
        }

        public void NextLocation()
        {
            if (++_currentLocationIndex > _level.Locations.Count - 1)
            {
                _gameStateMachine.SwitchState(GameFlow.CompleteLevel);
                return;
            }
            
            CurrentLocation = _level.Locations[_currentLocationIndex];
            _gameFactory.Player.GetComponent<PlayerMovement>().SetDestination(CurrentLocation.LocationWayPoint.Position);
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