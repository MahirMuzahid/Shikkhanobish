using System;
using System.Collections.Generic;
using System.Text;

namespace Shikkhanobish.Model
{
    public delegate void EventHandler(object source, EventArgs e);
    public class StaticPageForGeneralUse
    {
        public static event EventHandler EndCall;
        public static int TappedTeacherIdForTuition { get; set; }

        public void PlaceTeacherStatusToOffile()
        {
            OnEndCall(EventArgs.Empty);
        }
        protected virtual void OnEndCall(EventArgs e)
        {
            EndCall?.Invoke(this, e);
        }
    }
}
