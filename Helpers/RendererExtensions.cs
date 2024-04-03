using System;
using DG.Tweening;
using UnityEngine;

namespace VFoundation.Helpers
{
    public static class RendererExtensions
    {
        public static void SetFloatAnim(this Renderer renderer, Material mat, string key, float value,
            float duration = 0, Action onComplete = null)
        {
            MaterialPropertyBlock block = new();
            int matIndex = Array.IndexOf(renderer.sharedMaterials, mat);
            renderer.GetPropertyBlock(block, matIndex);

            if (duration <= 0)
            {
                block.SetFloat(key, value);
                renderer.SetPropertyBlock(block, matIndex);
                onComplete?.Invoke();
                return;
            }

            DOVirtual.Float(block.GetFloat(key), value, duration, fl =>
            {
                block.SetFloat(key, fl);
                renderer.SetPropertyBlock(block, matIndex);
            }).onComplete += () => onComplete?.Invoke();
        }

        public static void SetColorAnim(this Renderer renderer, Material mat, string key, Color value, float duration = 0, Action onComplete = null)
        {
            MaterialPropertyBlock block = new();
            int matIndex = Array.IndexOf(renderer.sharedMaterials, mat);
            renderer.GetPropertyBlock(block, matIndex);

            if (duration <= 0)
            {
                block.SetColor(key, value);
                renderer.SetPropertyBlock(block, matIndex);
                onComplete?.Invoke();
                return;
            }

            DOVirtual.Color(block.GetColor(key), value, duration, fl =>
            {
                block.SetColor(key, fl);
                renderer.SetPropertyBlock(block, matIndex);
            }).onComplete += () => onComplete?.Invoke();;
        }
    }
}
