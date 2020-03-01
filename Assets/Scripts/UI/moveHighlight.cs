using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveHighlight : MonoBehaviour
{
    RectTransform UIContainer;
    Vector3 startingPosition;

    public int index;

    private const int INDEX_CEILING = 6;
    public const float X_TRANS_INTERVAL = 3.85f;

    private bool buttonIsBeingPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        UIContainer = GameObject.Find("UI").GetComponent<RectTransform>();
        startingPosition = new Vector3(0f + UIContainer.position.x, 0f + UIContainer.position.y, 0f + UIContainer.position.z);
        transform.position = startingPosition;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cycle"))
        {
            index++;
            index = index % INDEX_CEILING;
            Debug.Log(index);
            buttonIsBeingPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (buttonIsBeingPressed == true)
        {
            Debug.Log("updating ui...");
            UpdatePosition();
            buttonIsBeingPressed = false;
        }
        //transform.Translate(0f, -0.001f, 0f);
        // translate 1
    }

    private void UpdatePosition()
    {
        switch (index)
        {
            case 0:
                Debug.Log("item 0");
                transform.Translate(-X_TRANS_INTERVAL * (INDEX_CEILING - 1), 0f, 0f);
                break;
            case 1:
                Debug.Log("item 1");
                transform.Translate(X_TRANS_INTERVAL, 0f, 0f);
                break;
            case 2:
                Debug.Log("item 2");
                transform.Translate(X_TRANS_INTERVAL, 0f, 0f);
                break;
            case 3:
                Debug.Log("item 3");
                transform.Translate(X_TRANS_INTERVAL, 0f, 0f);
                break;
            case 4:
                Debug.Log("item 4");
                transform.Translate(X_TRANS_INTERVAL, 0f, 0f);
                break;
            case 5:
                Debug.Log("item 5");
                transform.Translate(X_TRANS_INTERVAL, 0f, 0f);
                break;
        }
    }
}
