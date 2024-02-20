using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InputHandel : MonoBehaviour
{
    Commend run;//J键
    Commend jump;//K键
    private GameObject go;
    public GameObject Go
    {
        get
        {
            go=GameObject.Find("Player").transform.GetChild(0).gameObject;
            return go;
        }
        set
        {
            go = value;
        }
    }

    private void Start()
    {
        //go = GameObject.Find("Player");
        //run = new MoveCommend();//强制绑定移动命令
        //jump = new JumpCommend();//强制绑定跳跃命令       
    }

    private void Update()
    {
        inputListen();//每帧监听
    }


    void inputListen()
    {
        ////如果按下J键就执行J键对应命令
        //if (Input.GetKey(KeyCode.J))
        //{
        //    run.execute(go);
        //}
        //else if (Input.GetKey(KeyCode.Space))
        //{
        //    jump.execute(go);
        //}
    }
}