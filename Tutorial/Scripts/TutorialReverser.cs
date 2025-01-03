using System;
using UnityEngine;

namespace Tutorial
{
    public class TutorialReverser : MonoBehaviour
    {
        [SerializeField] private ReverserStruct[] structs;

        public bool HasReverse => structs.Length > 0;
        
        private void Awake()
        {          
            TutorialManager.FirstPartInited += TutorialManagerOnFirstPartInited;
        }

        private void OnDestroy()
        {
            TutorialManager.FirstPartInited -= TutorialManagerOnFirstPartInited;
        }

        private void TutorialManagerOnFirstPartInited()
        {
            if (!HasReverse)
                return;

            foreach (ReverserStruct reverserStruct in structs)
            {
                if (TutorialManager.StepIndex != reverserStruct.fromThis) continue;
                TutorialManager.HardSetStep(reverserStruct.toThis);
                break;
            }
            TutorialManager.FinishInit();
        }
    }
    
    [Serializable]
    public struct ReverserStruct
    {
        public int fromThis;
        public int toThis;
    }
}
