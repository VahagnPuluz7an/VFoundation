using UnityEngine;

namespace VFoundation.Tutorial
{
    public class DisablerInTutorial : MonoBehaviour
    {
        private void Start()
        {
            if (TutorialManager.TutorialIsCompleted)
                return;
            TutorialManager.TutorialCompleted += () => { gameObject.SetActive(true); };
            gameObject.SetActive(false);
        }
    }
}
