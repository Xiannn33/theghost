using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerData p;
    public PlayerData P
    {
        get
        {
            if (p == null)
                p = DataMgr.Instance.GetData();
            return p;
        }
    }
    private void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }
    /// <summary>
    /// 在这个物品槽存储物品
    /// 如果已经存在物品则数量+1
    /// 如果不存在物品，则实例化该物品
    /// </summary>
    /// <param name="item">item</param>
    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject itemobj = Instantiate(Resources.Load<GameObject>("Item"));
            itemobj.transform.SetParent(this.transform);
            itemobj.transform.localPosition = Vector3.zero;
            itemobj.transform.localScale = Vector3.one;
            itemobj.GetComponent<ItemUI>().SetItem(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddAmount();
        }
    }
    /// <summary>
    /// 得到当前物品槽存储的物品(ID)
    /// </summary>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUI>().Item.ID;
    }

    /// <summary>
    /// 鼠标移上
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string tip = transform.GetChild(0).GetComponent<ItemUI>().Item.GetTipText();
            InventoryMgr.Instance.ShowTip(tip);
        }
    }
    /// <summary>
    /// 鼠标移出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
            InventoryMgr.Instance.HideTip();
    }
    /// <summary>
    /// 使用物品
    /// </summary>
    public void OnClick()
    {
        if (transform.childCount != 0)
        {
            Item item = transform.GetChild(0).GetComponent<ItemUI>().Item;
            UseItem(item);
            GameObject.Find("Player").GetComponentInChildren<Player>().UseItem(item);
            GameObject.Find("Player").GetComponentInChildren<Player>().HealthAddition(item.GetHP());
        }
    }
    public void UseItem(Item item)
    {
        if (transform.GetChild(0).GetComponent<ItemUI>().Amount == 1)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUI>().ReduceAmount();
        }
    }
}
