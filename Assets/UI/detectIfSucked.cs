using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectIfSucked : MonoBehaviour
{
    public bool obj0Sucked;
    public bool obj1Sucked;
    public bool obj2Sucked;
    public bool obj3Sucked;
    public bool obj4Sucked;
    public bool obj5Sucked;

    private Image obj0;
    private Image obj1;
    private Image obj2;
    private Image obj3;
    private Image obj4;
    private Image obj5;

    // Start is called before the first frame update
    void Start()
    {
        obj0Sucked = false;
        obj1Sucked = false;
        obj2Sucked = false;
        obj3Sucked = false;
        obj4Sucked = false;
        obj5Sucked = false;

        obj0 = GameObject.Find("obj 0").GetComponent<Image>();
        obj1 = GameObject.Find("obj 1").GetComponent<Image>();
        obj2 = GameObject.Find("obj 2").GetComponent<Image>();
        obj3 = GameObject.Find("obj 3").GetComponent<Image>();
        obj4 = GameObject.Find("obj 4").GetComponent<Image>();
        obj5 = GameObject.Find("obj 5").GetComponent<Image>();

        var startColor = obj0.color;
        startColor.a = 0.25f;
        obj0.color = startColor;
        obj1.color = startColor;
        obj2.color = startColor;
        obj3.color = startColor;
        obj4.color = startColor;
        obj5.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var currentColor = obj0.color;
        if(obj0Sucked == true)
        {
            currentColor.a = 1f;
        } else
        {
            currentColor.a = 0.25f;
        }
        obj0.color = currentColor;

        if (obj1Sucked == true)
        {
            currentColor.a = 1f;
        }
        else
        {
            currentColor.a = 0.25f;
        }
        obj1.color = currentColor;

        if (obj2Sucked == true)
        {
            currentColor.a = 1f;
        }
        else
        {
            currentColor.a = 0.25f;
        }
        obj2.color = currentColor;

        if (obj3Sucked == true)
        {
            currentColor.a = 1f;
        }
        else
        {
            currentColor.a = 0.25f;
        }
        obj3.color = currentColor;

        if (obj4Sucked == true)
        {
            currentColor.a = 1f;
        }
        else
        {
            currentColor.a = 0.25f;
        }
        obj4.color = currentColor;

        if (obj5Sucked == true)
        {
            currentColor.a = 1f;
        }
        else
        {
            currentColor.a = 0.25f;
        }
        obj5.color = currentColor;
    }
}
