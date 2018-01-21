using System.Collections;
using System.Collections.Generic;
using Maximiner;
using UnityEngine;

public class GameplayHandler : MaximinerBehaviour
{
    void OnEnable()
    {
        EventsController.OnAsteroidEntryClicked += OnAsteroidEntryClicked;
    }
    
    void OnDisable()
    {
        EventsController.OnAsteroidEntryClicked -= OnAsteroidEntryClicked;
    }

    private void OnAsteroidEntryClicked(Asteroid asteroid)
    {
        Debug.Log("Gameplay | clicked on an asteroid: " + asteroid.ToString());
        // An Asteroid Menu UI element was clicked, we want to see if we can mine.
        if (_Ship.CurrentShip.IsMiningAsteroid(asteroid))
        {
            _Ship.CurrentShip.StopMiningAsteroid(asteroid);
        }            
        else if (_Ship.CurrentShip.IsReadyToMine)
        {
            Debug.Log("The ship is ready to mine.");
            _Ship.CurrentShip.StartMiningAsteroid(asteroid);
        }
        else
        {
            string warning = "The ship does not have enough cargo space. Return to the station.";
            if (!_Ship.CurrentShip.HasAvailableMiningModules)
            {
                warning = "The ship does not have any available mining modules.";
            } 
            else if (!_Ship.CurrentShip.HasAvailableCargoSpace)
            {
                warning = "The ship does not have enough cargo space. Return to the station.";
            }
            
            _Events.ReportDoWarningMessage(warning);
        }
    }
}
