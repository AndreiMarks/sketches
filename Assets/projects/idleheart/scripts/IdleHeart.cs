using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHeart : IdleHeartBehaviour 
{
    public UIPanelHandler ui;
    public HeartHandler heart;
    public IdleHeartInputHandler input;
    public IdleHeartGameHandler game;
    public ColorHandler color;
    public ScoreHandler score;
    public StoreHandler store;
    public OrganHandler organs;
    public BodyNetHandler bodyNet;

    void Start()
    {
        StartCoroutine( ShowTitle() );
    }
    
    private IEnumerator ShowTitle()
    {
        ui.ShowSinglePanelByName( "Title" );
        yield return 0;
    }

    public void PrepareGame()
    {
        StartCoroutine( DoPrepareGame() );
    }

    private IEnumerator DoPrepareGame()
    {
        ui.ShowSinglePanelByName( "Prepare" );
        yield return 0;
    }

    public void StartGame()
    {
        StartCoroutine( RunGame() );
    }

    private IEnumerator RunGame()
    {
        Debug.Log( "Running game." );

        ui.ShowSinglePanelByName( "Game" );

        game.ShowStage();
        game.ResetGame();

        while( heart.IsAlive )
        {
            heart.UpdateHeart();
            yield return 0;
        }

        StartCoroutine( EndGame() );
    }

    private IEnumerator EndGame()
    {
        Debug.Log( "Ending game." );
        ui.ShowSinglePanelByName( "End" );
        
        game.EndGame();
        int score = game.GetScore();

        _events.ReportShowScoreScreen( score );

        yield return 0;
    }

    public void RestartGame()
    {
        StartCoroutine( ShowTitle() );
    }

    public void DebugButtonPress()
    {
        Debug.Log("I'm pressing button.");
    }
}
