using UnityEngine;

namespace VFoundation.Tutorial
{
    [CreateAssetMenu(fileName = "PointerSettings", menuName = "ScriptableObjects/PointerSettings")]
    public class PointerSettings : ScriptableObject
    {
        [field: SerializeField] public GameObject PointerPrefab { get; private set; }
    }
}
