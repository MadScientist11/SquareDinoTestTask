using UnityEngine;

namespace Game.Source.PlayerLogic
{
    [CreateAssetMenu(menuName = "PlayerConfiguration", fileName = "PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        public float AttackCooldown;
        public float Speed;
    }
}
