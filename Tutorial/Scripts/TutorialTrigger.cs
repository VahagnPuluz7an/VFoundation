using UnityEngine;

namespace VFoundation.Tutorial
{
   public class TutorialTrigger : MonoBehaviour
   {
      [SerializeField] private string objectTag;
      [SerializeField] private int stepIndex;
      [SerializeField] private bool nextEnabled;
      [SerializeField] private bool resumeTutorial;
      [SerializeField] private bool pauseTutorial;
      
      private void OnTriggerEnter(Collider other)
      {
         if (!other.CompareTag(objectTag)) return;

         if (resumeTutorial)
         {
            TutorialManager.Resume(stepIndex);
            return;
         }

         if (pauseTutorial)
         {
            TutorialManager.Pause(stepIndex);
            return;
         }
         
         TutorialManager.SetNewStep(stepIndex,nextEnabled);
      }
   }
}