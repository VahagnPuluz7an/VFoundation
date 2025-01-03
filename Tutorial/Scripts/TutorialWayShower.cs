using UniRx;
using UnityEngine;

namespace Tutorial
{
    public class TutorialWayShower : MonoBehaviour
    {
        [SerializeField] private int showIndex;
        [SerializeField] private int hideIndex;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform[] endPoints;
        [SerializeField] private Vector3 offset;

        private LineRenderer _line;
        private static readonly CompositeDisposable Disposable = new();

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
            _line.useWorldSpace = true;
            TutorialManager.FullInited += TryChangeActivity;
            TutorialManager.NextStep += TryChangeActivity;
            TutorialManager.OnResume += TryChangeActivity;
        }

        private void OnDestroy()
        {
            TutorialManager.NextStep -= TryChangeActivity;
            TutorialManager.FullInited -= TryChangeActivity;
            TutorialManager.OnResume -= TryChangeActivity;
            Disposable.Clear();
        }
        
        private void TryChangeActivity()
        {
            if (TutorialManager.StepIndex == showIndex && TutorialManager.CurrentPartActive)
            {
                Disposable.Clear();
                gameObject.SetActive(true);
                return;
            }

            if (TutorialManager.StepIndex == hideIndex)
            {
                Disposable.Clear();
                gameObject.SetActive(false);
                return;
            }

            // Observable.TimerFrame(1).Subscribe(x =>
            // {
            //     gameObject.SetActive(false);
            // }).AddTo(Disposable);
        }
        
        private void Update()
        {
            int activeEndPointIndex = -1;
            
            for (int i = 0; i < endPoints.Length; i++)
            {
                if (endPoints[i] != null && endPoints[i].gameObject.activeSelf)
                {
                    activeEndPointIndex = i;
                    break;
                }
            }

            if (activeEndPointIndex < 0)
            {
                gameObject.SetActive(false);
                return;
            }
            
            _line.positionCount = 2;
            _line.SetPosition(0, startPoint.position + offset);
            _line.SetPosition(1, endPoints[activeEndPointIndex].position + offset);
        }
    }
}
