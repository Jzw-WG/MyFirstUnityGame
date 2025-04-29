using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherOne : MonoBehaviour
{
    public Vector3 startScale = Vector3.one;          // 初始缩放
    public Vector3 targetScale = Vector3.one * 5f;    // 目标缩放
    public float duration = 30f;                       // 放大时间（秒）
    public bool loop = false;                         // 是否循环放大/还原

    private float timer = 0f;

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);
        transform.localScale = Vector3.Lerp(startScale, targetScale, t);

        if (t >= 1f)
        {
            if (loop)
            {
                // 反转放大/缩小方向
                (startScale, targetScale) = (targetScale, startScale);
                timer = 0f;
            }
            else
            {
                enabled = false; // 放完一次就停
            }
        }
    }
}
