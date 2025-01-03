using System;
using System.Collections.Generic;
using System.Reflection;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class ComponentUtility
    {
        public static void SetActive(this Component component, bool state)
        {
            component.gameObject.SetActive(state);
        }
        
        public static void SetActive(this Component component, bool state, float delay)
        {
            Observable.Timer(TimeSpan.FromSeconds(delay)).Subscribe(x =>
            {
                component.gameObject.SetActive(state);
            }).AddTo(component.gameObject);
        }
        
        public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
        {
            return go.AddComponent<T>().GetCopyOf(toAdd) as T;
        }
        
        private static T GetCopyOf<T>(this T comp, T other) where T : Component
        {
            Type type = comp.GetType();
            Type othersType = other.GetType();
            if (type != othersType)
            {
                Debug.LogError($"The type \"{type.AssemblyQualifiedName}\" of \"{comp}\" does not match the type \"{othersType.AssemblyQualifiedName}\" of \"{other}\"!");
                return null;
            }

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default;
            PropertyInfo[] pinfos = type.GetProperties(flags);

            foreach (var pinfo in pinfos) 
            {
                if (pinfo.CanWrite) 
                {
                    try 
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch
                    {
                        /*
                         * In case of NotImplementedException being thrown.
                         * For some reason specifying that exception didn't seem to catch it,
                         * so I didn't catch anything specific.
                         */
                    }
                }
            }

            FieldInfo[] finfos = type.GetFields(flags);

            foreach (var finfo in finfos) 
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp as T;
        }
        
        public static T TakeRandom<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static Vector3 RandomVector3(this Vector3 axis, float range)
        {
            return RandomVector3(axis, 0, range);
        }
        
        public static Vector3 RandomVector3(this Vector3 axis, float minRange,float maxRange)
        {
            float x = Random.Range(minRange,maxRange);
            float y = Random.Range(minRange,maxRange);
            float z = Random.Range(minRange,maxRange);
            Vector3 randomPos = new(x,y,z);
            return Vector3.Scale(randomPos,axis);
        }

        public static bool TryGetComponentInParent<T1,T2>(this T1 comp, out T2 component) where T1 : Component
        {
            component = comp.GetComponentInParent<T2>();
            return component != null;
        }
    }
}