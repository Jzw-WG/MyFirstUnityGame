using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherSix : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float invisibleTime = 10f;
    public GameObject sprite;
    private SpriteRenderer spriteRenderer;
    private Coroutine fadeRoutine;

    void Start()
    {
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        fadeRoutine = StartCoroutine(FadeCycle());
    }

    IEnumerator FadeCycle()
    {
        while (true)
        {
            // 1. 渐隐（可见 → 透明）
            yield return StartCoroutine(FadeTo(0f));

            // 2. 隐身维持10秒
            yield return new WaitForSeconds(invisibleTime);

            // 3. 渐显（透明 → 可见）
            yield return StartCoroutine(FadeTo(1f));

            // 4. 可见后立即进入下一次循环
        }
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = spriteRenderer.color.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            Color c = spriteRenderer.color;
            c.a = newAlpha;
            spriteRenderer.color = c;

            yield return null;
        }

        // 强制设定目标透明度，避免浮点残差
        Color final = spriteRenderer.color;
        final.a = targetAlpha;
        spriteRenderer.color = final;
    }
}
