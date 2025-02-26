using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TickAnimations : MonoBehaviour
{
    [SerializeField] public Image tickImage;
    private float duration = 1;

    public float StartTickAnimation()
    {
        StartCoroutine(TickAnimationCoroutine());
        return duration;
    }

    private void OnReset()
    {
        tickImage.fillAmount = 0;
    }

    private IEnumerator TickAnimationCoroutine()
    {
        tickImage.fillAmount = 0;
        float currentFill = 0;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            currentFill = Mathf.Lerp(currentFill, 1, time / duration);
            tickImage.fillAmount = currentFill;
            yield return null;
        }
    }
}
