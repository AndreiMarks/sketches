using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHeartBehaviour : AmBehaviour 
{
    private IdleHeart _ih { get { return IdleHeartController.Instance.idleHeart; } }
    
    protected IdleHeartEvents _events { get { return IdleHeartEvents.Instance; } }

    protected ColorHandler _color { get { return _ih.color; } }
    protected OrganHandler _organs { get { return _ih.organs; } }
    protected ScoreHandler _score { get { return _ih.score; } }
    protected StoreHandler _store { get { return _ih.store; } }
    protected UIPanelHandler _ui { get { return _ih.ui; } }
}
