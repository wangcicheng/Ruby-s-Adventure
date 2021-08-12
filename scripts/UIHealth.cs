using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public Image mask;
    private float originalsize;

    

    public static UIHealth instance { get;private set; }
    public bool hastask;
    

    public int fix = 0;
    public int restlingjain;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        originalsize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame        
    
    public void Setvalue(float fillvalue)//设置当前ui血条显示
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,originalsize * fillvalue);//水平轴向、改变多少
    }
}
