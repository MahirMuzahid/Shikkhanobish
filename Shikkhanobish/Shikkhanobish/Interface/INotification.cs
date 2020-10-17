using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Interface
{
    public interface INotification
    {
        event EventHandler NotificationReceived;
        void CreateNotification ( String title , String message );
        void ReceiveOrCancleCall();
    }
}
