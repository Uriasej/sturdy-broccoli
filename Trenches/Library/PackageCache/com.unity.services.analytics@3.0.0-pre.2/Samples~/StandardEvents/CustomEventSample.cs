using System;
using System.Collections.Generic;

namespace Unity.Services.Analytics
{
    public static class CustomEventSample
    {
        public static void RecordCustomEventWithNoParameters()
        {
            Events.CustomData("myEvent", new Dictionary<string, object>());
        }
        
        public static void RecordCustomEventWithParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "fabulousString", "hello there" },
                { "sparklingInt", 1337 },
                { "tremendousLong", Int64.MaxValue },
                { "spectacularFloat", 0.451f },
                { "incredibleDouble", 0.000000000000000031337 },
                { "peculiarBool", true },
                { "ultimateTimestamp", DateTime.UtcNow }
            };
            
            Events.CustomData("myEvent", parameters);
        }
    }
}
