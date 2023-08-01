using System.Threading.Tasks;
using Game.Source.GameFSM;
using Game.Source.LevelSystem;
using Game.Source.PlayerLogic;
using Game.Source.Services.Factories;

namespace Game.Source.Services
{
    public interface ILevelController
    {
        void InitializeLevel();
        Task NextLocation();
        Location CurrentLocation { get; }
        void ScanCurrentLocation();
    }

    public class LevelController : ILevelController
    {
        public Location CurrentLocation { get; private set; }

        private int _currentLocationIndex = 0;
        
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
            CurrentLocation = _level.Locations[_currentLocationIndex];
            CreateEnemies();
        }

        public async Task NextLocation()
        {
            if (++_currentLocationIndex > _level.Locations.Count - 1)
            {
                await Task.Delay(2000);
                _gameStateMachine.SwitchState(GameFlow.CompleteLevel);
                return;
            }
            
            CurrentLocation = _level.Locations[_currentLocationIndex];
            _gameFactory.Player.GetComponent<PlayerMovement>().SetDestination(CurrentLocation.LocationWayPoint.Position);
        }

        public async void ScanCurrentLocation()
        {
            if (CurrentLocation.LocationEnemies.Count == 0) 
                await NextLocation();
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