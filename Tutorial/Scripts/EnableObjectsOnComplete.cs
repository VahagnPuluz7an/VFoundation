using UnityEngine;

namespace VFoundation.Tutorial
{
    public class EnableObjectsOnComplete : MonoBehaviour
    {
        [SerializeField] private GameObject[] objects;
        [SerializeField] private bool destroyInComplete = true;
        [SerializeField] private float destroyTime = 1;
        
        private void Start()
        {
            if (TutorialManager.TutorialIsCompleted)
            {
                foreach (GameObject obj in objects)
                    Destroy(obj);
                return;
            }
            
            TutorialManager.TutorialCompleted += () =>
            {
                foreach (GameObject obj in objects)
                {
                    obj.SetActive(true);
                    if (destroyInComplete)
                    {
                        Destroy(obj, destroyTime);
                    }
                }
            };
        }
    }
}
