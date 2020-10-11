using System;

namespace RobotSumo.Core
{
    public class CallbackActions
    {
        public CallbackActions(Action noActionNotification, Action actionNotification)
        {
            NoActionNotification = noActionNotification;
            ActionNotification = actionNotification;
        }

        public Action NoActionNotification { get; private set; }
        public Action ActionNotification { get; private set; }
    }
}