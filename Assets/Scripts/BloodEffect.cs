using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private float timeDestroy=1.0f;
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }
    

    void Update()
    {
        
    }
}
