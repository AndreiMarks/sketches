using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHeartEvents : Controller<IdleHeartEvents>
{
    public static event Action<int> OnHeartbeatsUpdated = (beats) => {};
    public void ReportHeartbeatsUpdated( int beats )
    {
        OnHeartbeatsUpdated( beats );
    }

    public static event Action<int> OnLifeForceUpdated = (amount) => {};
    public void ReportLifeForceUpdated( int amount )
    {
        OnLifeForceUpdated( amount );
    }

    public static event Action<float> OnDeathTimerUpdated = (ratio) => {};
    public void ReportDeathTimerUpdated( float ratio )
    {
        OnDeathTimerUpdated( ratio );
    }

    public static event Action<int> OnShowScoreScreen = (score) => {};
    public void ReportShowScoreScreen( int score )
    {
        OnShowScoreScreen( score );
    }

    public static event Action<List<StoreHandler.ItemInfo>> OnStoreOpening = (items) => {};
    public void ReportStoreOpening( List<StoreHandler.ItemInfo> items )
    {
        OnStoreOpening( items );
    }
}
