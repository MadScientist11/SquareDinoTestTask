namespace Game.Source.Services
{
    public interface IDataProvider
    {
        EnemyConfiguration EnemyConfig { get; }
        
    }
    public class DataProvider : IDataProvider, IInitializableService
    {
        private readonly IAssetProvider _assetProvider;

        public EnemyConfiguration EnemyConfig { get; private set; }

        private const string EnemyConfigPath = "EnemyConfiguration";


        public DataProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            EnemyConfig = _assetProvider.LoadAsset<EnemyConfiguration>(EnemyConfigPath);
        }

        
    }
}