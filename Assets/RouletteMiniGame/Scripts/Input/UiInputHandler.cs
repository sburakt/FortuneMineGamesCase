using System;
using System.Collections;
using System.Collections.Generic;
using RouletteMiniGame.Events;
using RouletteMiniGame.EventSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace RouletteMiniGame.UiHandlers
{
    public class UiInputHandler : MonoBehaviour
    {
        [SerializeField] private GameObject ButtonGO;


        private void Awake()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GlobalEventSystem.Instance.Subscribe<Events.Events.SpinAnimationTriggerEvent>(OnAnimationStarted);
            GlobalEventSystem.Instance.Subscribe<Events.Events.SpinAnimationCompleteEvent>(OnAnimationCompleted);
        }

        public void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            GlobalEventSystem.Instance.Unsubscribe<Events.Events.SpinAnimationTriggerEvent>(OnAnimationStarted);
            GlobalEventSystem.Instance.Unsubscribe<Events.Events.SpinAnimationCompleteEvent>(OnAnimationCompleted);
        }

        private void EnableButton()
        {
            ButtonGO.SetActive(true);
        }

        private void DisableButton()
        {
            ButtonGO.SetActive(false);
        }


        // Event Functions

        private void OnAnimationCompleted(Events.Events.SpinAnimationCompleteEvent e)
        {
            EnableButton();
        }

        private void OnAnimationStarted(Events.Events.SpinAnimationTriggerEvent e)
        {
            DisableButton();
        }
    }
}