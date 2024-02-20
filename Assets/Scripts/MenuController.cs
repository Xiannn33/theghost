using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 暂停按钮
    /// </summary>
    private Button pause;
    /// <summary>
    /// 继续按钮
    /// </summary>
    private Button contin;
    /// <summary>
    /// 存档按钮
    /// </summary>
    private Button save;
    /// <summary>
    /// 退出按钮
    /// </summary>
    private Button exit;
    /// <summary>
    /// 返回按钮
    /// </summary>
    private Button back;
    /// <summary>
    /// 提示框
    /// </summary>
    private Tip tip;
    /// <summary>
    /// 提示框是否显示
    /// </summary>
    private bool isTipShow = false;
    /// <summary>
    /// 位置偏移
    /// </summary>
    private Vector2 tipPosOffset = new Vector2(0, -20);

    private GameObject canvas;
    void Start()
    {
        pause = GameObject.Find("btn_Pause").GetComponent<Button>();
        //暂停按钮监听
        pause.onClick.AddListener(() => ShowMenu());
        tip = GameObject.Find("Tip").GetComponent<Tip>();
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        if (isTipShow)
        {
            //控制提示面板跟随鼠标
            //得到鼠标所在位置
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
            tip.SetPosition(position + tipPosOffset);
        }
    }
    public void ShowMenu()
    {
        pause.enabled = false;
        //暂停游戏
        Time.timeScale = 0;
        //实例化暂停菜单
        GameObject obj = Instantiate(Resources.Load<GameObject>("pal_Menu"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        obj.name = "pal_Menu";
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.SetAsFirstSibling();
        //继续按钮
        contin = GameObject.Find("btn_Continue").GetComponent<Button>();
        contin.onClick.AddListener(() => Contin());
        //存档按钮
        save = GameObject.Find("btn_Save").GetComponent<Button>();
        save.onClick.AddListener(() => Save());
        //返回按钮
        back = GameObject.Find("btn_Back").GetComponent<Button>();
        back.onClick.AddListener(() => Back());
        //退出按钮
        exit = GameObject.Find("btn_Exit").GetComponent<Button>();
        exit.onClick.AddListener(() => Exit());

    }
    /// <summary>
    /// 继续游戏
    /// </summary>
    public void Contin()
    {
        pause.enabled = true;
        Time.timeScale = 1;
        Destroy(GameObject.Find("pal_Menu"));
    }
    /// <summary>
    /// 存档
    /// </summary>
    public void Save()
    {
        PlayerData p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Save();
        List<PlayerData> list = DataMgr.Instance.Load();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].ID == PlayerPrefs.GetInt("Player"))
            {
                list[i] = p;
                break;
            }
        }
        DataMgr.Instance.Save(list);
    }
    /// <summary>
    /// 返回
    /// </summary>
    public void Back()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Save();
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR//在编辑器模式退出
        UnityEditor.EditorApplication.isPlaying = false;
#else//发布后退出
        Application.Quit();
#endif
    }
    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="text">文字内容</param>
    public void ShowTip(string text)
    {
        isTipShow = true;
        tip.Show(text);
    }
    /// <summary>
    /// 隐藏
    /// </summary>
    public void HideTip()
    {
        isTipShow = false;
        tip.Hide();
    }
    /// <summary>
    /// 鼠标移上
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        string str = "";
        switch (GetOverUI(canvas).name)
        {
            case "btn_Pause": str = "暂停"; break;
            case "btn_Continue": str = "继续游戏"; break;
            case "btn_Save": str = "存档"; break;
            case "btn_Back": str = "返回"; break;
            case "btn_Exit": str = "退出游戏"; break;
        }
        if (str != "")
            ShowTip(str);
    }
    /// <summary>
    /// 鼠标移出
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        HideTip();
    }
    /// <summary>
    /// 获取鼠标停留处UI
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <returns></returns>
    public GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[0].gameObject;
        }
        return null;
    }
}
