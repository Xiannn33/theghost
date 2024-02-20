using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommodityUI : MonoBehaviour
{
    /// <summary>
    /// 物品
    /// </summary>
    public Item Item { get; set; }
    /// <summary>
    /// 商品图片
    /// </summary>
    private Image commoditySprite;
    /// <summary>
    /// 商品名称
    /// </summary>
    private Text commodityName;
    /// <summary>
    /// 商品价格
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
