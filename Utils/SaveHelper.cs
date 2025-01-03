using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class SaveHelper
    {
        [Serializable]
        public struct DataPair<TKey, TValue>
        {
            [SerializeField] public TKey One;
            [SerializeField] public TValue Two;
        }
        
        public static void Load<T>(this List<T> list, string key)
        {
            if (!PlayerPrefs.HasKey(key)) return;

            string strData = PlayerPrefs.GetString(key);
            
            if (string.IsNullOrEmpty(strData)) return;

            var data = (SaveCell<T>) JsonUtility.FromJson(strData, typeof(SaveCell<T>));
            list.AddRange(data.Data.Select(d => d));
        }

        public static void Save<T>(this List<T> list, string key)
        {
            if (list == null || !list.Any())
            {
                PlayerPrefs.SetString(key, string.Empty);
                return;
            }
            
            SaveCell<T> data = new SaveCell<T>()
            {
                Data = list
            };
            
            PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
        }
    }
}