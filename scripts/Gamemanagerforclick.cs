using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanagerforclick : MonoBehaviour
{
    public GameObject jieshao;
    // Start is called before the first frame update
    public Button  readmore, start,exit,close1;
    private void Awake()

    {


        start.onClick.AddListener(() =>
            {

                SceneManager.LoadScene(1);//加载开始游戏场景；

            });

        exit.onClick.AddListener(() =>
        {
            Application.Quit();//退出游戏
        });

        readmore.onClick.AddListener(() =>
        {
            Showmore();
        });

        close1.onClick.AddListener(() =>
        {
            jieshao.SetActive(false);
        });
    }

    public void Showmore()
    {
        jieshao.SetActive(true);
    }

}
