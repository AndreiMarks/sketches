using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDisplay : MonoBehaviour 
{
    public PositionInfo[] displayInfos;

    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.UpArrow ) ) MoveDisplayToCenter();
        if ( Input.GetKeyDown( KeyCode.DownArrow ) ) MoveDisplayToBottom();
    }

    public void MoveDisplayToCenter()
    {
        MoveDisplayToPosition( Position.Center );
    }

    public void MoveDisplayToBottom()
    {
        MoveDisplayToPosition( Position.Bottom );
    }

    private void MoveDisplayToPosition( Position position )
    {
        foreach( PositionInfo pi in displayInfos )
        {
            switch( position )
            {
                case ( Position.Center ):
                    pi.transform.anchoredPosition = pi.centerPosition;
                break;

                case ( Position.Bottom ):
                    pi.transform.anchoredPosition = pi.bottomPosition;
                break;
            }
        }
    }

    private enum Position { Center, Bottom }

    [System.Serializable]
    public class PositionInfo
    {
        public RectTransform transform;
        public Vector2 centerPosition;
        public Vector2 bottomPosition;
    }
}
