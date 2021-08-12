using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Uicontroller : MonoBehaviour
{

    public GameObject gameover,gameover1;
    public Button exit;/// <summary>
                       /// //返回上一级按钮
                       /// </summary>
    public static Uicontroller instance { get; private set; }

    private void Awake()
    {
        instance = this;
        Restartgame();
        exit.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);//加载开始游戏场景；
        });
    }
    private void  Restartgame()
    {
        gameover.transform.Find("playagain").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);//重新加载场景
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//两种调用场景的方法

        });


        gameover.transform.Find("backtomean").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);//重新加载主菜单
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//两种调用场景的方法

        });

    }
    // Start is called before the first frame update


    // Update is called once per frame


    public void showgameover()
    {
        gameover.SetActive(true);
    }
    public void showgameover1()
    {
        gameover1.SetActive(true);
        Time.timeScale = 0; //游戏结束，画面静止
    }


    

}
