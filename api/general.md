# General Information
* All examples HTTP messages are compatible with standard HTTP request file format.
* All unique identifiers are 128bit UUID (aka GUID).

## Making Requests

Using your programming language of choice, issue an HTTP request. 

Requests are made to :

    https://api.barxui.com/api/v2

and MUST include an Authorization header with the value:

    Token <API KEY>

and the expected content type:

    Content-Type: application/json
