using System;
using DSystem;
using UnityEngine;

namespace VFoundation.Tutorial
{
    [AutoRegistry]
    public class TutorialReverser : MonoBehaviour
    {
        [SerializeField] private ReverserStruct[] structs;

        public bool HasReverse => structs.Length > 0;
        
        public void Awake()
        {
            TutorialManager.Inited += TutorialManagerOnInited;
        }

        public void OnDestroy()
        {
            TutorialManager.Inited -= TutorialManagerOnInited;
        }

        private void TutorialManagerOnInited()
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
