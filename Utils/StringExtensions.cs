using UnityEngine;
using Color = UnityEngine.Color;

namespace Utils
{
    public static class StringExtensions
    {
        public static string ChangeColor(this string text, Color col) =>
            $"<color={ColorHexFromUnityColor(col)}>{text}</color>";

        public static string ChangeColorAndSize(this string text, Color col, float size)
        {
            return  $"<color={ColorHexFromUnityColor(col)}>" +
                    $"<size={size}>{text}";
        }

        public static string ColorHexFromUnityColor(this Color unityColor) =>
            $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";

        public static string AddZeroSprite(this string text) => $"<sprite=0> {text}";
    }
}