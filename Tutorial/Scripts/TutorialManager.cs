using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Tutorial
{
    [DefaultExecutionOrder(-100)]
    public class TutorialManager : MonoBehaviour
    {
        public static event Action TutorialCompleted; 
        public static event Action FirstPartInited; 
        public static event Action FullInited; 
        public static event Action NextStep;
        public static event Action OnPouse;
        public static event Action OnResume;
        public static int StepIndex { get; private set; }
        public static TutorialPart CurrentPart => _parts[StepIndex];
        public static bool CurrentPartActive => CurrentPart.gameObject.activeSelf;
        private static bool TutorialIsCompleted => StepIndex >= _parts.Length;

        [SerializeField] private TutorialPart[] parts;
        [SerializeField] private TutorialType tutorialType;

        private static TutorialReverser _reverser;
        
        private static TutorialPart[] _parts;
        private static TutorialType _tutorialType;
        private static GameObject _gameObject;
        
        private void Awake()
        {
            _parts = parts;
            _tutorialType = tutorialType;
            _gameObject = gameObject;
            
            StepIndex = PlayerPrefs.GetInt("TutorialStepIndex");
            Observable.TimerFrame(1).Subscribe(_ => FirstPartInited?.Invoke()).AddTo(this);
        }

        public static void FinishInit()
        {
            foreach (TutorialPart part in _parts)
                if (part != null)
                    part.SetActive(false);

            if (TutorialIsCompleted)
            {
                _gameObject.SetActive(false);
                FullInited?.Invoke();
                return;
            }
            
            _gameObject.SetActive(true);

            foreach (TutorialPart part in _parts)
                if (part != null)
                    part.SetActive(false);
            
            while (_parts[StepIndex] == null)
            {
                StepIndex++;
            }
            
            _parts[StepIndex].SetActive(true);
            FullInited?.Invoke();
        }

        public static bool ProhibitedFor(IEnumerable<int> indexes)
        {
            return !TutorialIsCompleted && indexes.Contains(StepIndex);
        }

        public static void SaveStep() => PlayerPrefs.SetInt("TutorialStepIndex", StepIndex);

        public static void SetNewStep(int stepIndex,bool nextEnabled)
        {
            if (TutorialIsCompleted || stepIndex <= StepIndex || stepIndex > StepIndex + 1)
                return;
            
            _parts[StepIndex].SetActive(false);
            
            StepIndex = stepIndex;
            
            while (!TutorialIsCompleted && _parts[StepIndex] == null)
            {
                StepIndex++;
            }
            
            NextStep?.Invoke();
            
            if(!TutorialIsCompleted)
                _parts[StepIndex].SetActive(nextEnabled);
            else
            {
                _gameObject.SetActive(false);
                TutorialCompleted?.Invoke();
            }

            Save();
        }

        public static void Pause(int index)
        {
            while (index < _parts.Length && _parts[index] == null)
            {
                index++;
            }
            
            if (index != StepIndex) return;

            _parts[StepIndex].SetActive(false);
            OnPouse?.Invoke();
        }

        public static void Resume(int index)
        {
            while (index < _parts.Length && _parts[index] == null)
            {
                index++;
            }
            
            if (index != StepIndex) return;

            _parts[StepIndex].SetActive(true);
            OnResume?.Invoke();
        }

        public static void HardSetStep(int index)
        {
            StepIndex = index;
            SaveStep();
        }

        private static void Save()
        {
            switch (_tutorialType)
            {
                case TutorialType.SaveEveryStep:
                    SaveStep();
                    break;
                case TutorialType.SaveManual:
                    break;
                case TutorialType.SaveInEnd:
                    if (TutorialIsCompleted)
                        SaveStep();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [ContextMenu("SkipTutorial")]
        private void SkipTutorial()
        {
            HardSetStep(_parts.Length);
            TutorialCompleted?.Invoke();
        }
    }
}