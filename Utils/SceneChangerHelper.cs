using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public class SceneChangerHelper : MonoBehaviour
    {
        private static SceneChangerHelper _instance;

#if UNITY_EDITOR
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(this);
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.LoadScene(0);
            }
        }
#endif
    }
}