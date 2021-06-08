using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject xIcon;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void showXIconHint() {
        xIcon.SetActive(true);
    }
    public void hideXIconHint()
    {
        xIcon.SetActive(false);
    }

}
