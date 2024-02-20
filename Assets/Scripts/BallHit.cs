using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{
    private GameObject ball;
    private bool isAttack = false;
    private float attackTime = 1.0f;
    void Start()
    {
        ball = Resources.Load<GameObject>("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo currentStateInfo = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName("enemy_attack"))
        {
            if (!isAttack)
            {
                isAttack = true;
                Shoot();
                attackTime = 0f;
            }
            if (isAttack)
            {
                attackTime += Time.deltaTime;
            }
            if (attackTime >= 1.0f)
            {
                isAttack = false;
            }
        }
    }

    public void Shoot()
    {
        GameObject obj = Instantiate(ball);
        obj.transform.position = transform.position;
    }
}
