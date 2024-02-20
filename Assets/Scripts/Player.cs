using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    AnimatorStateInfo stateInfo;
    private State state;
    private Transform groundCheck;
    private bool isGround, isJump;
    private bool jumpPressed;

    public AudioSource jumpAudio;
    public AudioSource attackAudio;
    public AudioSource collAudio;

    /// <summary>
    /// 可以连续跳跃的次数
    /// </summary>
    private int jumpNumber = 2;
    /// <summary>
    /// 金币数
    /// </summary>
    private int money;
    private Text txtMoney;
    /// <summary>
    /// 攻击力
    /// </summary>
    private int damage = 10;
    /// <summary>
    /// 物品生效时间
    /// </summary>
    private float time;
    /// <summary>
    /// 角色血量
    /// </summary>
    private float health;

    private Image blood1;
    private Image blood2;
    private Image blood3;
    private Image blood4;

    public LayerMask ground;

    private PlayerData p;

    List<Item> list = new List<Item>();

    private GameObject obj;

    private string enemyName;

    private Transform target;

    private Renderer rend;

    private bool isInvincible = false;

    private float invincibleTime;

    private bool isAttack = false;

    private float attackTime;

    private int level;

    void Start()
    {
        Packsack.Instance.SetPacksack();
        p = DataMgr.Instance.GetData();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = new State(anim);
        groundCheck = transform.Find("GroundCheck");
        ground = 1 << 7;
        money = p.Money;
        level = p.Level;
        txtMoney = GameObject.Find("Canvas").transform.Find("Packsack/Money").GetComponentInChildren<Text>();
        health = p.Health;
        blood1 = GameObject.Find("Image1").GetComponent<Image>();
        blood2 = GameObject.Find("Image2").GetComponent<Image>();
        blood3 = GameObject.Find("Image3").GetComponent<Image>();
        blood4 = GameObject.Find("Image4").GetComponent<Image>();
        foreach (var item in p.ItemList)
        {
            list.Add(item);
        }
        if (list.Count != 0)
        {
            foreach (var item in list)
            {
                Packsack.Instance.StoreItem(item);
            }
        }
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerAttack();
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
        Attach();
        Attacked();
        BloodControl();

        if (isAttack)
        {
            attackTime += Time.deltaTime;
        }

        if (health <= 0.1f)
        {
            PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
            Invoke("Defeat", 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpNumber > 0)
        {
            jumpPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowPacksack();
            txtMoney.text = money.ToString();
        }
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            damage = 10;
        }
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        PlayerMove();
        PlayerJump();
    }
    /// <summary>
    /// 通关
    /// </summary>
    public void Success()
    {
        string name = SceneManager.GetActiveScene().name;
        string numberString = Regex.Replace(name, @"[^0-9]+", "");
        int numberInt = int.Parse(numberString);
        level = numberInt + 1;
        GameObject.Find("Canvas").GetComponent<MenuController>().Save();
    }
    /// <summary>
    /// 附身模块
    /// </summary>
    public void Attach()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //附身
            if (PlayerPrefs.GetString("Role").Equals("Ghost"))
            {
                if (obj != null && obj.GetComponent<FSM>().GetHealth() <= 10.0f)
                {
                    //transform.parent.position = target.position;
                    transform.parent.position = transform.position;
                    Destroy(GetComponentInParent<InputHandel>().Go);
                    Destroy(GameObject.Find(enemyName));
                    PlayerPrefs.SetString("Role", enemyName + "_Player");
                    //实例化
                    GameObject obj = Instantiate(Resources.Load<GameObject>(PlayerPrefs.GetString("Role")));
                    obj.name = PlayerPrefs.GetString("Role");
                    obj.transform.position = GameObject.Find("Player").transform.position;
                    obj.transform.SetParent(GameObject.Find("Player").transform);
                    GetComponentInParent<InputHandel>().Go = obj;

                    GameObject obj1 = Instantiate(Resources.Load<GameObject>("Door"));
                    obj1.transform.position = GameObject.Find("Player").transform.position;
                    obj1.name = "Door";
                    Save();
                    //通关自动存档
                    Success();
                }
            }
            //取消附身
            else
            {
                transform.parent.position = transform.position;
                Destroy(transform.gameObject);
                PlayerPrefs.SetString("Role", "Ghost");
                //实例化
                GameObject obj = Instantiate(Resources.Load<GameObject>(PlayerPrefs.GetString("Role")));
                obj.name = PlayerPrefs.GetString("Role");
                obj.transform.position = GameObject.Find("Player").transform.position;
                obj.transform.SetParent(GameObject.Find("Player").transform);
                GetComponentInParent<InputHandel>().Go = obj;
                Save();
            }
        }
    }
    /// <summary>
    /// 血条
    /// </summary>
    public void BloodControl()
    {
        if (health >= 75f)
        {
            blood1.fillAmount = (health - 75f) / 25f;
            blood2.fillAmount = 1;
            blood3.fillAmount = 1;
            blood4.fillAmount = 1;
        }
        else if (health >= 50f)
        {
            blood1.fillAmount = 0;
            blood2.fillAmount = (health - 50f) / 25f;
            blood3.fillAmount = 1;
            blood4.fillAmount = 1;
        }
        else if (health >= 25f)
        {
            blood1.fillAmount = 0;
            blood2.fillAmount = 0;
            blood3.fillAmount = (health - 25f) / 25f;
            blood4.fillAmount = 1;
        }
        else if (health <= 25f && health >= 0)
        {
            blood1.fillAmount = 0;
            blood2.fillAmount = 0;
            blood3.fillAmount = 0;
            blood4.fillAmount = (health) / 25f;
        }
        else
        {
            blood1.fillAmount = 0;
            blood2.fillAmount = 0;
            blood3.fillAmount = 0;
            blood4.fillAmount = 0;
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    public void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(horizontal * 5, rb.velocity.y, 0);
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
            state.ChangeState(State.PlayerAnimState.walk);
        }
        else
            state.ChangeState(State.PlayerAnimState.idle);
    }
    /// <summary>
    /// 跳跃
    /// </summary>
    public void PlayerJump()
    {
        if (rb.velocity.y < 0)
        {
            state.ChangeState(State.PlayerAnimState.fall);
        }
        if (isGround)
        {
            jumpNumber = 2;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            jumpAudio.Play();
            isJump = true;
            rb.velocity = new Vector3(rb.velocity.x, 10, 0);
            state.ChangeState(State.PlayerAnimState.jump);
            jumpNumber--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpNumber > 0 && isJump)
        {
            jumpAudio.Play();
            rb.velocity = new Vector3(rb.velocity.x, 10, 0);
            state.ChangeState(State.PlayerAnimState.jump);
            jumpNumber--;
            jumpPressed = false;
        }
    }
    /// <summary>
    /// 打开背包
    /// </summary>
    public void ShowPacksack()
    {
        //打开背包面板
        GameObject obj = GameObject.Find("Canvas").transform.Find("Packsack").gameObject;
        if (!obj.activeSelf)
        {
            obj.SetActive(true);
            txtMoney.text = money.ToString();
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            obj.SetActive(false);
        }
    }
    public void UseItem(Item item)
    {
        int flag = 0;
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name.Equals(item.Name))
                {
                    flag = i;
                }
            }
            list.RemoveAt(flag);
        }
    }
    /// <summary>
    /// 触发器
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集金币
        if (collision.tag == "Money")
        {
            collAudio.Play();
            Destroy(collision.gameObject);
            money += 1;
        }
        //收集物品
        else if (collision.tag == "Items")
        {
            collAudio.Play();
            Destroy(collision.gameObject);
            //string name = Regex.Replace(collision.name.Trim(), "[(][^)]*[)]",string.Empty);
            Item item = InventoryMgr.Instance.GetItemById(GetID(collision.name));
            Packsack.Instance.StoreItem(item);
            list.Add(item);
        }
        //附身
        else if (collision.CompareTag("Attach") && collision.gameObject != GetComponentInParent<InputHandel>().Go)
        {
            obj = collision.transform.parent.gameObject;
            enemyName = obj.name;
            target = obj.transform;
        }
        //死亡
        else if (collision.tag == "DeadLine")
        {
            PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
            Invoke("Defeat", 0.1f);
        }
    }
    public void Defeat()
    {
        SceneManager.LoadScene("Defeat");
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            attackAudio.Play();
            state.ChangeState(State.PlayerAnimState.attack);
        }
        if (PlayerPrefs.GetString("Role") == "Slime_Player")
        {
            if (GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                AnimatorStateInfo currentStateInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Enemy").transform.position) <= 0.1f && currentStateInfo.IsName("enemy_attack"))
                {
                    if (!isAttack)
                    {
                        isAttack = true;
                        GameObject.FindGameObjectWithTag("Enemy").transform.GetComponent<FSM>().TakeDamage(damage);
                        attackTime = 0f;
                    }

                    if (attackTime >= 1.0f)
                    {
                        isAttack = false;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 受到攻击
    /// </summary>
    public void Attacked()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            AnimatorStateInfo currentStateInfo = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (currentStateInfo.IsName("enemy_attack"))
            {
                if (!isInvincible)
                {
                    Blink(5, 0.1f);
                    isInvincible = true;
                    health -= 10f;
                    invincibleTime = 0f;
                }
                if (isInvincible)
                {
                    invincibleTime += Time.deltaTime;
                }
                if (invincibleTime >= 1.0f)
                {
                    isInvincible = false;
                }
            }
        }
    }
    /// <summary>
    /// 玩家受伤闪烁效果
    /// </summary>
    /// <param name="numBlinks">闪烁次数</param>
    /// <param name="second">闪烁时间</param>
    public void Blink(int numBlinks, float second)
    {
        StartCoroutine(DoBlink(numBlinks, second));
    }

    IEnumerator DoBlink(int numBlinks, float second)
    {
        for (int i = 0; i < numBlinks; i++)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(second);
        }
        rend.enabled = true;
    }
    /// <summary>
    /// 得到收集品的ID
    /// </summary>
    /// <param name="name">收集品的名字</param>
    /// <returns>收集品的ID</returns>
    public int GetID(string name)
    {
        if (name.Contains("Starfruit"))
        {
            return 4;
        }
        else if (name.Contains("Honey"))
        {
            return 5;
        }
        else if (name.Contains("Blueberry"))
        {
            return 6;
        }
        else if (name.Contains("Melon"))
        {
            return 7;
        }
        return 0;
    }
    /// <summary>
    /// 购买物品
    /// </summary>
    public void PlayerBuy()
    {
        money = p.Money;
    }
    /// <summary>
    /// 保存数据
    /// </summary>
    /// <returns>玩家数据</returns>
    public PlayerData Save()
    {
        p.Health = health;
        p.Money = money;
        p.ItemList = list;
        p.Level = level;
        DataMgr.Instance.P = p;
        return DataMgr.Instance.P;
    }
    /// <summary>
    /// 增加血量
    /// </summary>
    /// <param name="hp">血量</param>
    public void HealthAddition(int hp)
    {
        if (health < 100)
        {
            health = health + hp;
            if (health > 100)
                health = 100;
        }
    }
    /// <summary>
    /// 增加攻击力
    /// </summary>
    /// <param name="agg">攻击力</param>
    public void AggressivityAddition(int agg)
    {
        time = 2f;
        if (damage < 10)
        {
            damage = damage + agg;
            if (damage > 10)
                damage = 10;
        }
    }
    public PlayerData GetPlayer()
    {
        return p;
    }
}
