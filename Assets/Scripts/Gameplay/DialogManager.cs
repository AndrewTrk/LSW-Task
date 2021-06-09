using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogueText;
    public float letterPerSec;
    public Image spaceIcon;
    public RectTransform enterIcon;


    private int currentLine=0;
    private bool isTyping = false;
    Dialog dialog;
    public static DialogManager Instance { get; private set; }

    public event Action onDialogshown;  
    public event Action<int> onDialogDismissed; 
    
    private void Awake()
    {
        Instance = this;
    }
    public void ShowDialog(Dialog dialog)
    {
        currentLine = 0;
        //broadcast dialog shown event
        onDialogshown?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        if (!isTyping)
        {
            StartCoroutine(TypeAnimation(dialog.Lines[currentLine] , currentLine));
        }
    }

    private void Update()
    {
        //start animating the text in the Next Line of the dialog when space key is pressed
        if (Input.GetKey(KeyCode.Space) && dialog!=null) {
            spaceIcon.gameObject.SetActive(false);
            if (!isTyping)
            {
                if (currentLine < dialog.Lines.Count)
                {
                    StartCoroutine(TypeAnimation(dialog.Lines[currentLine],currentLine));
                }
                else {
                    dialogBox.SetActive(false);
                    enterIcon.gameObject.SetActive(false);
                    onDialogDismissed?.Invoke(0);
                }
                
                
                }
        }

        if (Input.GetKey(KeyCode.Return) && dialog != null && !isTyping)
        {
            if (currentLine == dialog.Lines.Count)
            {
                currentLine = 0;
                //enterIcon.gameObject.SetActive(true);
                dialogBox.SetActive(false);
                enterIcon.gameObject.SetActive(false);
                onDialogDismissed?.Invoke(1);
            }
 
        }
    }

    private IEnumerator TypeAnimation(string line, int linenumber)
    {
        //add letter by letter to the dialog text box
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSec);
        }
        spaceIcon.gameObject.SetActive(true);
        //Debug.Log("Animated in Line" + currentLine);
        if (linenumber == 2) {
            enterIcon.gameObject.SetActive(true);
        }
        currentLine++;
        isTyping = false;
    }

    
}
