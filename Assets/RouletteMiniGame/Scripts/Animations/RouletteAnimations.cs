using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteMiniGame.EventSystem;
using RouletteMiniGame.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace RouletteMiniGame.Animations
{
    public class RouletteAnimations : MonoBehaviour
    {
        private float _initialHighlightIntervalDuration;
        private float _secondaryHighlightIntervalDuration;
        private List<CuisinePartyCircleRowAnimations> _circleRowAnimationsList;

        public void InitializeDependencies(float initialHighlightIntervalDuration, float secondaryHighlightIntervalDuration, List<GameObject> circleRowGOList)
        {
             _initialHighlightIntervalDuration = initialHighlightIntervalDuration;
             _secondaryHighlightIntervalDuration = secondaryHighlightIntervalDuration;
             _circleRowAnimationsList = new List<CuisinePartyCircleRowAnimations>();
             foreach (var circleRowGO in circleRowGOList)
             {
                 _circleRowAnimationsList.Add(circleRowGO.GetComponent<CuisinePartyCircleRowAnimations>());
             }
        }

        private void Awake()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GlobalEventSystem.Instance.Subscribe<Events.Events.SlotSelectEvent>(StartAnimation);
        }
        

        private void UnsubscribeEvents()
        {
            GlobalEventSystem.Instance.Unsubscribe<Events.Events.SlotSelectEvent>(StartAnimation);
        }

        public void OnDisable()
        {
            UnsubscribeEvents();
        }

        public void StartAnimation(Events.Events.SlotSelectEvent e)
        {
            StartCoroutine(RouletteSpinAnimation(e.SelectedSlotTask));
        }

        public IEnumerator RouletteSpinAnimation(Task<Slot> targetSlotTask)
        {
            int targetRowIndex = -1;
            int targetTurnCount = 2;
            int turnCount = 0;
            int index = 0;
            WaitForSeconds initialWait = new WaitForSeconds(_initialHighlightIntervalDuration);
            WaitForSeconds secondaryWait = new WaitForSeconds(_secondaryHighlightIntervalDuration);
            while (turnCount < targetTurnCount)
            {
                _circleRowAnimationsList[index].OnHighlight();
                yield return initialWait;
                NextIndex();
                if (turnCount == targetTurnCount && targetSlotTask.Status != TaskStatus.RanToCompletion) 
                {
                    targetTurnCount++;
                }
            }
            targetRowIndex = targetSlotTask.Result.Index;
            for (index = 0; index < targetRowIndex; index++)
            {
                _circleRowAnimationsList[index].OnHighlight();
                yield return secondaryWait;
            }

            _circleRowAnimationsList[targetRowIndex].OnSelect();

            void NextIndex()
            {
                if (index == _circleRowAnimationsList.Count - 1)
                {
                    index = 0;
                    turnCount++;
                }
                else
                {
                    index++;
                }
            }
        }
    }
}