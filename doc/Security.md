# Security

In addition to Authentication/Authorization described in [Account Management](Account Management),

All GET api endpoints take the userId and return only the entities that user is allowed to access.

Moreover, the UI doesn't allow unauthorized operations (e.g. removing a member from the team if not the owner, etc...)

Later on, all data will be encrypted.