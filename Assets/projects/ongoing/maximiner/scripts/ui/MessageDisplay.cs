using System.Collections;
using System.Collections.Generic;
using Maximiner;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    public WarningMessage warningMessage;

    void OnEnable()
    {
        EventsController.OnDoWarningMessage += OnDoWarningMessage;
    }
    
    void OnDisable()
    {
        EventsController.OnDoWarningMessage -= OnDoWarningMessage;
    }

    private void OnDoWarningMessage(string message)
    {
        warningMessage.Show();
        warningMessage.SetMessage(message);
    }
}
