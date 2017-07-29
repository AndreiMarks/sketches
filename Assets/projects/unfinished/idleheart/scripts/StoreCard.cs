using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCard : MonoBehaviour 
{
    public Image iconBackground;
    public Image icon;
    public Text itemText;
    public Text priceText;

    private StoreHandler.ItemInfo _itemInfo;

    public void Initialize( StoreHandler.ItemInfo itemInfo )
    {
        _itemInfo = itemInfo;    

        iconBackground.color = _itemInfo.itemColor;
        icon.sprite = _itemInfo.itemSprite;
        itemText.text = _itemInfo.itemName;
        priceText.text = _itemInfo.itemPrice.ToString();
    }
}
