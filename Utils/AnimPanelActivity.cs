using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class AnimPanelActivity : MonoBehaviour
    {
        public event Action Opened;
        public event Action OpenedFull;
        public event Action Closed;
        public event Action ClosedFull;

        [SerializeField] private Transform panel;
        [SerializeField] private Image backImage;
        [SerializeField] private Vector3 scaleAxis = Vector3.one;

        private float _backAlpha;
        
        private void Awake()
        {
            if (backImage != null)  _backAlpha = backImage.color.a;
        }

        public void Open()
        {
            if (gameObject.activeSelf && panel.gameObject.activeSelf)
                return;
            
            Opened?.Invoke();
            gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            
            Vector3 x = Vector3.one;
            if (scaleAxis.x > 0) x.x = 0;
            if (scaleAxis.y > 0) x.y = 0;
            if (scaleAxis.z > 0) x.z = 0;

            panel.localScale = x;

            panel.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).onComplete += () =>
            {
                OpenedFull?.Invoke();
            };
            
            if (backImage == null) return;
            
            Color defColor = backImage.color;
            defColor.a = 0;
            backImage.color = defColor;
            backImage.DOFade(_backAlpha, 0.2f).SetUpdate(true);
        }

        public void OpenWhenClosing()
        {
            panel.DOComplete(true);
            Open();
        }

        public void Close()
        {
            if (!gameObject.activeSelf || !panel.gameObject)
                return;
            
            Closed?.Invoke();
            panel.localScale = Vector3.one;
            if (backImage != null)
                backImage.DOFade(0, 0.2f);

            Vector3 x = Vector3.one;
            if (scaleAxis.x > 0) x.x = 0;
            if (scaleAxis.y > 0) x.y = 0;
            if (scaleAxis.z > 0) x.z = 0;
            
            panel.DOScale(x, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
                gameObject.SetActive(false);
                ClosedFull?.Invoke();
            });
        }
    }
}
