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
    /// 账号
    /// </summary>
    private InputField account;
    /// <summary>
    /// 密码
    /// </summary>
    private InputField password;
    /// <summary>
    /// 确认密码
    /// </summary>
    private InputField password2;
    /// <summary>
    /// 警示框
    /// </summary>
    private GameObject warnImage;
    /// <summary>
    /// 登录按钮
    /// </summary>
    private Button login;
    /// <summary>
    /// 登录界面的注册按钮
    /// </summary>
    private Button register;
    /// <summary>
    /// 注册界面的注册按钮
    /// </summary>
    private Button register1;
    /// <summary>
    /// 警示框文字
    /// </summary>
    private Text warnText;
    /// <summary>
    /// 玩家账号密码数据
    /// </summary>
    private List<PlayerData> data;
    /// <summary>
    /// 存储数据的文件
    /// </summary>
    private string fileName;
    void Start()
    {
        //文件位置
        fileName = Application.dataPath + "/Resources/Data/playerInfo.dat";
        //初始化data
        data = new List<PlayerData>();
        Load();
        //实例化登录界面
        GameObject obj = Instantiate(Resources.Load<GameObject>("pal_Login"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -40, 0);
        obj.name = "pal_Login";
        obj.transform.localScale = new Vector3(1, 1, 1);
        //账号输入框
        account = GameObject.Find("inf_Account").GetComponent<InputField>();
        //密码输入框
        password = GameObject.Find("inf_Password").GetComponent<InputField>();
        //警示框
        warnImage = GameObject.Find("Canvas").transform.Find("img_Warn").gameObject;
        //警示文字
        warnText = warnImage.GetComponentInChildren<Text>();
        //登录按钮
        login = GameObject.Find("btn_Login").GetComponent<Button>();
        //注册按钮
        register = GameObject.Find("btn_Register").GetComponent<Button>();
        //登录事件监听
        login.onClick.AddListener(() => Login());
        //注册事件监听
        register.onClick.AddListener(() => RegisterPanel());
    }
    void Update()
    {
    }
    /// <summary>
    /// 验证
    /// 账号和密码由数字和字母组成
    /// </summary>
    private bool Validate(string value)
    {
        Regex r = new Regex(@"^[a-zA-Z0-9]+$");
        return r.Match(value).Success;
    }
    /// <summary>
    /// 登录
    /// </summary>
    private void Login()
    {
        //判断账号密码是否符合规则
        if (!Validate(account.text) || !Validate(password.text))
        {
            warnImage.SetActive(true);
            warnText.text = "账号和密码只能是字母和数字";
        }
        else
        {
            if (File.Exists(fileName))
            {
                //检验玩家输入的账号和密码是否正确
                foreach (var i in data)
                {

                    if (i.Account == account.text && i.Password == password.text)
                    {
                        Debug.Log(i.ID);
                        PlayerPrefs.SetInt("Player", i.ID);
                        PlayerPrefs.SetString("Role", "Ghost");
                        //跳转进入游戏
                        SceneManager.LoadScene("Main");
                    }
                    else
                    {
                        warnImage.SetActive(true);
                        warnText.text = "账号或密码错误";
                    }
                }
            }
            else
            {
                warnImage.SetActive(true);
                warnText.text = "账号或密码错误";
            }
        }
    }
    /// <summary>
    /// 注册界面
    /// </summary>
    private void RegisterPanel()
    {
        warnImage.SetActive(false);
        //隐藏登录界面
        GameObject.Find("pal_Login").SetActive(false);
        //实例化注册界面
        GameObject obj = Instantiate(Resources.Load<GameObject>("pal_Register"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -54, 0);
        obj.name = "pal_Register";
        obj.transform.localScale = new Vector3(1, 1, 1);
        //账号输入框
        account = GameObject.Find("inf_Account").GetComponent<InputField>();
        //密码输入框
        password = GameObject.Find("inf_Password").GetComponent<InputField>();
        //再次输入密码的输入框
        password2 = GameObject.Find("inf_PasswordAgain").GetComponent<InputField>();
        //注册按钮
        register1 = GameObject.Find("btn_Register").GetComponent<Button>();
        //注册事件监听
        register1.onClick.AddListener(() => Register());
    }
    /// <summary>
    /// 注册
    /// </summary>
    private void Register()
    {
        //检验玩家输入的账号和密码是否正确
        if (!Validate(account.text) || !Validate(password.text))
        {
            warnImage.SetActive(true);
            warnText.text = "账号和密码只能是字母和数字";
        }
        //检验两次输入的密码是否相同
        else if (password.text != password2.text)
        {
            warnImage.SetActive(true);
            warnText.text = "输入的两次密码不同";
        }
        //注册成功，跳转到登录界面
        else
        {
            Save();
            warnImage.SetActive(true);
            warnText.text = "注册成功,请登录";
            Destroy(GameObject.Find("pal_Register"));
            GameObject.Find("Canvas").transform.Find("pal_Login").gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 游戏存档
    /// </summary>
    private void Save()
    {
        PlayerData p = new PlayerData
        {
            ID = data.Count + 1,
            Account = account.text,
            Password = password.text
        };
        //将数据加入数组
        data.Add(p);
        DataMgr.Instance.Save(data);
    }
    /// <summary>
    /// 游戏读档
    /// </summary>
    private void Load()
    {
        data = DataMgr.Instance.Load();
    }
}
