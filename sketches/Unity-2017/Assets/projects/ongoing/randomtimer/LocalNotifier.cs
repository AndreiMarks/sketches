using System;
using UnityEngine;
using UnityEngine.iOS;

using NotificationServices = UnityEngine.iOS.NotificationServices;
using LocalNotification = UnityEngine.iOS.LocalNotification;

public static class LocalNotifier
{
    private const string _ALERT_SOUND_NAME = "ZenBell.wav";
    private const string _ALERT_TEXT = "Time is up!";
    
    public static void RegisterNotifications()
    {
        NotificationServices.RegisterForNotifications(
            NotificationType.Alert |
            NotificationType.Badge |
            NotificationType.Sound);
    }
    
    public static void ClearAlertNotifications()
    {
        if (NotificationServices.scheduledLocalNotifications.Length > 0)
        {
            Debug.Log("Clearing Local Notifs.");
            NotificationServices.CancelAllLocalNotifications();
            NotificationServices.ClearLocalNotifications();
        }
        
        DebugNotifications();
    }
    
    public static void ScheduleAlertNotification(int seconds)
    {
        LocalNotification notification = new LocalNotification();
        
        DateTime fireDate = DateTime.Now.AddSeconds(seconds);
        
        notification.fireDate = fireDate;
        Debug.Log("Notif should fire at: " + fireDate);
        notification.alertBody = _ALERT_TEXT;
        notification.soundName = _ALERT_SOUND_NAME;
        
        NotificationServices.ScheduleLocalNotification(notification);
        
        DebugNotifications();
    }

    public static void DebugNotifications()
    {
        Debug.Log("Debugging Notifications");
        if (NotificationServices.scheduledLocalNotifications.Length > 0)
        {
            foreach (LocalNotification ln in NotificationServices.scheduledLocalNotifications)
            {
                Debug.Log(ln.fireDate.ToString());
            }
        }
        else
        {
            Debug.Log("No notifications scheduled.");
        }
    }
}
