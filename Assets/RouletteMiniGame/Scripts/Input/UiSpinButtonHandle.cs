using RouletteMiniGame.EventSystem;
using RouletteMiniGame.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RouletteMiniGame.UiHandlers
{
    public class UiSpinButtonHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        Transform ButtonTransform;
        Vector3 OnPressScale = new Vector3(0.9f, 0.9f, 1f);
        bool ButtonPressed = false;

        private void Start()
        {
            ButtonTransform = gameObject.transform;
        }

        // Pointer Events

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonPressed = true;
            ButtonTransform.localScale = OnPressScale;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonTransform.localScale = Vector3.one;
            if (ButtonPressed)
            {
                ButtonPressed = true;
                GlobalEventSystem.Instance.Invoke<UIEvents.SpinButtonPushEvent>(new UIEvents.SpinButtonPushEvent()
                {
                    ButtonPushStatus = UIEvents.SpinButtonPushEvent.PushStatus.Release
                });
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ButtonPressed = false;
            ButtonTransform.localScale = Vector3.one;
        }
    }
}