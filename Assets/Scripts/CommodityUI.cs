using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommodityUI : MonoBehaviour
{
    /// <summary>
    /// ��Ʒ
    /// </summary>
    public Item Item { get; set; }
    /// <summary>
    /// ��ƷͼƬ
    /// </summary>
    private Image commoditySprite;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    private Text commodityName;
    /// <summary>
    /// ��Ʒ�۸�
    /// </summary>
    private Text commodityPrice;

    private Image CommoditySprite
    {
        get
        {
            if (commoditySprite == null)
                commoditySprite = GetComponent<Image>();
            return commoditySprite;
        }
    }

    private Text CommodityName
    {
        get
        {
            if (commodityName == null)
                commodityName = transform.Find("txt_CommodityName").GetComponent<Text>();
            return commodityName;
        }
    }

    private Text CommodityPrice
    {
        get
        {
            if (commodityPrice == null)
                commodityPrice = transform.Find("txt_CommodityPrice").GetComponent<Text>();
            return commodityPrice;
        }
    }
    public void SetCommodity(Item item)
    {
        Item = item;
        CommoditySprite.sprite = Resources.Load<Sprite>(item.Sprite);
        CommodityName.text = item.Name;
        CommodityPrice.text = item.BuyPrice.ToString();
    }
}
