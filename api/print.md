# Endpoint: Print

Methods to print a label. Print request can include data or reference data from barxui tables.  Output can be to a printer via an agent or as a download of a single PDF file or a single PNG files or a zip of multiple PNG files.

## POST print

Print one or more labels to file or to a printer via an agent.

Printing requires knowing the label and the printer to use.
* A list of labels can be obtained from the [label endpoint](./label.md).  The labelId is used to specify the label to print.
* A list of printers can be obtained from the [printer endpoint](./printer.md).  The printerId is used to specify the printer to use.

### Parameters

|Parameter|Description|
|--|--|
|Body|[PrintRequest](#PrintRequest)|

#### Examples

Print to PDF.

```
{
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "labelId": "123e4567-e89b-12d3-a456-426614174000",
    "data": [
        {
            "name": "John Doe",
            "address": "123 Main St",
            "city": "Anytown",
            "state": "CA",
            "zip": "12345"
        },
        {
            "name": "Jane Doe",
            "address": "456 Elm St",
            "city": "Othertown",
            "state": "CA",
            "zip": "54321"
        }   
    ],
    "outputMode": "PDF",
    "outputParameters": {
        "filename": "label.pdf",
        "copies": 1,
        "collate": true,
        "columnFirst": false,
        "cropMarks": false,
        "flip": false,
        "mirror": false
    }
}
```

Print to printer.

```
{
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "labelId": "123e4567-e89b-12d3-a456-426614174000",
    "data": {
        "name": "John Doe",
        "address": "123 Main St",
        "city": "Anytown",
        "state": "CA",
        "zip": "12345"
    },
    "outputMode": "PRINT",
    "outputParameters": {
        "printerId": "123e4567-e89b-12d3-a456-426614174000",
        "copies": 1,
        "collate": true,
        "columnFirst": false,
        "cropMarks": false,
        "flip": false,
        "mirror": false
    }
}
```

# Data

## Requests

<a name="PrintRequest" />

### PrintRequest

Request print of a label to PDF, PNG (Zipped) or to a printer via an Agent.

|Property|Description|Type|
|--|--|--|
|id|An id for the print request.  Can be used to fetch result later.|UUID|
|labelId|Id of the label to print|UUID|
|input|Input data for the label|Input Dictionary (see below)|
|data|Data to print on the label|Data Dictionary (see below)|
|queryParams|Parameters to the label query|Data Dictionary (see below)|
|outputMode|"PDF", "PNG", "PRINT"|ENUM|
|outputParameters|Options relative to the outputMode|Output Parameters Dictionary (see below)|

#### Notes

- id of the print request is optional but is required to retrieve the status of the print request to an agent printer.

- labelId is required to specify the label to print.  The labelId can be obtained from the [label endpoint](./label.md) or from the label URL in [barxui](https://app.barxui.com/design).


##### Input

input is a  dictionary of key/value pairs.  The key is the name of a prompt in the label template.  The value should match the expected type and constraints of prompt.

##### Data 

data is an array of data rows.  Each row has key/value pairs.  The key should be one of the fields from the [label definition](label.md#label).  The value should match the expected type and constraints of data in the underlying table definition.

##### QueryParams

queryParams is a dictionary of key/value pairs.  The key should be one of the query parameters from the [label definition](label.md#label).  The value must be specified and can contain wildcard characters, eg "widget*", or "*widget" or "*" for all.


##### OutputParameters 

|Property|Description|Type|
|--|--|--|
|copies|The number of copies of each label to print|INT|
|collate|Collate copies of labels when label define multiple labels per page |BOOL|
|columnFirst|Print down each column when label define multiple labels per page |BOOL|
|cropMarks|Print crop marks are the corner of each page||
|flip|Flip the labels vertically|BOOL|
|mirror|Flip the label horizontally|BOOL|
|filename|Name of the output file if PDF or PNG mode|TEXT|
|printerId|ID of the printer if PRINT mode|UUID|

## Responses

<a name="PrintResponse" />

### PrintResponse

|Property|Description|Type|
|--|--|--|
|id|Id of the print request|UUID|
|status|Status of the print request|ENUM|
|message|Message from the print request|TEXT|
