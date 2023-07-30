using UnityEngine;

namespace Game.Source.Services
{
    [CreateAssetMenu(menuName = "EnemyConfiguration", fileName = "Game/EnemyConfiguration", order = 0)]
    public class EnemyConfiguration : ScriptableObject
    {
        public int MaxHealth;
    }
}