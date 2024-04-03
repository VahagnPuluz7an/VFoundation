using UnityEngine;

namespace VFoundation.Helpers
{
    public static class ComponentUtility
    {
        public static void SetActive(this Component component, bool state)
        {
            component.gameObject.SetActive(state);
        }
    }
}