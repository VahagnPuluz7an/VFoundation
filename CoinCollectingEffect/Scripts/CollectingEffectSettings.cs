using UnityEngine;
using UnityEngine.UI;

namespace VFoundation.CoinCollectingEffect
{
    [CreateAssetMenu(fileName = "CollectingEffect", menuName = "ScriptableObjects/CollectingEffect")]
    public class CollectingEffectSettings : ScriptableObject
    {
        [field: SerializeField] public Sprite CoinSprite { get; private set; }
        [field: SerializeField] public int ImagesCount { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Vector2 ImageSize { get; private set; }
        [field: SerializeField] [field: Range(0, 5)] public float NoiseForce { get; private set; }
        [field: SerializeField] [field: Range(0.1f, 3f)] public float Splicing { get; private set; }
    }
}