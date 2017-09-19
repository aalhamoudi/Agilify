# Account Management

![](Account-Management.jpeg)

## Authentication

Authentication is delegated to Social identity providers (Facebook, Microsoft, etc...), for convenience and added security (manual registatrion has been implemnted but is disabled)

## Authorization

APIs are secured using OAuth 2.0, requiring JWT access token to be included in requests for them to be accepted.

## Flow

On first run, the user is prompted to login (authenticate) using a social identity provider, once authenticated, the provider returns an access token, which the backend uses to retrieve user info (name, email, and picture) from the providers graph API, and creates (or retreives) an account based on the retreived info.

The backend then sends the info associated with the newly created account along with a generated access token to the client.

The client stores the info and the access token as settings, and embeds the access token in requests to the server.