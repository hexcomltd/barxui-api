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
    "data": {
        "name": "John Doe",
        "address": "123 Main St",
        "city": "Anytown",
        "state": "CA",
        "zip": "12345"
    },
    "outputMode": "PDF",
    "outputParameters": {
        "filename": "label.pdf",
        "copies": 1,
        "collate": true,
        "columnFirst": false,
        "cropMarks": false,
        "flip": false,
        "mirror": false,
        "test": false
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
        "mirror": false,
        "test": false
    }
}
```

# Data

## Requests

<a name="PrintRequest" />

### PrintRequest

|Property|Description|Type|
|--|--|--|
|id|An id for the print request.  Can be used to fetch result later.|UUID|
|labelId|Id of the label to print|UUID|
|data|Data to print on the label|Data Dictionary (see below)|
|outputMode|"PDF", "PNG", "PRINT"|ENUM|
|outputParameters|Options relative to the outputMode|Output Parameters Dictionary (see below)|

### Data Parameter

Data is a dictionary of key/value pairs.  The key is the name of the field in the label template.  The value is the value to print in the label.

### Output Parameters 

All output parameter are defied as key/value pairs.

All options include these common parameters:

|Property|Description|Type|
|--|--|--|
|copies|The number of copies of each label to print|INT|
|collate|Collate copies of labels when label define multiple labels per page |BOOL|
|columnFirst|Print down each column when label define multiple labels per page |BOOL|
|cropMarks|Print crop marks are the corner of each page||
|flip|Flip the labels vertically|BOOL|
|mirror|Flip the label horizontally|BOOL|
|test|Print a single label to test.  Disables increment of serial numbers.|BOOL|

#### PDF

Additional parameters:

|Property|Description|Type|
|--|--|--|
|filename|Name of the output file|TEXT|

#### PNG

Additional parameters:

|Property|Description|Type|
|--|--|--|
|filename|Name of the output file|TEXT|

#### Printer

|Property|Description|Type|
|--|--|--|
|printerId|ID of the printer|UUID|

## Responses

<a name="PrintResponse" />

### PrintResponse

|Property|Description|Type|
|--|--|--|
|id|Id of the print request|UUID|
|status|Status of the print request|ENUM|
|message|Message from the print request|TEXT|
