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
    /// ����R
    /// </summary>
    public Animator Anim;
    /// <summary>
    /// Ŀ��㼶
    /// </summary>
    public LayerMask TargetLayer;
    /// <summary>
    /// ����Բ��
    /// </summary>
    public Transform AttackPoint;
    /// <summary>
    /// ������Χ
    /// </summary>
    public float AttackArea;
    /// <summary>
    /// Ѳ�ߵ�
    /// </summary>
    public Transform[] PatrolPoints;
    /// <summary>
    /// ׷����
    /// </summary>
    public Transform[] ChasePoints;

    public Transform Target;
}
/// <summary>
/// ����״̬��
/// </summary>
public class FSM : MonoBehaviour
{
    /// <summary>
    ///����Ѫ��
    /// </summary>

    private float flashTime;
    private double health;
    /// <summary>
    /// ��Ѫ��Ч
    /// </summary>
    private GameObject bloodEffect;
    /// <summary>
    /// ���˾���ͼ
    /// </summary>
    private SpriteRenderer sr;
    /// <summary>
    /// ��ʼ��ɫ
    /// </summary>
    private Color originalColor;
    /// <summary>
    /// ״̬
    /// </summary>
    private ISate currentState;
    
    /// <summary>
    /// ����
    /// </summary>
    public Paramter Param;
    /// <summary>
    /// ״̬
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
            //ͨ���Զ��浵
            GameObject.Find("Player").GetComponentInChildren<Player>().Success();
            Destroy(gameObject);
        }
    }
    public void TransitionState(StateType type)
    {
        //�˳�ǰһ��״̬
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

