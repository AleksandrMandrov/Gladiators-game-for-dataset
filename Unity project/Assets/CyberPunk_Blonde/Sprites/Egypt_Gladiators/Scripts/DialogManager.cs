using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public GameObject panelDialog;
    public Text dialog;
    public string[] message;

    // Start is called before the first frame update
    void Start()
    {
        message[0] = "Hi";
        message[1] = "Ok";
        panelDialog.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            panelDialog.SetActive(true);
            dialog.text = message[0];
            if (Input.GetKeyDown(KeyCode.R))
            {
                dialog.text = message[1];
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
