using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroFarm : MonoBehaviour
{
    public FarmBoard farmBoard;
    public MicroFarmTimeHandler timeHandler;
    
    void Start()
    {
        RunGame();
    }

    private void RunGame()
    {
        Debug.Log( "Starting the game." );
        farmBoard.CreateBoard();
        StartCoroutine( WaitForRoundStart() );
    }

    private IEnumerator WaitForRoundStart()
    {
        while ( true )
        {
            if ( Input.anyKeyDown )
            {
                StartCoroutine( StartPeriod() );
                yield break;
            }
            
            yield return 0;
        }
    }
    
    private IEnumerator StartPeriod()
    {
        yield return StartCoroutine( timeHandler.StartPeriod() );

        StartCoroutine( WaitForRoundStart() );
    }
}
