using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcdialog : MonoBehaviour
{
    public GameObject dialogbox;

    public float displaytime=10f;
    private float timerdisplay;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        dialogbox.SetActive(false);
        timerdisplay = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerdisplay>=0)
        {
            timerdisplay -= Time.deltaTime;
            if(timerdisplay<0)
            {
                dialogbox.SetActive(false);
            }
        }
    }

    public void displaydialog()
    {
        timerdisplay = displaytime;
        dialogbox.SetActive(true);
        UIHealth.instance.hastask=true;
        if(UIHealth.instance.fix==20)//已经完成任务
        {
            //修改对话框
            text.text = "哦可爱的Ruby，呱呱呱呱。（蛙语：十分感谢！时候不早了，到我家来睡觉吧   游戏结束,恭喜闯关成功";
            Uicontroller.instance.showgameover1();
        }

    }

}
