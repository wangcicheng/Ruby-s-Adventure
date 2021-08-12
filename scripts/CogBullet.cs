using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody2d;


    void Awake()//将update改为awake
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
       
    }
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction *force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("当前子弹碰撞到的游戏物体是：" + collision.gameObject);
        EnemyController enemyController =collision.gameObject.GetComponent<EnemyController>();//子弹碰到机器人
        if (enemyController != null)
        {
            enemyController.Fixed();
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        //if (transform.position.magnitude>2000)//销毁距离
        //{
          //  Destroy(gameObject);
        //}
    }

    // Update is called once per frame
    //void Update() 不需要update



}
