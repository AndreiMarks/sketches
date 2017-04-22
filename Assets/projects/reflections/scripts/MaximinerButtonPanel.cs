using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MaximinerButtonPanel : ButtonPanel
{
    public ScrollRect scrollRect;
    public CanvasScaler canvasScaler;
    public GridLayoutGroup gridLayoutGroup;

    protected override Dictionary<string, Action> _ButtonDict 
    { 
        get 
        {
            if ( _buttonDict == null )
            {
                _buttonDict = new Dictionary<string, Action>() {};
            }

            return _buttonDict;
        } 
    }

    public void AddButtonToDict( string name, Action action )
    {
        _ButtonDict.Add( name, action );
    }

    public ButtonPanelButton AddButtonToPanel( string name, Action action )
    {
        _ButtonDict.Add( name, action );
        return CreateButtonObject( name, action );
    }

    public override ButtonPanelButton CreateButtonObject( string name, Action onClick )
    {
        if ( this.gameObject.activeInHierarchy ) StartCoroutine( StopScrollRect() );
        return base.CreateButtonObject( name, onClick );
    }

    private IEnumerator StopScrollRect()
    {
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        yield return 0;
        scrollRect.movementType = ScrollRect.MovementType.Elastic;
    }

    public void CreatePanel()
    {
        ClearButtonObjects();
        CreateButtonObjects();
    }

    public void SetMapButtonLayout( MapButtonLayout layout )
    {
        float xFactor = 1;
        float yFactor = 1;
        
        float width = canvasScaler.referenceResolution.x;
        float height = canvasScaler.referenceResolution.y;

        switch( layout )
        {
            case ( MapButtonLayout.TwoByEight ):
                xFactor = 2;
                yFactor = 4;
            break;

            case ( MapButtonLayout.OneByFive ):
                xFactor = 1;
                yFactor = 5;
            break;
        }

        Vector2 cellSize = new Vector2( width / xFactor, height / yFactor );
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.constraintCount = (int)xFactor;
    }
}
