using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    /// <summary>
    /// 物品
    /// </summary>
    public Item Item { get; set; }
    /// <summary>
    /// 物品数量
    /// </summary>
    public int Amount { get; set; }
    private Image itemSprite;
    private Text itemAmount;

    private Image ItemSprite
    {
        get
        {
            if (itemSprite == null)
                itemSprite = GetComponent<Image>();
            return itemSprite;
        }
    }

    private Text ItemAmount
    {
        get
        {
            if (itemAmount == null)
                itemAmount = GetComponentInChildren<Text>();
            return itemAmount;
        }
    }
    /// <summary>
    /// 修改Item
    /// </summary>
    /// <param name="item">物品</param>
    /// <param name="amount">数量</param>
    public void SetItem(Item item, int amount = 1)
    {
        Item = item;
        Amount = amount;
        ItemSprite.sprite = Resources.Load<Sprite>(item.Sprite);
        ItemAmount.text = Amount.ToString();
    }
    /// <summary>
    /// 修改物品数量
    /// </summary>
    /// <param name="amount">添加的数量</param>
    public void AddAmount(int amount = 1)
    {
        Amount += amount;
        itemAmount.text = Amount.ToString();
    }
    /// <summary>
    /// 使用物品
    /// 物品个数大于1时，数量减少
    /// 物品个数等于1时，直接销毁物品
    /// </summary>
    public void ReduceAmount()
    {
        Amount--;
        itemAmount.text = Amount.ToString();
    }
}
