using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit_Player : MonoBehaviour
{
    private GameObject ball;
    void Start()
    {
        ball = Resources.Load<GameObject>("Ball_Player");
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
        GameObject obj = Instantiate(ball);
        obj.transform.position = transform.position;
    }
}
