using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ItemInfo = StoreHandler.ItemInfo;

public class BodyShop : IdleHeartBehaviour 
{
    public StoreCard storeCardPrefab;
    public Transform storeCardHolder;
    public ScrollRect scrollRect;
    
    void OnEnable()
    {
        IdleHeartEvents.OnStoreOpening += OnStoreOpening;
    }

    void OnDisable()
    {
        IdleHeartEvents.OnStoreOpening -= OnStoreOpening;
    }

    private void OnStoreOpening( List<ItemInfo> items )
    {
        storeCardHolder.DestroyAllChildren();

        foreach( ItemInfo item in items )
        {
            StoreCard newCard = storeCardHolder.InstantiateChild( storeCardPrefab );
            newCard.transform.ZeroOut();

            newCard.Initialize( item );
        }

        scrollRect.normalizedPosition = new Vector2( 0.5f, 1f );
    }
}
