# Endpoint: Printer

Methods to manage printers.

## GET printer/list

List the printers.

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
| values           | List of printer objects          | ARRAY   |
| values.id        | Unique ID of Printer             | UUID    |
| values.name      | Name of the Printer              | TEXT    |
| page             | Current page of results          | INTEGER |
| totalPages       | Total number of pages            | INTEGER |
| totalItems       | Total number of items            | INTEGER |
| pageSize         | Number of items per page         | INTEGER |

### Examples

```
{
    "values": [
        {
            "id": "123e4567-e89b-12d3-a456-426614174000",
            "name": "Printer Model A - location 1"
        },
        {
            "id": "123e4567-e89b-12d3-a456-426614174000",
            "name": "Printer Model B - location 2"
        }
    ],
    "page": 0,
    "totalPages": 1,
    "totalItems": 2,
    "pageSize": 2
}
```
