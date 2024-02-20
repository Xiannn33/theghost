using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店
/// </summary>
public class Shop : Inventory
{
    private static Shop instance;
    public static Shop Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("Shop").GetComponent<Shop>();
            }
            return instance;
        }
    }
    /// <summary>
    /// 玩家数据
    /// </summary>
    private PlayerData p;
    public PlayerData P
    {
        get
        {
            p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Save();
            return p;
        }
        set
        {
            p = value;
        }
    }
    private Text txtMoney;
    public Text TxtMoney
    {
        get
        {
            txtMoney = transform.GetChild(1).GetChild(0).GetComponent<Text>();
            txtMoney.text = P.Money.ToString();
            return txtMoney;
        }
    }
}

