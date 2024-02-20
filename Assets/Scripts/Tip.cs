using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>
    private Text tipText;
    private Text contentText;
    /// <summary>
    /// �����飨������ʾ�����أ�
    /// </summary>
    private CanvasGroup canvasGroup;
    /// <summary>
    /// Ŀ��͸����
    /// </summary>
    private float targetAlpha = 0;
    /// <summary>
    /// ���䶯���Ŀ���
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
    /// ��ʾ
    /// </summary>
    public void Show(string text)
    {
        tipText.text = text;
        contentText.text = text;
        targetAlpha = 1;
    }
    /// <summary>
    /// ����
    /// </summary>
    public void Hide()
    {
        targetAlpha = 0;
    }
    /// <summary>
    /// ������ʾ��λ��
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
