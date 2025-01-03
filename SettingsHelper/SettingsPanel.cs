using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Image backImage;
        [SerializeField] private Transform panel; 
        
        public void Open()
        {
            Time.timeScale = 0;
            Color defColor = backImage.color;
            defColor.a = 0;
            backImage.color = defColor;
            panel.localScale = Vector3.zero;
            gameObject.SetActive(true);
            
            backImage.DOFade(1, 0.2f).SetUpdate(true);
            panel.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetUpdate(true);
        }
        
        public void Close()
        {
            Time.timeScale = 1;
            backImage.DOFade(0, 0.2f);
            panel.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).SetUpdate(true).onComplete += () => gameObject.SetActive(false);
        }
    }
}