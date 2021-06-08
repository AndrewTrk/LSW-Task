using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName ="ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int coins;
    public Sprite icon;
}
