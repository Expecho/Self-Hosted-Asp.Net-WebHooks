using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiHost
{
    [Authorize]
    public class MessagesController : ApiController
    {
        private static readonly List<Message> Messages = new List<Message>();

        public List<Message> Get()
        {
            return Messages;
        }

        public async Task Post(Message message)
        {
            Messages.Add(message);

            await this.NotifyAsync(WebHookFilterProvider.MessagePostedEvent, new { Message = message });
            Console.WriteLine("Notification send for message from " + message.Sender);
        }
    }
}