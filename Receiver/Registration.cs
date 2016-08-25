using System.Collections.Generic;

namespace Receiver
{
    public class Registration
    {
        public string WebHookUri { get; set; }
        public string Secret { get; set; }
        public string Description { get; set; }
        public List<string> Filters { get; set; }
    }
}