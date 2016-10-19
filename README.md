# Asp.Net WebHooks
Self hosted custom webhook receiver and sender.
More about webhooks: https://docs.asp.net/projects/webhooks/en/latest/

The solution contains 2 projects:

## Receiver
This console application sends a registration for a Webhook and receives message of the Webhook. It also calls the api that triggers the Webhook to send a notification.

## Web Api Host
This console application hosts the Webhook registration endpoints and hosts a web api controller that triggers the Webhook.

## Usage
Set the startup projects of the solution to both projects and run the solution. Wait until both projects are fully loaded and then start interacting.
