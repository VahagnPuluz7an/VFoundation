using UnityEngine;

namespace Vahag_s_Assets.Utils
{
    public class DisableObjectsCaller : MonoBehaviour
    {
        private void Start()
        {
            var objects = FindObjectsOfType<MonoBehaviour>(true);
            foreach (var mono in objects)
            {
                if(mono.TryGetComponent<IDisableObject>(out var disableObject))
                    disableObject.Initialize();
            }
        }

        private void OnDestroy()
        {
            var objects = FindObjectsOfType<MonoBehaviour>();
            foreach (var mono in objects)
            {
                if(mono.TryGetComponent<IDisableObject>(out var disableObject))
                    disableObject.Dispose();
            }
        }
    }
}
