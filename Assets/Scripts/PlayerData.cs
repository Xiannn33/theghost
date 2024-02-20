using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class PlayerData
{
    /// <summary>
    /// 玩家ID
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 玩家账号
    /// </summary>
    public string Account { get; set; }
    /// <summary>
    /// 玩家密码
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// 关卡
    /// </summary>
    public int Level { get; set; } = 1;
    /// <summary>
    /// 金币数
    /// </summary>
    public int Money { get; set; } = 0;
    /// <summary>
    /// 生命值
    /// </summary>
    public float Health { get; set; } = 100;
    /// <summary>
    /// 物品list
    /// </summary>
    public List<Item> ItemList { get; set; } = new List<Item>();
}
