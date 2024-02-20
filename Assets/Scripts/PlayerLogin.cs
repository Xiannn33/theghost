using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLogin : MonoBehaviour
{
    /// <summary>
    /// �˺�
    /// </summary>
    private InputField account;
    /// <summary>
    /// ����
    /// </summary>
    private InputField password;
    /// <summary>
    /// ȷ������
    /// </summary>
    private InputField password2;
    /// <summary>
    /// ��ʾ��
    /// </summary>
    private GameObject warnImage;
    /// <summary>
    /// ��¼��ť
    /// </summary>
    private Button login;
    /// <summary>
    /// ��¼�����ע�ᰴť
    /// </summary>
    private Button register;
    /// <summary>
    /// ע������ע�ᰴť
    /// </summary>
    private Button register1;
    /// <summary>
    /// ��ʾ������
    /// </summary>
    private Text warnText;
    /// <summary>
    /// ����˺���������
    /// </summary>
    private List<PlayerData> data;
    /// <summary>
    /// �洢���ݵ��ļ�
    /// </summary>
    private string fileName;
    void Start()
    {
        //�ļ�λ��
        fileName = Application.dataPath + "/Resources/Data/playerInfo.dat";
        //��ʼ��data
        data = new List<PlayerData>();
        Load();
        //ʵ������¼����
        GameObject obj = Instantiate(Resources.Load<GameObject>("pal_Login"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -40, 0);
        obj.name = "pal_Login";
        obj.transform.localScale = new Vector3(1, 1, 1);
        //�˺������
        account = GameObject.Find("inf_Account").GetComponent<InputField>();
        //���������
        password = GameObject.Find("inf_Password").GetComponent<InputField>();
        //��ʾ��
        warnImage = GameObject.Find("Canvas").transform.Find("img_Warn").gameObject;
        //��ʾ����
        warnText = warnImage.GetComponentInChildren<Text>();
        //��¼��ť
        login = GameObject.Find("btn_Login").GetComponent<Button>();
        //ע�ᰴť
        register = GameObject.Find("btn_Register").GetComponent<Button>();
        //��¼�¼�����
        login.onClick.AddListener(() => Login());
        //ע���¼�����
        register.onClick.AddListener(() => RegisterPanel());
    }
    void Update()
    {
    }
    /// <summary>
    /// ��֤
    /// �˺ź����������ֺ���ĸ���
    /// </summary>
    private bool Validate(string value)
    {
        Regex r = new Regex(@"^[a-zA-Z0-9]+$");
        return r.Match(value).Success;
    }
    /// <summary>
    /// ��¼
    /// </summary>
    private void Login()
    {
        //�ж��˺������Ƿ���Ϲ���
        if (!Validate(account.text) || !Validate(password.text))
        {
            warnImage.SetActive(true);
            warnText.text = "�˺ź�����ֻ������ĸ������";
        }
        else
        {
            if (File.Exists(fileName))
            {
                //�������������˺ź������Ƿ���ȷ
                foreach (var i in data)
                {

                    if (i.Account == account.text && i.Password == password.text)
                    {
                        Debug.Log(i.ID);
                        PlayerPrefs.SetInt("Player", i.ID);
                        PlayerPrefs.SetString("Role", "Ghost");
                        //��ת������Ϸ
                        SceneManager.LoadScene("Main");
                    }
                    else
                    {
                        warnImage.SetActive(true);
                        warnText.text = "�˺Ż��������";
                    }
                }
            }
            else
            {
                warnImage.SetActive(true);
                warnText.text = "�˺Ż��������";
            }
        }
    }
    /// <summary>
    /// ע�����
    /// </summary>
    private void RegisterPanel()
    {
        warnImage.SetActive(false);
        //���ص�¼����
        GameObject.Find("pal_Login").SetActive(false);
        //ʵ����ע�����
        GameObject obj = Instantiate(Resources.Load<GameObject>("pal_Register"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -54, 0);
        obj.name = "pal_Register";
        obj.transform.localScale = new Vector3(1, 1, 1);
        //�˺������
        account = GameObject.Find("inf_Account").GetComponent<InputField>();
        //���������
        password = GameObject.Find("inf_Password").GetComponent<InputField>();
        //�ٴ���������������
        password2 = GameObject.Find("inf_PasswordAgain").GetComponent<InputField>();
        //ע�ᰴť
        register1 = GameObject.Find("btn_Register").GetComponent<Button>();
        //ע���¼�����
        register1.onClick.AddListener(() => Register());
    }
    /// <summary>
    /// ע��
    /// </summary>
    private void Register()
    {
        //�������������˺ź������Ƿ���ȷ
        if (!Validate(account.text) || !Validate(password.text))
        {
            warnImage.SetActive(true);
            warnText.text = "�˺ź�����ֻ������ĸ������";
        }
        //������������������Ƿ���ͬ
        else if (password.text != password2.text)
        {
            warnImage.SetActive(true);
            warnText.text = "������������벻ͬ";
        }
        //ע��ɹ�����ת����¼����
        else
        {
            Save();
            warnImage.SetActive(true);
            warnText.text = "ע��ɹ�,���¼";
            Destroy(GameObject.Find("pal_Register"));
            GameObject.Find("Canvas").transform.Find("pal_Login").gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// ��Ϸ�浵
    /// </summary>
    private void Save()
    {
        PlayerData p = new PlayerData
        {
            ID = data.Count + 1,
            Account = account.text,
            Password = password.text
        };
        //�����ݼ�������
        data.Add(p);
        DataMgr.Instance.Save(data);
    }
    /// <summary>
    /// ��Ϸ����
    /// </summary>
    private void Load()
    {
        data = DataMgr.Instance.Load();
    }
}
