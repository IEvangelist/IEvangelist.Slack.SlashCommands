# Slack
### / Commands (slash commands)

#### Getting Started

This ASP.NET Core Web API application is hosted in Azure as an App Service. It is configured to leverage bitly, open weather map and the icndb apis. All of these APIs require various credentials and registration to consume them.

 - [Open Weather Map - Signup for API](https://home.openweathermap.org/users/sign_up)
 - [Bitly.com - Signup for API](https://bitly.com/a/sign_up)

Add environment variables for open weather map:

```
setx OpenWeatherMapOptions__Key [Your Key Here]
```

Add environment variables for bit.ly:

```
setx BitlyOptions__ApiKey [Your API Key Here]
setx BitlyOptions__Login [Your Login Here]
```

Visual Studio requires a restart after environment variables have been added. 

#### Testing Server

To avoid registering for several new APIs, etc... simply visit the Swagger API that I've conveniently exposed [here](https://slack-slashcommands.azurewebsites.net/swagger). The details for these APIs are listed below. Likewise, you are [free to join the slack channel](http://bit.ly/2TYia6g) and try out these commands in slack!

### APIs

All of these APIs are HTTP Post and expect the `SlackCommandRequest` object.

##### `api/jokes/random`

This endpoint maps to the `/joke` and `/joke share` slash commands.

###### Description

Returns a random nerdy joke about Chuck Norris. There is an optional parameter, type `share` to make the joke public to everyone in the room.

##### `api/weather`

This endpoint maps to the `/weather [zip-code]` slash command.

###### Description

Given a zip-code will return the current weather, with corresponding emoji.

##### `api/urls/shorten`

This endpoint maps to the `/shortenUrl [longUrl]` slash command.

###### Description

Given a long URL, will return a shortened URL - leverages bit.ly API under the covers.

### Resources

 - [Slack Slash Commands - Official Docs](https://api.slack.com/slash-commands)
 - [Slack Slash Commands with .NET Core Web API](https://medium.com/@scottmichaellandau/slack-slash-commands-with-net-core-web-api-a71395db7504)