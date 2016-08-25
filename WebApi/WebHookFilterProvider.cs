using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;

namespace WebApiHost
{
    public class WebHookFilterProvider : IWebHookFilterProvider
    {
        public const string MessagePostedEvent = "MessagePostedEvent";
        public const string MessageRemovedEvent = "MessageRemovedEvent";

        private readonly Collection<WebHookFilter> filters = new Collection<WebHookFilter>
        {
            new WebHookFilter { Name = MessagePostedEvent, Description = "A message is posted."},
            new WebHookFilter { Name = MessageRemovedEvent, Description = "A message is removed."}
        };

        public Task<Collection<WebHookFilter>> GetFiltersAsync()
        {
            return Task.FromResult(filters);
        }
    }
}