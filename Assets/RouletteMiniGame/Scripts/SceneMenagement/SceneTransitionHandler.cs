using System;
using System.Collections;
using RouletteMiniGame.Data;
using RouletteMiniGame.Events;
using RouletteMiniGame.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RouletteMiniGame.SceneMenagement
{
    public class SceneTransitionHandler : MonoBehaviour
    {
        private void Awake()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            GlobalEventSystem.Instance.Subscribe<Events.Events.SlotSelectEvent>(OnSlotSelected);
        }

        private void Unsubscribe()
        {
            GlobalEventSystem.Instance.Unsubscribe<Events.Events.SlotSelectEvent>(OnSlotSelected);
            GlobalEventSystem.Instance.Unsubscribe<Events.Events.SpinAnimationCompleteEvent>(OnLastSpinAnimationComplete);
        }

        private async void OnSlotSelected(Events.Events.SlotSelectEvent e)
        {
            Slot slot = await e.SelectedSlotTask;
            if (slot.IsLast)
                GlobalEventSystem.Instance.Subscribe<Events.Events.SpinAnimationCompleteEvent>(OnLastSpinAnimationComplete);
        }

        private void OnLastSpinAnimationComplete(Events.Events.SpinAnimationCompleteEvent e)
        {
            SceneManager.LoadSceneAsync(0);
        }
        
        public void OnDisable()
        {
            GlobalEventSystem.Instance.Invoke <Events.UnityGameEvents.SceneUnloadEvent>(new UnityGameEvents.SceneUnloadEvent{});
            Unsubscribe();
        }
    }
}