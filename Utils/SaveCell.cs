using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    [System.Serializable]
    public struct SaveCell<T>
    {
        [SerializeField] public List<T> Data;
    }
}