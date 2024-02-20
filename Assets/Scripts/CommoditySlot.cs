using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommoditySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject content;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    List<Item> list = new List<Item>();
    /// <summary>
    /// �������
    /// </summary>
    private PlayerData p;
    /// <summary>
    /// �����
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
    /// �������Ʒ�۴洢��Ʒ
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
    /// �õ���ǰ��Ʒ�۴洢����Ʒ(ID)
    /// </summary>
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<CommodityUI>().Item.ID;
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        string tip = transform.GetChild(0).GetComponent<CommodityUI>().Item.GetTipText();
        InventoryMgr.Instance.ShowTip(tip);
    }
    /// <summary>
    /// ����Ƴ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryMgr.Instance.HideTip();
    }
    /// <summary>
    /// ����
    /// </summary>
    public void OnClick()
    {
        money = Shop.Instance.P.Money;
        Item item = transform.GetChild(0).GetComponent<CommodityUI>().Item;
        if (money >= item.BuyPrice)
        {
            //���������Ʒ����������
            list.Add(item);
            //��������ʾ��Ӧ��Ʒ
            Packsack.Instance.StoreItem(item);
            //���������
            money -= item.BuyPrice;
            txtMoney.text = money.ToString();
            //���ݴ洢
            p.ItemList = list;
            p.Money = money;
            DataMgr.Instance.P = p;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PlayerBuy();
        }
        else
        {
            Debug.Log("��������㣬����ʧ�ܣ�");
        }
    }
}
