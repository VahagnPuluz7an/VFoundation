using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VFoundation.Helpers;
using Random = UnityEngine.Random;

namespace VFoundation.CoinCollectingEffect
{
    public class CollectingEffect : MonoBehaviour
    {
        private static List<Transform> _imagesTransforms = new ();

        private void Awake()
        {
            _imagesTransforms = new List<Transform>();
        }

        private void OnDestroy()
        {
            _imagesTransforms = new List<Transform>();
        }

        public static void Effect(Vector2 startScreenPos, Vector3 endPoint, Transform parent, int count, CollectingEffectSettings settings)
        {
            CompleteEffect(startScreenPos, endPoint, parent, count, settings, () => {});
        }
        
        public static void Effect(Vector2 startScreenPos, Vector3 endPoint, Transform parent, int count, CollectingEffectSettings settings, Action callBack)
        {
            CompleteEffect(startScreenPos, endPoint, parent, count, settings, () => callBack?.Invoke());
        }
        
        public static void Effect(Vector2 startScreenPos, Vector3 endPoint, Transform parent, CollectingEffectSettings settings)
        {
            CompleteEffect(startScreenPos, endPoint, parent, settings.ImagesCount, settings, () => {});
        }

        private static void CompleteEffect(Vector2 startScreenPos, Vector3 endPoint, Transform parent, int imageCount,CollectingEffectSettings settings, Action callBack)
        {
            if (imageCount <= 0)
                return;
            
            if(_imagesTransforms.Count <= 0 || _imagesTransforms.Count < imageCount)
                AddPool(imageCount - _imagesTransforms.Count, settings, parent);
            
            SetPositionForPool(startScreenPos, imageCount,settings);

            if (_imagesTransforms == null) return;
            for (int i = 0; i < imageCount; i++)
            {
                Transform image = _imagesTransforms[i];
                image.localScale = Vector3.zero;
                image.SetActive(true);

                Sequence sq = DOTween.Sequence();
                sq.Append(image.DOScale(Vector3.one, settings.Duration).SetEase(Ease.OutBack))
                    .Append(image.DOMove(endPoint, settings.Duration).SetDelay(settings.Splicing * i / 10f));
                    //.Append(image.DOScale(Vector3.zero, settings.Duration / 2f));
                sq.onComplete += () =>
                {
                    image.SetActive(false);
                    callBack?.Invoke();
                };
            }
        }

        private static void AddPool(int count, CollectingEffectSettings settings ,Transform parent)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject imgObject = new("EffectImage " + _imagesTransforms.Count);
                imgObject.transform.SetParent(parent);

                RectTransform trans = imgObject.AddComponent<RectTransform>();
                trans.localScale = Vector3.one;
                trans.anchoredPosition = new Vector2(0f, 0f);
                trans.sizeDelta = settings.ImageSize;

                Image image = imgObject.AddComponent<Image>();
                image.sprite = settings.CoinSprite;
                
                imgObject.SetActive(false);
                _imagesTransforms.Add(imgObject.transform);
            }
        }

        private static void SetPositionForPool(Vector2 startPos, int count ,CollectingEffectSettings settings)
        {
            for (int i = 0; i < count; i++)
            {
                Transform image = _imagesTransforms[i];
                image.position = startPos + (Random.insideUnitCircle * settings.NoiseForce * 100);
            }
        }
    }
}
