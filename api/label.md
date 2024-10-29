# Endpoint: Label

## GET /label/<id>

Get a single Label.

```
GET {{host}}/api/v2/label/{{id}}
Content-Type: application/json
Authorization: Token {{apiKey}}
```

### Parameters

|Parameter|Description|
|--|--|
|id|Label ID|

### Response

|Status Code|Data|
|--|--|
|OK|[Label](#LabelResponse) |
|NotFound|no body|

## POST /label/list

Get a page of LabelHeader.

__Must request in pages__.

```
POST {{host}}/api/v2/label/list
Content-Type: application/json
Authorization: Token {{apiKey}} 

{
  [LabelSearchRequest](#LabelSearchRequest)
}
```

# Data

Request and response data definitions

## Requests

<a name="LabelSearchRequest" />

### LabelSearchRequest

```
{
  "Page" : 0..LAST PAGE,
  "PageSize" : 1..50,
  "SearchText" : "TEXT",
  "OrderBy" : "Name | Updated | Category",
  "Descending": true | false
}
```

## Responses

<a name="LabelHeaderResponse" />

### LabelHeader Response

```

```

<a name="LabelResponse"/>

### Label Response

```
{
  "settings": { SETTINGS },
  "media": { MEDIATEMPLATE },
  "layers": [ LAYER ],
  "variables": [ VARIABLE ],
  "id": "UUID",
  "name": "TEXT",
  "description": "TEXT",
  "organisationID": "UUID",
  "ownerID": "UUID",
  "authorID": "UUID",
  "created": "DATE",
  "lastUpdated": "DATE",
  "cultureCode": "CULTURE",
  "mediaTemplateID": "UUID"
}
```
