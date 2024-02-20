using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品基类
/// </summary>
[Serializable]
public class Item
{
    /// <summary>
    /// 物品ID
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 物品名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 物品属性描述
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 物品购买价格
    /// </summary>
    public int BuyPrice { get; set; }
    /// <summary>
    /// 物品出售价格
    /// </summary>
    public int SellPrice { get; set; }
    /// <summary>
    /// 物品显示图标
    /// </summary>
    public string Sprite { get; set; }

    public Item(int id, string name, string description, int buyPrice, int sellPrice, string sprite)
    {
        ID = id;
        Name = name;
        Description = description;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Sprite = sprite;
    }
    /// <summary>
    /// 提示面板应该显示的内容
    /// </summary>
    /// <returns></returns>
    public virtual string GetTipText()
    {
        string str = Name + "n" + Description;
        return str.Replace('n', '\n');
    }
    public virtual int GetHP()
    {
        return 0;
    }
    public int GetIDByName(string name)
    {
        if (Name.Equals(name))
            return ID;
        return -1;
    }
}
