
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float speed=5;//Ruby的速度

    //Ruby生命值
    public int maxHealth = 5;//最大生命值
    private int currentHealth;//Ruby的当前生命值
    public int Health { get { return currentHealth; } }



    //rub无敌时间
    public float timeinvincible=1.5f;//无敌时间
    private bool isininvincible;//触发无敌
    private float invincibleTimer;//无敌计时器

    private Vector2 Lookdirection = new Vector2(0, -1);
    private Animator animator;


    
    // Start is called before the first frame update

    private AudioSource audioSource;

    public AudioClip playerhit;
    public AudioClip attacksound;

    public GameObject tips;
    public float tipsdisplaytime = 8f;
    private float Timerdisplay;
    public Text tipstext;
    public Text fixednum;
    public Text lingjianshu;

    public GameObject projectileprefab;

    private void Start()
    {
        
        tips.SetActive(false);
        Timerdisplay = -1;

    //Application.targetFrameRate = 30;
    rigidbody2d = GetComponent<Rigidbody2D>();
         currentHealth = maxHealth;
        //currentHealth = 1;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1; //游戏结束，画面静止

        UIHealth.instance.restlingjain = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        //玩家输入监听
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        UIHealth.instance.Setvalue((float)currentHealth / maxHealth);



        if (Health == 0)//ruby死亡
        {
            
            Uicontroller.instance.showgameover();
            //Time.timeScale = 0;  游戏结束，画面静止
            return;
        }

        //当前玩家输入轴向值不为0
        if (!Mathf.Approximately(move.x,0)||!Mathf.Approximately(move.y,0))
        {
            Lookdirection.Set(move.x, move.y);
            Lookdirection.Normalize();

        }

        animator.SetFloat("Look X",Lookdirection.x);
        animator.SetFloat("Look Y",Lookdirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        //当前玩家输入的某个轴向值不为0

        Vector2 position = transform.position;
        ////Ruby的水平方向移动
        position.x = position.x + speed * horizontal * Time.deltaTime;
        ////Ruby的垂直方向移动
        position.y = position.y + speed * vertical * Time.deltaTime;
        //Ruby位置的移动

        //transform.position = position;
        rigidbody2d.MovePosition(position);

        if(isininvincible)
        {
            invincibleTimer = invincibleTimer - Time.deltaTime;
            if (invincibleTimer <= 0)
                isininvincible = false;
        }


        if (Input.GetKeyDown(KeyCode.J))
        {

            Launch();
        }

        //Debug.Log("ruby 血量为" + Health);
    

        


        //检查是否与npc对话
        if(Input.GetKeyDown(KeyCode.H))
        {
            
            RaycastHit2D hit = Physics2D.Raycast
                (rigidbody2d.position+Vector2.up*0.2f,Lookdirection,1.5f,LayerMask.GetMask("npc"));
            if (hit.collider != null)
            {
                // Debug.Log("当前射线检测到的是青蛙");
                npcdialog npcdialog=hit.collider.GetComponent<npcdialog>();
                if (npcdialog != null)
                {
                    npcdialog.displaydialog();
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.T))
        {
            tipsplay();
        }
        if (Timerdisplay > 0)
        {

            Timerdisplay -= Time.deltaTime;
            if (Timerdisplay < 0)
            {
                tips.SetActive(false);
            }
        }

        fixednum.text = UIHealth.instance.fix + "/20";
        lingjianshu.text = UIHealth.instance.restlingjain +"";
    }

    public  void tipsplay()
    {
         Timerdisplay = tipsdisplaytime;

        

        if (UIHealth.instance.hastask == true & UIHealth.instance.fix < 20)
        {
            tipstext.text = "可爱的Ruby，答应我了就记得帮我把所有机器人修好哦！你已修理的个数是"+ UIHealth.instance.fix+"/20";
        }

        if(UIHealth.instance.hastask == true & UIHealth.instance.fix==20)
        {
            tipstext.text = "我在该去找青蛙先生领取奖励了，累死我了~~ ^_^~~" ;
        }
         tips.SetActive(true);

       
    }

    public void ChangeHealth(int amount)
    {
        if(amount<0)
        {
            if(isininvincible)
            {
                return;//无敌
            }
            isininvincible = true;
            invincibleTimer = timeinvincible;
            animator.SetTrigger("Hit");
            playsound(playerhit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
    }

    //public int GetRubyHealthValue()
    //{
    //    return currentHealth;
    //}
    //子弹发射
    private void Launch()
    {
        if (!UIHealth.instance.hastask)
        {
            return;
        }
        GameObject projectileObject = Instantiate(projectileprefab,rigidbody2d.position+Vector2.up*0.55f, Quaternion.identity);
        CogBullet projectile = projectileObject.GetComponent<CogBullet>();
        projectile.Launch(Lookdirection, 500);

        animator.SetTrigger("Launch");
        playsound(attacksound);
    }
    
    public void playsound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);

    }
}
