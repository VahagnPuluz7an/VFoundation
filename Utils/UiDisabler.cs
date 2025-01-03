using UnityEngine;
using UnityEngine.UI;

namespace Helpers
{
    public class UiDisabler : MonoBehaviour
    {
        [ContextMenu("DisableAllUI")]
        private void DisableAllUI()
        {
            Graphic[] graphics = GetComponentsInChildren<Graphic>();
            foreach (Graphic graphic in graphics)
            {
                Color col = graphic.color;
                col.a = 0;
                graphic.color = col;
            }
        }
    }
}
