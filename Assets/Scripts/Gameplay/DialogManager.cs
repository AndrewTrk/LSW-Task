using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogueText;
    public float letterPerSec;

    private bool isTyping = false;
    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void ShowDialog(Dialog dialog)
    {
        dialogBox.SetActive(true);
        if (!isTyping)
        {
            StartCoroutine(TypeAnimation(dialog.Lines[0]));
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
        isTyping = false;
    }
}
