using System.Collections.Generic;
using RouletteMiniGame.AssetManagement;
using RouletteMiniGame.Data;
using RouletteMiniGame.EventSystem;
using RouletteMiniGame.MVP;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RouletteMiniGame.UiHandlers
{
    public class UiWalletHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private Dictionary<RewardDataBase.Reward, TextMeshProUGUI> rewardDictionary = new();

        private Wallet _playerWallet;
        public GameObject ElementPrefab;
        public GameObject WalletCanvas;
        private Slot _recentlyAddedSlot;
        private void Start()
        {
            _playerWallet = ModelLocator.GetModel().PlayerWallet;
            foreach (var reward in _playerWallet.GetRewardAmountDictionary().Keys)
            {
                AddElement(reward);
            }
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GlobalEventSystem.Instance.Subscribe<Events.Events.SlotSelectEvent>(OnSlotSelected);
            GlobalEventSystem.Instance.Subscribe<Events.Events.SpinAnimationCompleteEvent>(OnSpinAnimationCompleted);
        }

        private void UnsubscribeEvents()
        {
            GlobalEventSystem.Instance.Unsubscribe<Events.Events.SlotSelectEvent>(OnSlotSelected);
            GlobalEventSystem.Instance.Unsubscribe<Events.Events.SpinAnimationCompleteEvent>(OnSpinAnimationCompleted);
        }

        public void OnDisable()
        {
            UnsubscribeEvents();
        }

        void AddElement(RewardDataBase.Reward reward)
        {
            if (!rewardDictionary.ContainsKey(reward))
            {
                GameObject ElementGO = Instantiate(ElementPrefab, WalletCanvas.transform);
                Image ElementImage = ElementGO.transform.GetChild(0).GetComponent<Image>();
                ElementImage.overrideSprite = RewardSpriteLoader.GetSprite(reward);
                var tmp = ElementGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                rewardDictionary.Add(reward, tmp);
                tmp.text = _playerWallet.GetRewardAmountDictionary()[reward].ToString();
            }
            else
            {
                rewardDictionary[reward].text = _playerWallet.GetRewardAmountDictionary()[reward].ToString();
            }
        }


        private void ShowWalletContent()
        {
            WalletCanvas.SetActive(true);
        }

        private void HideWalletContent()
        {
            WalletCanvas.SetActive(false);
        }

        private void OnSpinAnimationCompleted(Events.Events.SpinAnimationCompleteEvent e)
        {
            AddElement(_recentlyAddedSlot.Reward);
        }

        private async void OnSlotSelected(Events.Events.SlotSelectEvent slotSelectEvent)
        {
            Slot slot = await slotSelectEvent.SelectedSlotTask;
            _recentlyAddedSlot = slot;
        }

        // Pointer Events

        public void OnPointerDown(PointerEventData eventData)
        {
            ShowWalletContent();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideWalletContent();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            HideWalletContent();
        }

    }
}