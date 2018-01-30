using System;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
    public class NotificationExample : MonoBehaviour
    {
		public static NotificationExample instance;
		void Awake()
		{
			if (instance == null)
				instance = this;
		}
		public void ScheduleSimple(int pTime)
        {
			NotificationManager.Send(TimeSpan.FromSeconds(pTime), "Simple notification", "Customize icon and color", new Color(1, 0.3f, 0.15f));
        }

		public void ScheduleNormal(int pTime)
        {
			NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(pTime), "Notification", "Notification with app icon", new Color(0, 0.6f, 1), NotificationIcon.Message);
        }

		public void ScheduleCustom(int pTime)
        {
            var notificationParams = new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
				Delay = TimeSpan.FromSeconds(pTime),
                Title = "Custom notification",
                Message = "Message",
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Heart,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }

        public void CancelAll()
        {
            NotificationManager.CancelAll();
        }
    }
}