using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{

    public GameObject panelDialog;
    public TMP_Text dialog;
    public string[] message;
    public bool dialogStart;
    public bool dialogFirst;
    public int friend = 0;
    // Start is called before the first frame update
    void Start()
    {
        message[0] = "Hi. Are you offering me an alliance?";
        message[1] = "OK.";
        message[2] = "Go away!";
        message[3] = "Good bye!";
        message[4] = "We are already friends.";
        dialogStart = false;
        dialogFirst = true;
        panelDialog.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            dialogStart = true;
            panelDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        panelDialog.SetActive(false);
        dialogStart = false;
        dialogFirst = false;
    }
    // Update is called once per frame
    private void Update()
    {
        if(dialogStart == true)
        {
            if (dialogFirst == true && friend == 0)
            {
                dialog.text = message[0];
                if (Input.GetKeyDown(KeyCode.Y) && friend == 0)
                {
                    dialog.text = message[1];
                    //GameObject.FindGameObjectWithTag("Purple").isFriend = true;
                    friend = 1;
                }
                if (Input.GetKeyDown(KeyCode.N) && friend == 0)
                {
                    dialog.text = message[3];
                    friend = 2;
                }
            }
            else
            {
                if(friend == 1)
                {
                    dialog.text = message[4];
                }
                else{      
                    dialog.text = message[2];
                }
            }
        }
    }
}
