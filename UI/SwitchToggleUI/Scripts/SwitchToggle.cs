using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour
{
   [SerializeField] private RectTransform uiHandleRectTransform;
   [SerializeField] private Color handleActiveColor;

   private Image _handleImage;
   private Color _handleDefaultColor;
   private Vector2 _handlePosition;

   public Toggle Toggle { get; private set; }

   public void Init()
   {
      Toggle = GetComponent<Toggle>();

      _handlePosition = uiHandleRectTransform.anchoredPosition;

      _handleImage = uiHandleRectTransform.GetComponent<Image>();

      _handleDefaultColor = _handleImage.color;

      Toggle.onValueChanged.AddListener(OnSwitch);
      Toggle.onValueChanged.AddListener(SwitchWithoutAnim);
   }

   private void OnSwitch(bool on)
   {
      if (!gameObject.activeInHierarchy)
         return;
      
      uiHandleRectTransform.DOAnchorPos(on ? _handlePosition * -1 : _handlePosition, .4f).SetEase(Ease.InOutBack);
      _handleImage.DOColor(on ? handleActiveColor : _handleDefaultColor, .4f);
   }

   private void SwitchWithoutAnim(bool on)
   {
      if (gameObject.activeInHierarchy)
         return;
      
      uiHandleRectTransform.anchoredPosition = on ? _handlePosition * -1 : _handlePosition;
      _handleImage.color = on ? handleActiveColor : _handleDefaultColor;
   }

   private void OnDestroy() => Toggle.onValueChanged.RemoveAllListeners();
}