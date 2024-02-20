using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommoditySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject content;
    /// <summary>
    /// 物品数组
    /// </summary>
    List<Item> list = new List<Item>();
    /// <summary>
    /// 玩家数据
    /// </summary>
    private PlayerData p;
    /// <summary>
    /// 金币数
    /// </summary>
    private int money;
    private Text txtMoney;
    void Start()
    {
        p = Shop.Instance.P;
        list = Shop.Instance.P.ItemList;

        txtMoney = Shop.Instance.TxtMoney;

        content = GameObject.Find("Content");
        transform.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }
    /// <summary>
    /// 在这个物品槽存储物品
    /// </summary>
    /// <param name="item">item</param>
    public void StoreCommodity(Item item)
    {
        GameObject itemobj = Instantiate(Resources.Load<GameObject>("Commodity"));
        itemobj.transform.SetParent(this.transform);
        itemobj.transform.localPosition = Vector3.zero;
        itemobj.transform.localScale = Vector3.one;
        itemobj.GetComponent<CommodityUI>().SetCommodity(item);

    }
    /// <summary>
    /// 得到当前商品槽存储的商品(ID)
    /// </summary>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<CommodityUI>().Item.ID;
    }
    /// <summary>
    /// 鼠标移上
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        string tip = transform.GetChild(0).GetComponent<CommodityUI>().Item.GetTipText();
        InventoryMgr.Instance.ShowTip(tip);
    }
    /// <summary>
    /// 鼠标移出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryMgr.Instance.HideTip();
    }
    /// <summary>
    /// 购买
    /// </summary>
    public void OnClick()
    {
        money = Shop.Instance.P.Money;
        Item item = transform.GetChild(0).GetComponent<CommodityUI>().Item;
        if (money >= item.BuyPrice)
        {
            //将购买的物品放入数组中
            list.Add(item);
            //背包里显示对应物品
            Packsack.Instance.StoreItem(item);
            //金币数减少
            money -= item.BuyPrice;
            txtMoney.text = money.ToString();
            //数据存储
            p.ItemList = list;
            p.Money = money;
            DataMgr.Instance.P = p;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PlayerBuy();
        }
        else
        {
            Debug.Log("金币数不足，购买失败！");
        }
    }
}
