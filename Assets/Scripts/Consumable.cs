using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>
/// 消耗品类
/// </summary>
public class Consumable : Item
{
    /// <summary>
    /// 物品可以加的血量
    /// </summary>
    public int HP { get; set; }
    /// <summary>
    /// 物品可以加的攻击力
    /// </summary>
    public int Aggressivity { get; set; }

    public Consumable(int id, string name, string description,int buyPrice, int sellPrice, string sprite,int hp,int aggressivity)
        : base(id, name, description, buyPrice, sellPrice, sprite)
    {
        HP = hp;
        Aggressivity = aggressivity;
    }

    public override int GetHP()
    {
        return HP;
    }
}
