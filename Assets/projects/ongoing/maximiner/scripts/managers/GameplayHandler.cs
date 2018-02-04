using System.Collections;
using System.Collections.Generic;
using Maximiner;
using UnityEngine;

public class GameplayHandler : MaximinerBehaviour
{
    void OnEnable()
    {
        EventsController.OnAsteroidEntryClicked += OnAsteroidEntryClicked;
        EventsController.OnAsteroidMiningCycleFinished += OnAsteroidMiningCycleFinished;
    }
    
    void OnDisable()
    {
        EventsController.OnAsteroidEntryClicked -= OnAsteroidEntryClicked;
        EventsController.OnAsteroidMiningCycleFinished -= OnAsteroidMiningCycleFinished;
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

    private void OnAsteroidMiningCycleFinished(AsteroidMiningInfo ami)
    {
        Debug.Log("Finished a mining cycle.");
        OreYieldInfo yield = ami.GetCycleOreYieldInfo();
        ami.Asteroid.RemoveOre(yield);

        if (!_Ship.CurrentShip.TryAddOreToCargoModule(yield))
        {
            // Can't mine anymore. Finish up.
            Debug.Log("Hello?");
            ami.MiningModule.StopMining(ami.Asteroid);
        }
    }
}
