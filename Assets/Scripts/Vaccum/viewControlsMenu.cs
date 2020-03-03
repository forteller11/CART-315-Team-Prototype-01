using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class viewControlsMenu : MonoBehaviour
{
    private Text menuInstructionText;
    private GameObject menuOverlay;
    private bool menuToggled;

    // Start is called before the first frame update
    void Start()
    {
        menuInstructionText = GameObject.Find("Menu Instruction Text").GetComponent<Text>();
        menuOverlay = GameObject.Find("Menu Overlay");
        menuToggled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("View Menu"))
        {
            menuToggled = !menuToggled;
        }
    }

    private void FixedUpdate()
    {
        if (menuToggled == false)
        {
            menuOverlay.SetActive(false);
            menuInstructionText.text = "[Start] / [Esc] - Controls ";

        } else
        {
            menuOverlay.SetActive(true);
            menuInstructionText.text = "[Start] / [Esc] - Return to game ";
        }
    }
}
