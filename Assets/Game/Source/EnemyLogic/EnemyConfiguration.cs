using UnityEngine;

namespace Game.Source.EnemyLogic
{
    [CreateAssetMenu(menuName = "EnemyConfiguration", fileName = "Game/EnemyConfiguration", order = 0)]
    public class EnemyConfiguration : ScriptableObject
    {
        public int MaxHealth;
    }
}