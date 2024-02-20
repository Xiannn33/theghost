using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

class StoreData
{
    // 二进制序列化
    public static void SerializeMethod(string fileName,List<PlayerData> data)   
    {
        FileStream fs = new FileStream(fileName, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, data);
        fs.Close();
    }
    // 二进制反序列化
    public static List<PlayerData> DeserializeMethod(string fileName)     
    {
        FileStream fs = new FileStream(fileName, FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        List<PlayerData> data = (List<PlayerData>)bf.Deserialize(fs);
        fs.Close();
        return data;
    }
}

