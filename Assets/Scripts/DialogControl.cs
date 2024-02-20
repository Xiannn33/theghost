using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ������ʾ�������̵걳��
/// �������˰���R�򿪣��ٴΰ���R�ر�
/// </summary>
public class DialogControl : MonoBehaviour
{
    /// <summary>
    /// �ļ�λ��
    /// </summary>
    private string fileName;

    private void Start()
    {
        fileName = Application.dataPath + "/Resources/Data/playerInfo.dat";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //trader�����̵����
            if (transform.parent.name == "trader")
            {
                GameObject obj = GameObject.Find("Canvas").transform.Find("Shop").gameObject;
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    Time.timeScale = 0;
                    Shop.Instance.ShowCommodity();
                }
                else
                {
                    Time.timeScale = 1;
                    obj.SetActive(false);
                }
            }
            //sign����˵�����
            else if (transform.parent.name == "sign")
            {
                GameObject obj = GameObject.Find("GameMethod");
                if (obj == null)
                {
                    obj = Instantiate(Resources.Load<GameObject>("GameMethod"));
                    obj.transform.SetParent(GameObject.Find("Canvas").transform);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localScale = Vector3.one;
                    obj.name = "GameMethod";
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                    Destroy(obj);
                }
            }
            //adventurer���򿪹ؿ�ѡ�����
            else if (transform.parent.name == "adventurer")
            {
                List<PlayerData> data = DataMgr.Instance.Load();
                int id = PlayerPrefs.GetInt("Player");
                int level = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].ID == id)
                    {
                        level = data[i].Level;
                        break;
                    }
                }
                GameObject obj = GameObject.Find("pal_Level");
                if (obj == null)
                {
                    obj = Instantiate(Resources.Load<GameObject>("pal_Level"));
                    obj.transform.SetParent(GameObject.Find("Canvas").transform);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localScale = Vector3.one;
                    obj.name = "pal_Level";
                    for (int i = 1; i <= 4; i++)
                    {
                        obj.transform.GetChild(i - 1).GetComponent<Button>().onClick.AddListener(() => SelectLevel());
                        if (i > level)
                            obj.transform.GetChild(i - 1).GetComponent<Button>().interactable = false;
                    }
                }
                else
                {
                    Destroy(obj);
                }
            }
            //ͨ�ش���
            else if (transform.parent.name == "Door")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    public void SelectLevel()
    {
        var btn = EventSystem.current.currentSelectedGameObject;
        SceneManager.LoadScene(btn.name);
    }
}
