using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("与我们发生碰撞的对象是："+collision);
        RubyController rubyController = collision.GetComponent<RubyController>();
        //当前发生触发检测的游戏物体对象身上有否有RubyController脚本
        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }
}