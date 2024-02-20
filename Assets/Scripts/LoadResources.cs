using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResources : MonoBehaviour
{

    void Start()
    {        
        GameObject obj = Instantiate(Resources.Load<GameObject>(PlayerPrefs.GetString("Role")));
        obj.name = PlayerPrefs.GetString("Role");
        obj.transform.SetParent(GameObject.Find("Player").transform);
        obj.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
