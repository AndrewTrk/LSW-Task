using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManger : MonoBehaviour
{
    [SerializeField] private Transform itemTemplate;
    [SerializeField] private ShopItem[] shopItems;
    [SerializeField] private GameObject Player;
    [SerializeField] private Image previewImage;


    float shopItemTemplateHeight = 100;
    float shopItemVerticalSpacing = 25;
    private void Awake()
    {
        //itemTemplate = itemTemplate.Find("ItemImage");
    }
    private void Start()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            createShopItem(shopItems[i], shopItems[i].icon, shopItems[i].itemName, shopItems[i].coins, i);
        }
    }

    void createShopItem(ShopItem shopItem, Sprite iconImage, string itemName, int coinText, int position)
    {
        Transform shopItemTemplate = Instantiate(itemTemplate, transform);

        shopItemTemplate.GetComponent<RectTransform>().anchoredPosition = new Vector2(itemTemplate.position.x, (shopItemTemplateHeight + shopItemVerticalSpacing) * position);
        shopItemTemplate.GetChild(0).Find("ItemImage").GetComponent<Image>().sprite = iconImage;
        shopItemTemplate.GetChild(0).Find("ItemName").GetComponent<Text>().text = itemName;
        shopItemTemplate.GetChild(0).Find("ItemPrice").GetComponent<Text>().text = coinText.ToString();
        shopItemTemplate.GetComponent<Button>().onClick.AddListener(() => TryBuy(shopItem));

    }

    void TryBuy(ShopItem item)
    {
        if (InventoryManager.Instance.Coins >= item.coins)
        {
            if (InventoryManager.Instance.GetIventoryItems().Contains(item))
            {
                previewImage.GetComponent<Image>().sprite = item.icon;
                previewImage.gameObject.SetActive(true);
                Player.GetComponent<PlayerController>().equip(item);
                previewImage.GetComponent<Image>().sprite = item.icon;
                previewImage.gameObject.SetActive(true);
                Debug.Log("You already Have This Item in your Bag " + item.name);
                return;
            }

            Debug.Log("Equibed " + item.name);

            InventoryManager.Instance.Coins -= item.coins;
            InventoryManager.Instance.CollectItem(item);
            UIManager.Instance.setCoinsText(InventoryManager.Instance.Coins);
            previewImage.GetComponent<Image>().sprite = item.icon;
            previewImage.gameObject.SetActive(true);
            Player.GetComponent<PlayerController>().equip(item);
        }
        else
        {
            Debug.Log("You don't have enought money " + item.name);

        }


    }

    public void SellItem(ShopItem item)
    {
        previewImage.GetComponent<Image>().sprite = null;
        previewImage.gameObject.SetActive(false);

    }
}
