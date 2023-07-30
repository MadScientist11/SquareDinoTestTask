using Game.Source.PlayerLogic;
using Game.Source.Services;
using UnityEngine;
using VContainer;

namespace Game.Source.EnemyLogic
{
    public interface IDamageable
    {
        void TakeDamage(IDamageProvider damageProvider);
    }

    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        private int _currentHealth;

        private EnemyConfiguration _enemyConfiguration;

        [Inject]
        public void Construct(IDataProvider dataProvider)
        {
            _enemyConfiguration = dataProvider.EnemyConfig;
        }

        private void Start()
        {
            _currentHealth = _enemyConfiguration.MaxHealth;
        }

        public void TakeDamage(IDamageProvider damageProvider)
        {
            _currentHealth -= damageProvider.ProvideDamage();
        }
    }
}
