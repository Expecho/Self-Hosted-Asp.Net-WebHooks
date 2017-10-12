# Asp.Net WebHooks
Self hosted custom webhook receiver and sender.
More about webhooks: https://docs.asp.net/projects/webhooks/en/latest/. The product repo can be found [here](https://github.com/aspnet/WebHooks).

The solution contains 2 projects:

## Receiver
This console application sends a registration for a Webhook and receives message of the Webhook. It also calls the api that triggers the Webhook to send a notification.

## Web Api Host
This console application hosts the Webhook registration endpoints and hosts a web api controller that triggers the Webhook.

## Usage
Set the startup projects of the solution to both projects and run the solution. Wait until both projects are fully loaded and then start interacting.

# Points of interest
There are some not so very well documented features you should know of before throwing the towel in the ring.

## Endpoint verification
If you create your own controller with a POST method to facilitate incoming webhooks you have to have a GET method also that accepts an Echo parameter and returns the exact content of that parameter in plain text. Failure to do so will result in the following error when you register your webhook:

> WebHook verification failed. Please ensure that the WebHook URI is valid and that the endpoint is accessible.

To disable this verification you can include the NoEcho parameter upon registration like this:

    // Create a webhook registration to our custom controller
    var registration = new Registration
    {
        WebHookUri = $"{webhookReceiverBaseAddress}/api/webhook?NoEcho=1",
        Description = "A message is posted.",
        Secret = "12345678901234567890123456789012",

        // Remove the line below to receive all events, including the MessageRemovedEvent event.
         Filters = new List<string> { "MessagePostedEvent" } 
     };

(See [the registration](https://github.com/Expecho/Self-Hosted-Asp.Net-WebHooks/blob/4ea52401150c47a647d2ffebdc4794573ebfe2a0/Receiver/Program.cs#L28) and [the GET method](https://github.com/Expecho/Self-Hosted-Asp.Net-WebHooks/blob/4ea52401150c47a647d2ffebdc4794573ebfe2a0/Receiver/WebhookController.cs#L21))
