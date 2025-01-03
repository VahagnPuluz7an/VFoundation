using UnityEngine;

namespace Tutorial
{
    public class TutorialPart : MonoBehaviour
    {
        [SerializeField] private GameObject[] follows;
        public void SetActive(bool active)
        {
            foreach (GameObject follow in follows) follow.SetActive(active);
            gameObject.SetActive(active);
        }
    }
}