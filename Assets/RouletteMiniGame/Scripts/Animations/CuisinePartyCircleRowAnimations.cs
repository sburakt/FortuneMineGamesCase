using System.Collections;
using DG.Tweening;
using RouletteMiniGame.Events;
using RouletteMiniGame.EventSystem;
using UnityEngine;

public interface ICircleRowAnimations
{
    public void SetRewardAnimation(RewardAnimations rewardAnimation);
    public void SetTickAnimation(TickAnimations tickAnimation);
    public void OnHighlight();
    public void OnSelect();
    public void OnDeactivate();
}
public class CuisinePartyCircleRowAnimations : MonoBehaviour, ICircleRowAnimations
{
    RewardAnimations _rewardAnimation;
    TickAnimations _tickAnimation;
    [System.Serializable]public struct HighlightSettings
    {
        public float duration;
    }
    [System.Serializable]public struct SelectSettings
    {
        public float duration;
        public float fadeInDuration;
        public int pulseNumber;
    }

    [SerializeField] private HighlightSettings highlightSettings;
    [SerializeField] private SelectSettings selectSettings;
    [SerializeField] private SpriteRenderer coloredSpriteRenderer;
    [SerializeField] private SpriteRenderer glowSpriteRenderer;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite graySprites;

    public void SetRewardAnimation(RewardAnimations rewardAnimation)
    {
        _rewardAnimation = rewardAnimation;
    }

    public void SetTickAnimation(TickAnimations tickAnimation)
    {
        _tickAnimation = tickAnimation;
    }
    public void OnHighlight()
    {
        StartCoroutine(Pulse(1, highlightSettings.duration, glowSpriteRenderer));
    }

    public void OnSelect()
    {
        StartCoroutine(SelectAnimation());
    }

    public void OnDeactivate()
    {
        coloredSpriteRenderer.sprite = graySprites;
    }
    
    public void OnReset()
    {
        coloredSpriteRenderer.sprite = blueSprite;
        SetAlpha(0, coloredSpriteRenderer);
    }
    
    private IEnumerator SelectAnimation()
    {
        StartCoroutine(Pulse(selectSettings.pulseNumber, selectSettings.duration, glowSpriteRenderer));
        yield return new WaitForSeconds(selectSettings.duration);
        StartCoroutine(FadeIn(selectSettings.fadeInDuration,coloredSpriteRenderer));
        yield return new WaitForSeconds(selectSettings.fadeInDuration);
        float  startTickAnimationDuration = _tickAnimation.StartTickAnimation();
        yield return new WaitForSeconds(startTickAnimationDuration);
        TweenCallback OnRewardReachedWallet = new TweenCallback(DeactivateAnimation);
        _rewardAnimation.SendRewardToWallet(OnRewardReachedWallet);
    }

    private void DeactivateAnimation()
    {
        coloredSpriteRenderer.sprite = graySprites;
        GlobalEventSystem.Instance.Invoke(new Events.SpinAnimationCompleteEvent{});
    }
    

    #region FadeMethods
    IEnumerator Pulse(int pulseNumber ,float duration, SpriteRenderer spriteRenderer)
    {
        float fadeDuration = duration / (2 * pulseNumber);
        for (int i = 0; i < pulseNumber; i++)
        {
            StartCoroutine(FadeIn(fadeDuration, spriteRenderer));
            yield return new WaitForSeconds(fadeDuration);
            StartCoroutine(FadeOut(fadeDuration, spriteRenderer));
            yield return new WaitForSeconds(fadeDuration);
        }
    }
    
    IEnumerator FadeOut(float duration, SpriteRenderer spriteRenderer)
    {
        float startAlpha = spriteRenderer.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, time / duration);
            SetAlpha(alpha, spriteRenderer);
            yield return null;
        }
        SetAlpha(0f, spriteRenderer);
    }
    
    IEnumerator FadeIn(float duration, SpriteRenderer spriteRenderer)
    {
        float startAlpha = spriteRenderer.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 1, time / duration);
            SetAlpha(alpha, spriteRenderer);
            yield return null;
        }
        SetAlpha(1f,spriteRenderer);
    }

    void SetAlpha(float value, SpriteRenderer spriteRenderer)
    {
        Color color = spriteRenderer.color;
        color.a = value; 
        spriteRenderer.color = color;
    }
    #endregion
}