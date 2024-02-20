using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBladeHit : MonoBehaviour
{
    private GameObject windBlade;
    void Start()
    {
        windBlade = Resources.Load<GameObject>("WindBlade");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(windBlade, transform.position, transform.rotation);
    }
}
