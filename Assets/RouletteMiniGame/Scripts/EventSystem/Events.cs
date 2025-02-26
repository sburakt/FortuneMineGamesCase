using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RouletteMiniGame.EventSystem;
using RouletteMiniGame.Data;
using UnityEngine;

namespace RouletteMiniGame.Events
{

    public class UnityGameEvents
    {
        public struct SceneUnloadEvent : IEvent
        {
            
        }
    }
    
    public class Events
    {
        public struct SpinAnimationTriggerEvent : IEvent
        { }
        
        public struct SpinAnimationCompleteEvent : IEvent
        { }

        public struct SlotSelectEvent : IEvent
        {
            public Task<Slot> SelectedSlotTask;
            public bool IsLastSlot;
        }
        
        public struct AllSlotsClearEvent : IEvent
        {
            
        }
    }

    public class UIEvents
    {
        public struct SpinButtonPushEvent : IEvent
        {
            public enum PushStatus
            {
                Push,
                Release
            }
            public PushStatus ButtonPushStatus;
        }
    }
}