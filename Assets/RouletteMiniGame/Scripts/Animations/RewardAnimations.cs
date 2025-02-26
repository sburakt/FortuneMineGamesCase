using System;
using System.Collections;
using DG.Tweening;
using RouletteMiniGame.AssetManagement;
using RouletteMiniGame.Data;
using RouletteMiniGame.UiHandlers;
using UnityEngine;

public class RewardAnimations : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer rewardSpriteRenderer;
    static Transform walletTransform;
    private RewardDataBase.Reward _reward;

    public void Start()
    {
        if (walletTransform == null)
            walletTransform = FindObjectOfType<UiWalletHandler>().transform;
    }

    public void SetReward( RewardDataBase.Reward reward )
    {
        _reward = reward;
        rewardSpriteRenderer.sprite = RewardSpriteLoader.GetSprite(reward);
    }

    public void SendRewardToWallet( TweenCallback onRewardReachedWallet)
    {
        spriteTransform.DOMove(walletTransform.position, 1f).SetEase(Ease.InBack).OnComplete(onRewardReachedWallet);
        spriteTransform.DOScale(Vector3.zero, 1.1f).SetEase(Ease.InExpo);
    }

    private void OnReset()
    {
        transform.localPosition = Vector3.zero;
    }
}