using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject xIcon;
    public GameObject itemsShop;
    public Text CoinsText;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void ShowXIconHint() {
        xIcon.SetActive(true);
    }
    public void HideXIconHint()
    {
        xIcon.SetActive(false);
    }public void ShowitemsShop() {
        itemsShop.SetActive(true);
    }
    public void HideitemsShop()
    {
        itemsShop.SetActive(false);
    }

    public void setCoinsText(int coins) {
        CoinsText.text = coins.ToString();
    }

}
