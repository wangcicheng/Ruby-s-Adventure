using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rigidbody2d;

    public bool vertial;

    private int direction = 1;

    public float changetime = 3.0f;
    private float timer;

    private Animator animator;//控制转向


    private bool broken;//当前机器人是否损坏

    public ParticleSystem smokeeffect;//烟雾效果；

    //集中特效
    public GameObject hiteffect;
    private AudioSource AudioSource;
    public AudioClip fixedup;

    public AudioClip[] hitsound;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changetime;
        animator = GetComponent<Animator>();
        //animator.SetFloat("movex", direction);//direction 给movex赋值
        //animator.SetBool("vertical", !vertial);//direction
        playmoveanimition();
        broken = true;
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!broken)
        {

            return;//修理好则不再移动
        }


        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            //animator.SetFloat("movex",direction);//direction 给movex赋值
            playmoveanimition();
            timer = changetime;
        }
        Vector2 position = rigidbody2d.position;
        if (vertial)
        {

            position.x = position.x + Time.deltaTime * speed * direction;
        }
        else
        {

            position.y = position.y + Time.deltaTime * speed * direction;
        }

        rigidbody2d.MovePosition(position);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController rubyController = collision.gameObject.GetComponent<RubyController>();
        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);

            direction = -direction;
            //animator.SetFloat("movex", direction);//direction 给movex赋值
            playmoveanimition();
        }
        else
        {
            direction = -direction;
            playmoveanimition();
        }
    }

    private void playmoveanimition()
    {
        if (!vertial)
        {
            animator.SetFloat("movex", 0);
            animator.SetFloat("movey", direction);
        }
        else
        {
            animator.SetFloat("movex", direction);
            animator.SetFloat("movey", 0);
        }
    }

    public void Fixed()
    {
        Instantiate(hiteffect, transform.position, Quaternion.identity);
        //gameobject 位置  旋转角度
        broken = false;
        smokeeffect.Stop();
        rigidbody2d.simulated = false;
        animator.SetTrigger("fixed");
        int randomnum = Random.Range(0, 2);
        AudioSource.PlayOneShot(hitsound[randomnum]);
        AudioSource.PlayOneShot(fixedup);
        UIHealth.instance.fix+=1;

    }
}
