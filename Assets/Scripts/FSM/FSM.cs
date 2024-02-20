using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    idle, walk, patrol, chase, react,attack
}

[Serializable]
public class Paramter
{
    /// <summary>
    /// 动画R
    /// </summary>
    public Animator Anim;
    /// <summary>
    /// 目标层级
    /// </summary>
    public LayerMask TargetLayer;
    /// <summary>
    /// 攻击圆心
    /// </summary>
    public Transform AttackPoint;
    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackArea;
    /// <summary>
    /// 巡逻点
    /// </summary>
    public Transform[] PatrolPoints;
    /// <summary>
    /// 追击点
    /// </summary>
    public Transform[] ChasePoints;

    public Transform Target;
}
/// <summary>
/// 有限状态机
/// </summary>
public class FSM : MonoBehaviour
{
    /// <summary>
    ///敌人血量
    /// </summary>

    private float flashTime;
    private double health;
    /// <summary>
    /// 掉血特效
    /// </summary>
    private GameObject bloodEffect;
    /// <summary>
    /// 敌人精灵图
    /// </summary>
    private SpriteRenderer sr;
    /// <summary>
    /// 初始颜色
    /// </summary>
    private Color originalColor;
    /// <summary>
    /// 状态
    /// </summary>
    private ISate currentState;
    
    /// <summary>
    /// 参数
    /// </summary>
    public Paramter Param;
    /// <summary>
    /// 状态
    /// </summary>
    private Dictionary<StateType, ISate> states = new Dictionary<StateType, ISate>();
    void Start()
    {
        bloodEffect = Resources.Load<GameObject>("BloodEffect");
        flashTime = 0.4f;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        health = 100f;
        Param.Anim = GetComponentInChildren<Animator>();
        states.Add(StateType.idle, new IdleState(this));
        states.Add(StateType.walk, new WalkState(this));
        states.Add(StateType.patrol, new PatrolState(this));
        states.Add(StateType.chase, new ChaseState(this));
        states.Add(StateType.react, new ReactState(this));
        states.Add(StateType.attack, new AttackState(this));
        TransitionState(StateType.idle);
    }
    void Update()
    {
        currentState.OnUpdate();
        if (health <= 0.1f)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Door"));
            obj.transform.position = transform.position;
            obj.name = "Door";
            //通关自动存档
            GameObject.Find("Player").GetComponentInChildren<Player>().Success();
            Destroy(gameObject);
        }
    }
    public void TransitionState(StateType type)
    {
        //退出前一个状态
        if (currentState != null)
            currentState.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }
    public void FlipTo(Transform target)
    {
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Param.AttackPoint.position, Param.AttackArea);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Param.Target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Param.Target = null;
        }
    }
    public double GetHealth()
    {
        return health;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
        Instantiate(bloodEffect,transform);
    }
    private void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor",time);
    }
    private void ResetColor()
    {
        sr.color = originalColor;
    }
}

