using UnityEngine;

namespace Game.Source.UI
{
    public class BaseScreen : MonoBehaviour
    {
        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);

        public void Refresh()
        {
            Hide();
            Show();
        }
    }
}