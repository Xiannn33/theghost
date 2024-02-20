using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Defeat : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Restart").GetComponent<Button>().onClick.AddListener(()=>Restart());
        GameObject.Find("Back").GetComponent<Button>().onClick.AddListener(()=> Back());
        GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(()=> Exit());
    }
    
    void Update()
    {
        
    }
    public void Restart()
    {
        DataMgr.Instance.P.Health = 100;
        SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
    }
    public void Back()
    {
        DataMgr.Instance.P.Health = 100;
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
}
