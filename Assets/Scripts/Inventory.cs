using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /// <summary>
    /// 物品槽数组
    /// </summary>
    private Slot[] slotList;
    public Slot[] SlotList
    {
        get
        {
            if (slotList == null)
                slotList = GameObject.Find("Canvas").transform.Find("Packsack").GetComponentsInChildren<Slot>();
            return slotList;
        }
    }

    /// <summary>
    /// 存储数据的文件
    /// </summary>
    private string fileName;
    public string FileName
    {
        get
        {
            if (fileName == null)
                fileName = Application.dataPath + "/Resources/Data/playerInfo.dat";
            return fileName;
        }
    }
    /// <summary>
    /// 用户数据
    /// </summary>
    private List<PlayerData> data;
    public List<PlayerData> Data
    {
        get
        {
            if (data == null)
            {
                data = new List<PlayerData>();
                DataMgr.Instance.Load();
            }
            return data;
        }
    }

    void Update()
    {

    }

    /// <summary>
    /// 通过ID存储物品
    /// </summary>
    /// <param name="id"></param>
    /// <returns>能否存储</returns>
    public bool StoreItem(int id)
    {
        Item item = InventoryMgr.Instance.GetItemById(id);
        return StoreItem(item);
    }

    /// <summary>
    /// 根据item存储物品
    /// </summary>
    /// <param name="item"></param>
    /// <returns>能否存储</returns>
    public bool StoreItem(Item item)
    {
        if (item == null) return false;
        Slot slot1 = FindSlotWithSameItem(item);
        if (slot1 != null)
        {
            slot1.StoreItem(item);
        }
        else //找新的空物品槽
        {
            Slot slot2 = FindEmptySlot();
            if (slot2 != null)
            {
                slot2.StoreItem(item);
            }
            else
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 找装有同一个物品的物品槽
    /// </summary>
    /// <param name="item">item</param>
    /// <returns>Slot</returns>
    public Slot FindSlotWithSameItem(Item item)
    {
        foreach (var slot in SlotList)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.ID)
            {
                return slot;
            }
        }
        return null;
    }

    public void SetPacksack()
    {
        foreach (var slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                //立即销毁
                DestroyImmediate(slot.transform.GetChild(0).gameObject);
            }
        }
    }

    /// <summary>
    /// 找新的物品槽
    /// </summary>
    /// <returns></returns>
    public Slot FindEmptySlot()
    {
        foreach (var slot in SlotList)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }
        return null;
    }

    public void ShowCommodity()
    {
        InventoryMgr.Instance.ShowCommodity();
    }
}
