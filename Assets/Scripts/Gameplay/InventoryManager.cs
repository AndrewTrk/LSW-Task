using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private ShopManger shopManager;
    [SerializeField] private List<ShopItem> CollectedItems;
    [SerializeField] private GameObject hat1;
    [SerializeField] private GameObject hat2;
    [SerializeField] private GameObject hat3;
    [SerializeField] private GameObject hat4;
    //[SerializeField] private GameObject inventoryBag;
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
    
    public void RemoveItem(ShopItem soldItem)
    {
        shopManager.SellItem(soldItem);
        Coins += soldItem.coins;
        UIManager.Instance.setCoinsText(Coins);
        Player.GetComponent<PlayerController>().unequip(soldItem);
        CollectedItems.Remove(soldItem);
        switch (soldItem.itemName)
        {
            case "Cap":
                hat3.SetActive(false);
                break;
            case "Cowboy Hat":
                hat4.SetActive(false);
                break;
            case "Pirate's Hat":
                hat1.SetActive(false);
                break;
            case "Yellow Helmet":
                hat2.SetActive(false);
                break;
        }
    }





}
