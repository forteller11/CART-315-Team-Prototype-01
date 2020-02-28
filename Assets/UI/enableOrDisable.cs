using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enableOrDisable : MonoBehaviour
{
    public bool obj0Enabled;
    public bool obj1Enabled;
    public bool obj2Enabled;
    public bool obj3Enabled;
    public bool obj4Enabled;
    public bool obj5Enabled;

    private Image bg0;
    private Image bg1;
    private Image bg2;
    private Image bg3;
    private Image bg4;
    private Image bg5;

    GameObject highlight;
    moveHighlight highlightScript;

    // Start is called before the first frame update
    void Start()
    {
        obj0Enabled = false;
        obj1Enabled = false;
        obj2Enabled = false;
        obj3Enabled = false;
        obj4Enabled = false;
        obj5Enabled = false;

        bg0 = GameObject.Find("bg 0").GetComponent<Image>();
        bg1 = GameObject.Find("bg 1").GetComponent<Image>();
        bg2 = GameObject.Find("bg 2").GetComponent<Image>();
        bg3 = GameObject.Find("bg 3").GetComponent<Image>();
        bg4 = GameObject.Find("bg 4").GetComponent<Image>();
        bg5 = GameObject.Find("bg 5").GetComponent<Image>();

        highlight = GameObject.Find("Highlight");
        highlightScript = highlight.GetComponent<moveHighlight>();

        var startColor = bg0.color;
        startColor.a = 0f;
        bg0.color = startColor;
        bg1.color = startColor;
        bg2.color = startColor;
        bg3.color = startColor;
        bg4.color = startColor;
        bg5.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Enable or Disable"))
        {
            switch (highlightScript.index)
            {
                case 0:
                    obj0Enabled = !obj0Enabled;
                    break;
                case 1:
                    obj1Enabled = !obj1Enabled;
                    break;
                case 2:
                    obj2Enabled = !obj2Enabled;
                    break;
                case 3:
                    obj3Enabled = !obj3Enabled;
                    break;
                case 4:
                    obj4Enabled = !obj4Enabled;
                    break;
                case 5:
                    obj5Enabled = !obj5Enabled;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        var currentColor = bg0.color;
        if (obj0Enabled == true)
        {
            currentColor.a = 0.5f;
        }
        else
        {
            currentColor.a = 0f;
        }
        bg0.color = currentColor;

        if (obj1Enabled == true)
        {
            currentColor.a = 0.5f;
        }
        else
        {
            currentColor.a = 0f;
        }
        bg1.color = currentColor;

        if (obj2Enabled == true)
        {
            currentColor.a = 0.5f;
        }
        else
        {
            currentColor.a = 0f;
        }
        bg2.color = currentColor;

        if (obj3Enabled == true)
        {
            currentColor.a = 0.5f;
        }
        else
        {
            currentColor.a = 0f;
        }
        bg3.color = currentColor;

        if (obj4Enabled == true)
        {
            currentColor.a = 0.5f;
        }
        else
        {
            currentColor.a = 0f;
        }
        bg4.color = currentColor;

        if (obj5Enabled == true)
        {
            currentColor.a = 0.5f;
        }
        else
        {
            currentColor.a = 0f;
        }
        bg5.color = currentColor;
    }
}
