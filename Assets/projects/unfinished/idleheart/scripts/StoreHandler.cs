using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreHandler : IdleHeartBehaviour 
{
    public void OpenShop()
    {
        _ui.ShowPanelByName( "Shop" );
        List<ItemInfo> storeItems = GetItemInfoList();
        
        _events.ReportStoreOpening( storeItems );
    }

    private List<ItemInfo> GetItemInfoList()
    {
        List<OrganInfo> organs = _organs.organs.OrderBy( organ => organ.price ).ToList();
        List<ItemInfo> items = new List<ItemInfo>();

        foreach( OrganInfo organ in organs )
        {
            ItemInfo newItem = new ItemInfo( organ.Name,
                                                organ.price,
                                                _color.ShopColor,
                                                organ.image );
            items.Add( newItem );
        }

        return items;
    }

    public class ItemInfo
    {
        public string itemName;
        public int itemPrice;
        public Color itemColor;
        public Sprite itemSprite;

        public ItemInfo( string itemName, int itemPrice, Color itemColor, Sprite itemSprite )
        {
            this.itemName = itemName; 
            this.itemPrice = itemPrice;
            this.itemColor = itemColor;
            this.itemSprite = itemSprite;
        }
    }
}
