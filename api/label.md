# Endpoint: Label

## POST label/list

Get a page of Labels.

Use this to get a list of all labels - 1 page at a time, or to search for labels by name.

Search will match any part of the same and is not case-sensitive.

### Parameters

|Parameter|Description|
|--|--|
|Body|[LabelSearchRequest](#LabelSearchRequest)|

### Example

```
POST {{host}}/label/list
Content-Type: application/json
Authorization: Token {{apiKey}} 

{
  "page" : 0,
  "pageSize" : 10,
  "searchText" : "",
  "orderBy" : "Name",
  "descending": false
}
```

### Response

|Status Code|Data|
|--|--|
|OK|PagedResponse of [Label](#Label) |

## GET label/{{id}}

Get a single Label.

### Parameters

|Parameter|Description|Type|
|--|--|--|
|id|Label ID|UUID|

### Examples

```
GET {{host}}/label/{{id}}
Content-Type: application/json
Authorization: Token {{apiKey}}
```

### Response

|Status Code|Data|
|--|--|
|OK|[Label](#LabelResponse) |
|NotFound|no body|

# Data

Request and response data definitions

## Requests

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

<a name="LabelResponse"/>

### Label

|Property|Description|Type|
|--|--|--|
|id|Unique ID|UUID|
|name|Name of the label|TEXT|
|description|Description of the label|TEXT|
|created|Date label first created|DATE|
|lastUpdated|Date label last updated or null if not|DATE|
|cultureCode|Culture code override for label|TEXT|
|inputs|List of possible inputs & query parameters|TEXT Array|
|fields|List of possible data values|TEXT Array|

#### Examples

Labels can include a list of possible inputs.  In this example:

* Operator & Batch are input from an external system and are used directly on the label
* WidgetCode is used as a query parameter to find a list of matching records from a table in barxui

Required field inputs  must be specified when printing but may be blank.

Missing values for an input is effectively blank which may cause errors when printing.

A list of data fields is also included.  The value of these can be specified when printing rather than using the values from a barxui query.

```
{
  "id": "6396b7dd-f655-bc54-9fd8-8ae63808a28e",
  "name": "Red Left Widget Box",
  "description": "Left Handed, Red Widget Box Label",
  "created": "2020-06-23T20:48:13.0935527+12:00",
  "lastUpdated": "2023-06-09T05:46:00.5082365+12:00",
  "cultureCode": "en-US",
  "inputs": [ "Operator", "Batch", "WidgetCode" ],
  "fields": [ "WidgetCode", "Description", "Width", "Height", "Weight" ]
}
```
