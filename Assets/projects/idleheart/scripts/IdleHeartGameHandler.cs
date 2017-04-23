using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHeartGameHandler : MonoBehaviour 
{
    public GameObject stage;
    public HeartHandler heart;

    public void HideStage()
    {
        stage.SetActive( false );
    }

    public void ShowStage()
    {
        stage.SetActive( true );
    }

    public void EndGame()
    {
        HideStage();
    }
    
    public void ResetGame()
    {
        heart.ResetHeart();
    }

    public void UpdateGame()
    {
        heart.UpdateHeart();
    }

    public int GetScore()
    {
        return heart.CurrentScore;
    }
}
