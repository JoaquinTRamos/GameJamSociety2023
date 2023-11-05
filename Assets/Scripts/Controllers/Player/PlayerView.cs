using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Image fillHpImage;



        public void UpdateHpBar(float p_percentage)
        {
            fillHpImage.fillAmount = p_percentage;
        }
    }
}