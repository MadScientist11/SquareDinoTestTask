using UnityEngine;
using UnityEngine.UI;

namespace Game.Source.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _healthImage;
        
        public void SetValue(float currentValue, float maxValue)
        {
            _healthImage.fillAmount = Mathf.Max(0, currentValue / maxValue);
        }
    }
}
