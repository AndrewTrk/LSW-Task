using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour , IInteractable
{
    public Dialog dialog;
    public void Interact()
    {
        DialogManager.Instance.ShowDialog(dialog);
    }

  
}
