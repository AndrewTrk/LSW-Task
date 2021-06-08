using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private List<ShopItem> CollectedItems;
    [SerializeField] private GameObject hat1;
    [SerializeField] private GameObject hat2;
    [SerializeField] private GameObject hat3;
    [SerializeField] private GameObject hat4;
    [SerializeField] private int initalCoins = 100;

    public int Coins { get; set; }

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Coins = initalCoins;
    }
    public List<ShopItem> GetIventoryItems()
    {
        return CollectedItems;
    }

    public void CollectItem(ShopItem boughtItem)
    {
        CollectedItems.Add(boughtItem);
        switch (boughtItem.itemName)
        {
            case "Cap":
                hat3.SetActive(true);
                break;
            case "Cowboy Hat":
                hat4.SetActive(true);
                break;
            case "Pirate's Hat":
                hat1.SetActive(true);
                break;
            case "Yellow Helmet":
                hat2.SetActive(true);
                break;
        }
    }





}
