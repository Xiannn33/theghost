using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InputHandel : MonoBehaviour
{
    Commend run;//J��
    Commend jump;//K��
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
        //run = new MoveCommend();//ǿ�ư��ƶ�����
        //jump = new JumpCommend();//ǿ�ư���Ծ����       
    }

    private void Update()
    {
        inputListen();//ÿ֡����
    }


    void inputListen()
    {
        ////�������J����ִ��J����Ӧ����
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