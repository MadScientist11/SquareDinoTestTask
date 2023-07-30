using UnityEngine;

namespace Game.Source.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;

        
        public void TogglePlayerLogic(bool active)
        {
            _playerMovement.enabled = active;
            _playerAttack.enabled = active;
        }
    }
}