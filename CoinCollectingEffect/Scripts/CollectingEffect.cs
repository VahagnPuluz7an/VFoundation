using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using VFoundation.CoinCollectingEffect;
using Random = UnityEngine.Random;

namespace CoinCollectingEffect.Scripts
{
    public class CollectingEffect
    {
        public bool Playing => _sequences != null && _sequences.Any(x => x != null && x.IsPlaying());
        
        private readonly CollectingEffectSettings _settings;
        private readonly List<Transform> _imagesTransforms;
        private Tween[] _sequences;
        private readonly Transform _parent;
        private readonly bool _useGlobalPos;

        public CollectingEffect(CollectingEffectSettings settings,Transform parent,bool useGlobalPos = true)
        {
            _settings = settings;
            _imagesTransforms = new List<Transform>();
            _useGlobalPos = useGlobalPos;
            _parent = parent;
        }

        public void Effect(Vector2 startScreenPos, Vector3 endPoint, int count, Action callBack)
        {
            CompleteEffect(startScreenPos, endPoint, count, () => callBack?.Invoke());
        }
        
        public void Effect(Vector2 startScreenPos, Vector3 endPoint)
        {
            CompleteEffect(startScreenPos, endPoint, _settings.ImagesCount, () => {});
        }

        private void CompleteEffect(Vector2 startScreenPos, Vector3 endPoint, int imageCount, Action callBack)
        {
            if (imageCount <= 0)
                return;
            if(_imagesTransforms.Count <= 0 || _imagesTransforms.Count < imageCount)
                AddPool(imageCount - _imagesTransforms.Count, _settings, _parent);
            
            SetPositionForPool(startScreenPos, imageCount,_settings,_useGlobalPos);

            _sequences = new Tween[imageCount];
            
            if (_imagesTransforms == null) return;
            for (int i = 0; i < imageCount; i++)
            {
                Transform image = _imagesTransforms[i];
                image.localScale = Vector3.zero;
                image.SetActive(true);

                Sequence sq = DOTween.Sequence();
                sq.Append(image.DOScale(Vector3.one, _settings.Duration).SetEase(Ease.OutBack))
                    .Append(image.DOMove(endPoint, _settings.Duration).SetDelay(_settings.Splicing * i / 10f));
                int i1 = i;
                sq.onComplete += () =>
                {
                    _sequences[i1] = null;
                    image.SetActive(false);
                };
                _sequences[i] = sq;
            }

            CompositeDisposable disposable = new();
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (_sequences.Any(x => x != null && x.IsPlaying()))
                    return;
                callBack?.Invoke();
                disposable.Clear();
            }).AddTo(disposable);
        }

        private void AddPool(int count, CollectingEffectSettings settings ,Transform parent)
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

        private void SetPositionForPool(Vector2 startPos, int count,CollectingEffectSettings settings, bool useGlobalPos = true)
        {
            for (int i = 0; i < count; i++)
            {
                RectTransform image = _imagesTransforms[i].GetComponent<RectTransform>();
                if (useGlobalPos)
                    image.position = startPos + (Random.insideUnitCircle * (settings.NoiseForce * 100));
                else
                    image.position = startPos + (Random.insideUnitCircle * (settings.NoiseForce * 100));
            }
        }
    }
}
