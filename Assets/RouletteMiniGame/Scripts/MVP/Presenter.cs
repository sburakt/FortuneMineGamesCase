using System.Threading.Tasks;
using RouletteMiniGame.Animations;
using RouletteMiniGame.Data;
using RouletteMiniGame.Events;
using RouletteMiniGame.EventSystem;
using UnityEngine;
using RouletteMiniGame.Logic;

namespace RouletteMiniGame.MVP
{
    public class Presenter
    {
        private Model _model;
        private RouletteLogic _rouletteLogic;
        private Wallet _playerWallet;

        private void SubscribeEvents()
        {
            GlobalEventSystem.Instance.Subscribe<UIEvents.SpinButtonPushEvent>(OnSpinButtonPushed);
            GlobalEventSystem.Instance.Subscribe<UnityGameEvents.SceneUnloadEvent>(OnsceneUnload);
        }

        private void UnsubscribeEvents()
        {
            GlobalEventSystem.Instance.Unsubscribe<UIEvents.SpinButtonPushEvent>(OnSpinButtonPushed);
            GlobalEventSystem.Instance.Unsubscribe<UnityGameEvents.SceneUnloadEvent>(OnsceneUnload);
        }

        private void OnsceneUnload(UnityGameEvents.SceneUnloadEvent scene)
        {
            UnsubscribeEvents();
        }

        public Presenter(Model model)
        {
            _model = model;
            _rouletteLogic = new RouletteLogic(_model.slots);
            _playerWallet = _model.PlayerWallet;
            SubscribeEvents();
        }

        private void SpinRoulette()
        {
            Task<Slot> selectedSlotTask = RequestRewardSlotAsync();
            GlobalEventSystem.Instance.Invoke<Events.Events.SlotSelectEvent>(new Events.Events.SlotSelectEvent()
            {
                SelectedSlotTask = selectedSlotTask
            });
        }

        async Task<Slot> RequestRewardSlotAsync()
        {
            Slot selectedSlot = await _rouletteLogic.GetSelectSlotAsTask();
            //await Task.Delay(10000) possible network delay mock;
            _playerWallet.AddReward(selectedSlot.Reward, selectedSlot.Amount);
            return selectedSlot;
        }

        private void OnSpinButtonPushed(UIEvents.SpinButtonPushEvent e)
        {
            GlobalEventSystem.Instance.Invoke(new Events.Events.SpinAnimationTriggerEvent { });
            SpinRoulette();
        }
    }
}