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
}
