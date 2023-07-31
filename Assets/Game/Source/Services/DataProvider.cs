using Game.Source.EnemyLogic;
using Game.Source.PlayerLogic;

namespace Game.Source.Services
{
    public interface IDataProvider
    {
        EnemyConfiguration EnemyConfig { get; }
        PlayerConfiguration PlayerConfig { get; }
    }
    public class DataProvider : IDataProvider, IInitializableService
    {
        private readonly IAssetProvider _assetProvider;

        public EnemyConfiguration EnemyConfig { get; private set; }
        public PlayerConfiguration PlayerConfig { get; private set; }

        private const string EnemyConfigPath = "EnemyConfiguration";
        private const string PlayerConfigPath = "PlayerConfiguration";


        public DataProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            EnemyConfig = _assetProvider.LoadAsset<EnemyConfiguration>(EnemyConfigPath);
            PlayerConfig = _assetProvider.LoadAsset<PlayerConfiguration>(PlayerConfigPath);
        }

        
    }
}