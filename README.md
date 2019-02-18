# Slack
### / Commands (slash commands)

#### Getting Started

This ASP.NET Core Web API application is hosted in Azure as an App Service. It is configured to leverage bitly, open weather map and the icndb apis. All of these APIs require various credentials and registration to consume them.

#### Testing Server

To avoid registering for several new APIs, etc... simply visit the Swagger API that I've conveniently exposed [here](https://slack-slashcommands.azurewebsites.net/swagger). The details for these APIs are listed below. Likewise, you are [free to join the slack channel](http://bit.ly/2TYia6g) and try out these commands in slack!

### APIs

All of these APIs are HTTP Post and expect the `SlackCommandRequest` object.

##### `api/jokes/random`

Returns a random nerdy joke about Chuck Norris. There is an optional parameter, type `share` to make the joke public to everyone in the room.


##### `api/weather`

Given a zip-code will return the current weather, with corresponding emoji.

##### `api/urls/shorten`

Given a long URL, will return a shortened URL - leverages bit.ly API under the covers.
