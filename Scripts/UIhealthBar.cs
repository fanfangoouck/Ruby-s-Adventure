using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhealthBar : MonoBehaviour

{
    //set 属性是私有属性，不能够从脚本外部进行更改。
    public static UIhealthBar instance { get; private set; }

    public Image mask;

    public float originalSize;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    public void Setvalue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,originalSize * value);
    }
}
