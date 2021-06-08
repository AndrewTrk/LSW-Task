using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogueText;
    public float letterPerSec;

    private int currentLine=0;
    private bool isTyping = false;
    Dialog dialog;
    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void ShowDialog(Dialog dialog)
    {
        this.dialog = dialog;
        dialogBox.SetActive(true);
        if (!isTyping)
        {
            StartCoroutine(TypeAnimation(dialog.Lines[currentLine]));
        }
    }

    private void Update()
    {
        //Next  Line
        if (Input.GetKey(KeyCode.Space)) {
            if (!isTyping)
            {
                if (currentLine < dialog.Lines.Count)
                {
                    StartCoroutine(TypeAnimation(dialog.Lines[currentLine]));
                }
                else {
                    Debug.Log("Send Dialog End Event");
                }
                }
        }
    }

    private IEnumerator TypeAnimation(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSec);
        }
        currentLine++;
        isTyping = false;
    }
}
