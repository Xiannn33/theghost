using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    /// <summary>
    /// 文字内容
    /// </summary>
    private Text tipText;
    private Text contentText;
    /// <summary>
    /// 画布组（控制显示或隐藏）
    /// </summary>
    private CanvasGroup canvasGroup;
    /// <summary>
    /// 目标透明度
    /// </summary>
    private float targetAlpha = 0;
    /// <summary>
    /// 渐变动画的快慢
    /// </summary>
    private float smooth = 2;

    private void Start()
    {
        tipText = GetComponent<Text>();
        contentText = transform.Find("txt_Content").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smooth * Time.fixedDeltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
    /// <summary>
    /// 显示
    /// </summary>
    public void Show(string text)
    {
        tipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public void Hide()
    {
        targetAlpha = 0;
    }
    /// <summary>
    /// 设置提示窗位置
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
