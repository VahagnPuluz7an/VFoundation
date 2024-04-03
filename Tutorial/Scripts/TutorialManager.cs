using System;
using System.Collections.Generic;
using System.Linq;
using DSystem;
using UnityEngine;

namespace VFoundation.Tutorial
{
    [DefaultExecutionOrder(-100)]
    [DisableInitialize]
    public class TutorialManager : DBehaviour
    {
        public static event Action TutorialCompleted; 
        public static event Action Inited; 
        public static int StepIndex { get; private set; }
        public static TutorialPart CurrentPart => _parts[StepIndex];
        public static bool TutorialIsCompleted => StepIndex >= _parts.Length;

        [SerializeField] private TutorialPart[] parts;
        [SerializeField] private TutorialType tutorialType;

        [Inject] private static TutorialReverser _reverser;
        private static TutorialPart[] _parts;
        private static TutorialType _tutorialType;
        private static GameObject _gameObject;

        protected override void OnInitialized()
        {
            _parts = parts;
            _tutorialType = tutorialType;
            _gameObject = gameObject;
            
            StepIndex = PlayerPrefs.GetInt("TutorialStepIndex");
            Inited?.Invoke();

            // if (!_reverser.HasReverse)
            //     FinishInit();
        }

        public static void FinishInit()
        {
            foreach (TutorialPart part in _parts)
                part.SetActive(false);

            if (TutorialIsCompleted)
            {
                _gameObject.SetActive(false);
                return;
            }
            
            _gameObject.SetActive(true);

            foreach (TutorialPart part in _parts) part.SetActive(false);
            _parts[StepIndex].SetActive(true);
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
            if (index != StepIndex) return;
            
            _parts[StepIndex].SetActive(false);
        }

        public static void Resume(int index)
        {
            if (index != StepIndex) return;
            
            _parts[StepIndex].SetActive(true);
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
    }
}