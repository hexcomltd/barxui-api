# Endpoint: Printer

Methods to manage printers.

## GET printer/list

List the enabled agent printers. Also does not include file printer such as PDF or PNG.

### Parameters

|Parameter|Description|Type|
|--|--|--|
|page|Page of agent list|INT|
|pageSize|Number of records per page.  Max 20.|INT|

### Examples

```
GET {{host}}/printer/list?page={{page}}&pageSize={{pageSize}}
Accept: application/json
Authorization: Token {{apiKey}}
```

### Response

|Status Code|Data|
|--|--|
|OK|PagedResponse of [Printer](#PrinterResponse) |
|NotFound|no body|

# Data

<a name="PrinterResponse" />

## Printer Response

|Property|Description|Type|
|--|--|--|
|id|Unique ID of Printer|UUID|
|name|Name of the Printer|TEXT|
