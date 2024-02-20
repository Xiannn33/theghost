using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// ��ͣ��ť
    /// </summary>
    private Button pause;
    /// <summary>
    /// ������ť
    /// </summary>
    private Button contin;
    /// <summary>
    /// �浵��ť
    /// </summary>
    private Button save;
    /// <summary>
    /// �˳���ť
    /// </summary>
    private Button exit;
    /// <summary>
    /// ���ذ�ť
    /// </summary>
    private Button back;
    /// <summary>
    /// ��ʾ��
    /// </summary>
    private Tip tip;
    /// <summary>
    /// ��ʾ���Ƿ���ʾ
    /// </summary>
    private bool isTipShow = false;
    /// <summary>
    /// λ��ƫ��
    /// </summary>
    private Vector2 tipPosOffset = new Vector2(0, -20);

    private GameObject canvas;
    void Start()
    {
        pause = GameObject.Find("btn_Pause").GetComponent<Button>();
        //��ͣ��ť����
        pause.onClick.AddListener(() => ShowMenu());
        tip = GameObject.Find("Tip").GetComponent<Tip>();
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        if (isTipShow)
        {
            //������ʾ���������
            //�õ��������λ��
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out position);
            tip.SetPosition(position + tipPosOffset);
        }
    }
    public void ShowMenu()
    {
        pause.enabled = false;
        //��ͣ��Ϸ
        Time.timeScale = 0;
        //ʵ������ͣ�˵�
        GameObject obj = Instantiate(Resources.Load<GameObject>("pal_Menu"));
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        obj.name = "pal_Menu";
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.SetAsFirstSibling();
        //������ť
        contin = GameObject.Find("btn_Continue").GetComponent<Button>();
        contin.onClick.AddListener(() => Contin());
        //�浵��ť
        save = GameObject.Find("btn_Save").GetComponent<Button>();
        save.onClick.AddListener(() => Save());
        //���ذ�ť
        back = GameObject.Find("btn_Back").GetComponent<Button>();
        back.onClick.AddListener(() => Back());
        //�˳���ť
        exit = GameObject.Find("btn_Exit").GetComponent<Button>();
        exit.onClick.AddListener(() => Exit());

    }
    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void Contin()
    {
        pause.enabled = true;
        Time.timeScale = 1;
        Destroy(GameObject.Find("pal_Menu"));
    }
    /// <summary>
    /// �浵
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
    /// ����
    /// </summary>
    public void Back()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Save();
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR//�ڱ༭��ģʽ�˳�
        UnityEditor.EditorApplication.isPlaying = false;
#else//�������˳�
        Application.Quit();
#endif
    }
    /// <summary>
    /// ��ʾ
    /// </summary>
    /// <param name="text">��������</param>
    public void ShowTip(string text)
    {
        isTipShow = true;
        tip.Show(text);
    }
    /// <summary>
    /// ����
    /// </summary>
    public void HideTip()
    {
        isTipShow = false;
        tip.Hide();
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        string str = "";
        switch (GetOverUI(canvas).name)
        {
            case "btn_Pause": str = "��ͣ"; break;
            case "btn_Continue": str = "������Ϸ"; break;
            case "btn_Save": str = "�浵"; break;
            case "btn_Back": str = "����"; break;
            case "btn_Exit": str = "�˳���Ϸ"; break;
        }
        if (str != "")
            ShowTip(str);
    }
    /// <summary>
    /// ����Ƴ�
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        HideTip();
    }
    /// <summary>
    /// ��ȡ���ͣ����UI
    /// </summary>
    /// <param name="canvas">����</param>
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
