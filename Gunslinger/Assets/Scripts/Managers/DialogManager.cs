using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;

    [HideInInspector]

    public bool ShowingDialog { get { return dialogBox.activeInHierarchy; } }


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogBox.activeInHierarchy)
        {
            if(Input.GetButtonUp("Fire1"))
            {
                currentLine++;

                if(currentLine >= dialogLines.Length)
                {
                    dialogBox.SetActive(false);
                    nameBox.SetActive(false);
                }
                else
                {
                    dialogText.text = dialogLines[currentLine];
                }
            }
        }
    }

    public void ShowDialog(string[] dialogLines)
    {
        this.dialogLines = dialogLines;
        currentLine = 0;
        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);
    }

    public void ShowDialog(string name, string[] dialogLines)
    {
        nameBox.SetActive(true);
        nameText.text = name;
        ShowDialog(dialogLines);
    }
}
