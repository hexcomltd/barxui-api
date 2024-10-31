# Endpoint: Label

## GET label/{{id}}

Get a single Label.

### Parameters

|Parameter|Description|Type|
|--|--|--|
|id|Label ID|UUID|

### Response

|Status Code|Data|
|--|--|
|OK|[Label](#LabelResponse) |
|NotFound|no body|

### Examples

```
GET {{host}}/label/{{id}}
Content-Type: application/json
Authorization: Token {{apiKey}}
```

## POST label/list

Get a page of LabelHeader.

### Parameters

|Parameter|Description|
|--|--|
|Body|[LabelSearchRequest](#LabelSearchRequest)|

### Response

|Status Code|Data|
|--|--|
|OK|[LabelListResponse](#LabelListResponse) |
|NotFound|no body|

### Example

```
POST {{host}}/label/list
Content-Type: application/json
Authorization: Token {{apiKey}} 

{
  {{LabelSearchRequest}}
}
```

# Data

Request and response data definitions

## Requests

<a name="LabelResponse"/>

### Label Response

|Property|Description|Type|
|--|--|--|
|settings|General label settings|[LabelSettings](#LabelSettings)|
|media|Media definition|[LabelMediaTemplate](#LabelMediaTemplate)|
|layers|||
|variables|||
|id|||
|name|||
|description|||
|organisationID|||
|ownerID|||
|authorID|||
|created|||
|lastUpdated|||
|cultureCode|||
|mediaTemplateID|||

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

<a name="LabelSettings" />

### LabelSettings

|Property|Description|Type|
|--|--|--|

<a name="LabelSearchRequest" />

### LabelSearchRequest

|Property|Description|Type|
|--|--|--|
|page|Page number|INT|
|pageSize|Requested page size. Max 50|INT|
|searchText|Search for label names|INT|
|orderBy|"Name", "Updated", "Category"|ENUM|
|descending|true or false|BOOL|

#### Examples

```
{
  "page" : 0,
  "pageSize" : 10,
  "searchText" : "",
  "orderBy" : "Name",
  "descending": false
}
```

Search by name containing 'code':

```
{
  "page" : 0,
  "pageSize" : 10,
  "searchText" : "code",
  "orderBy" : "Name",
  "descending": false
}
```

## Responses

<a name="LabelHeader" />

### LabelHeader

|Property|Description|Type|
|--|--|--|
|id|Unique ID for the label|UUID|
|name|Name for the label|TEXT|
|description|Description of the label|TEXT|
|organisationID|Unique ID of the organisation|UUID|
|ownerID|Unique ID of the owner (user)|UUID|
|authorID|Unique ID of the author (user)|UUID|
|created|Date label was first created|DATE|
|lastUpdated|Date label was last updated|DATE|
|addons|A collection of AddOn codes that control/use this label|TEXT Array|
|cultureCode|Overriden culture code|TEXT|
|mediaTemplateID|Unique ID of the default media template|UUID|

Example:

```
{
    "id": "6396b7dd-f655-bc54-9fd8-8ae63808a28e",
    "name": "Sample Label",
    "description": "Print a widget label",
    "organisationID": "59a54e38-e407-4acc-acfb-aed83b9f58c5",
    "ownerID": "805a6a68-e847-441e-8cc9-f301494de78b",
    "authorID": "805a6a68-e847-441e-8cc9-f301494de78b",
    "created": "2020-06-23T20:48:13.0935527+12:00",
    "lastUpdated": "2023-06-09T05:46:00.5082365+12:00",
    "addOns": [],
    "cultureCode": "en-GB",
    "mediaTemplateID": "4e8a24a9-a0ac-ca19-15b5-871958aa1418"
  }
```

<a name="LabelHeaderResponse"/>

### LabelHeader Paged Response

|Property|Description|Type|
|--|--|--|
|values|List of values|[LabelHeader](#LabelHeader) Array|
|page|Page number|INT|
|totalPages|Total number of available pages|INT|
|totalItems|Total number of available items|INT|
|pageSize|Requested page size|INT|

Example:

```
{
  "values": [
     "id": "6396b7dd-f655-bc54-9fd8-8ae63808a28e",
      "name": "Sample Label",
      "description": "Print a widget label",
      "organisationID": "59a54e38-e407-4acc-acfb-aed83b9f58c5",
      "ownerID": "805a6a68-e847-441e-8cc9-f301494de78b",
      "authorID": "805a6a68-e847-441e-8cc9-f301494de78b",
      "created": "2020-06-23T20:48:13.0935527+12:00",
      "lastUpdated": "2023-06-09T05:46:00.5082365+12:00",
      "addOns": [],
      "cultureCode": "en-GB",
      "mediaTemplateID": "4e8a24a9-a0ac-ca19-15b5-871958aa1418"
  ],
  "page": 0,
  "totalPages": 5,
  "totalItems": 50,
  "pageSize": 10
}
```
