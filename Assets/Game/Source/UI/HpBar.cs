using UnityEngine;
using UnityEngine.UI;

namespace Game.Source.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;
        
        public void SetValue(float normalizedValue)
        {
            _healthImage.fillAmount = normalizedValue;
        }
    }
}
