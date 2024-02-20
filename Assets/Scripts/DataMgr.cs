using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DataMgr
{
    private static DataMgr instance;
    public static DataMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataMgr();
            }
            return instance;
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
                data = Load();
                Load();
            }
            return data;
        }
    }

    /// <summary>
    /// 游戏读档
    /// </summary>
    /// <param name="fileName">文件地址</param>
    /// <param name="data">数据</param>
    public void Save(List<PlayerData> data)
    {
        //首先实例化一个二进制串行化对象，用于将数据串行化
        BinaryFormatter bf = new BinaryFormatter();
        //然后实例化一个文件流用于在目录中创建 plyaerInfo.dat 文件
        FileStream file = File.Create(FileName);
        //序列化储存
        bf.Serialize(file, data);
        //关闭流操作
        file.Close();
    }
    /// <summary>
    /// 游戏读档
    /// </summary>
    public List<PlayerData> Load()
    {
        List<PlayerData> tmp = new List<PlayerData>();
        //首先确定有储存信息
        if (File.Exists(FileName))
        {
            //将文件反序列化
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(FileName, FileMode.Open);
            tmp = bf.Deserialize(file) as List<PlayerData>;
            file.Close();
        }
        return tmp;
    }
    public PlayerData GetData()
    {
        PlayerData tmp = new PlayerData();
        foreach (var i in Data)
        {
            if (i.ID == PlayerPrefs.GetInt("Player"))
            {
                tmp = i;
                break;
            }
        }
        return tmp;
    }
    private PlayerData p;
    public PlayerData P
    {
        get
        {
            return GetData();
        }
        set
        {
            p = value;
        }
    }

    //public PlayerData SetData(int money,float health)
    //{
    //    p.Money = money;
    //    p.Health = health;
    //    return p;
    //}
    //public PlayerData SetData(Item item)
    //{
    //    p.AddItem(item);
    //    return p;
    //}
    //public PlayerData GetThis()
    //{
    //    return p;
    //}
    //public List<PlayerData> SetDataList(PlayerData player)
    //{
    //    foreach (var i in Data)
    //    {
    //        if (i.ID == PlayerPrefs.GetInt("Player"))
    //        {
    //            i.Health = p.Health;
    //            i.Level = p.Level;
    //            i.Money = p.Money;
    //            i.ItemList = p.ItemList;
    //            break;
    //        }
    //    }
    //    return Data;
    //}
}
