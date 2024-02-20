using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

public class InventoryMgr : MonoBehaviour
{
    private static InventoryMgr instance;
    /// <summary>
    /// 存放物品的集合
    /// </summary>
    private List<Item> itemList;
    /// <summary>
    /// 提示框
    /// </summary>
    private Tip tip;

    private bool isTipShow = false;

    private RectTransform canvas;

    /// <summary>
    /// 位置偏移
    /// </summary>
    private Vector2 tipPosOffset = new Vector2(20, -20);

    private void Start()
    {
        ParseItemsJSON();
        tip = GameObject.Find("Tip").GetComponent<Tip>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isTipShow)
        {
            //控制提示面板跟随鼠标
            //得到鼠标所在位置
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out position);
            tip.SetPosition(position + tipPosOffset);
        }
    }

    /// <summary>
    /// 单例模式
    /// </summary>
    public static InventoryMgr Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("InventoryMgr").GetComponent<InventoryMgr>();
            return instance;
        }
    }
    /// <summary>
    /// 解析物品信息Json文件
    /// </summary>
    private void ParseItemsJSON()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Data/Items");
        JArray jsonText = (JArray)JsonConvert.DeserializeObject(itemText.text);
        foreach (var tmp in jsonText)
        {
            int id = (int)tmp["id"];
            string name = (string)tmp["name"];
            string description = (string)tmp["description"];
            int buyPrice = (int)tmp["buyPrice"];
            int sellPrice = (int)tmp["sellPrice"];
            string sprite = (string)tmp["sprite"];
            int hp = (int)tmp["hp"];
            int aggressivity = (int)tmp["aggressivity"];
            Item item = new Consumable(id, name, description, buyPrice, sellPrice, sprite, hp, aggressivity);
            itemList.Add(item);
        }
    }
    /// <summary>
    /// 通过ID获取item
    /// </summary>
    /// <param name="id">物品ID</param>
    /// <returns>item</returns>
    public Item GetItemById(int id)
    {
        foreach (var item in itemList)
        {
            if (item.ID == id)
                return item;
        }
        return null;
    }
    /// <summary>
    /// 通过Name获取item
    /// </summary>
    /// <param name="id">物品ID</param>
    /// <returns>item</returns>
    public Item GetItemByName(string name)
    {
        foreach (var item in itemList)
        {
            if (item.Name == name)
                return item;
        }
        return null;
    }
    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="text">文字内容</param>
    public void ShowTip(string text)
    {
        isTipShow = true;
        tip.Show(text);
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public void HideTip()
    {
        isTipShow = false;
        tip.Hide();
    }

    public void ShowCommodity()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (GameObject.Find("Content").transform.childCount < itemList.Count)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("CommoditySlot"));
                obj.transform.SetParent(GameObject.Find("Content").transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                CommoditySlot slot = GameObject.Find("Content").transform.GetChild(i).GetComponent<CommoditySlot>();
                slot.StoreCommodity(itemList[i]);
            }
        }
    }
}
