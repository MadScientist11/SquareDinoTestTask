using UnityEngine;

namespace Game.Source.DamageSystem
{
    public class CritChance : IDamageProvider
    {
        private readonly int _critDamage;
        private IDamageProvider _damageProvider;

        public CritChance(IDamageProvider damageProvider, int critDamage)
        {
            _damageProvider = damageProvider;
            _critDamage = critDamage;
        }

        public int ProvideDamage()
        {
            return _damageProvider.ProvideDamage() + 
                Random.value > 0.5f ? _critDamage : 0;
        }
    }
}