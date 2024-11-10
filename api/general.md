# General Information

General information that applies to the entire API, exceptions noted where applicable.

* All examples HTTP messages are compatible with standard HTTP request file format.
* All unique identifiers are 128bit UUID (aka GUID).

## Making Requests

Using your programming language of choice, issue an HTTP request. 

Requests are made to :

    https://api.barxui.com/api/v2
    {{host}} in samples.

and MUST include an Authorization header with the value:

    Token <API KEY>
    {{apikey}} in samples

and the expected content type:

    Accept: application/json

## Lists of items

All requests that fetch more than a single value will return a list as part of a PagedResponse.  
Request will include a page and pageSize.
The pageSize will have a maximum value.

A PagedResponse looks like this:

```
{
    "values": [array of value of specific type],
    "page": requested page number,
    "pageSize": requested page size,
    "totalItems": number of items for all pages,
    "totalPages": total number of pages available
}
```
