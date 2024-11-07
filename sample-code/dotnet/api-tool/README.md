
# Example dotnet api client

This sample application can be used to demonstrate how to use the barxui api client.

It is also usefull as simple command line tool to list objects and print labels.

## Build

- requires dotnet 8 or later
- update the `appsettings.json` file with your api key
- build the project

    ```bash
    dotnet build ./api-tool.csproj
    ```

## Usage

Format:

```
    dotnet run -- <command> [options]
```
or if using the compiled version:

```
    ./api-tool <command> [options]
```

Commands:

```
  list              List objects
  print             Print a label
  help              Display more information on a specific command.
  version           Display version information.
```

- List Options:

```
  -t, --type        Required. The type of object to list - AGENT, PRINTER, LABEL.
  -p, --page        (Default: 0) The page of objects
  -s, --pageSize    (Default: 10) The number of objects per
```  

- Print Options:

```
  -l, --label       Required. The label to print - name or ID
  -d, --data        The data to send - file in json format.
  -i, --inputs      The inputs to send - file in json format.
  -q, --query       The query parameters to send - file in json format.
  -o, --output      The output destination - PDF, PNG or a printer name or printer id.
  -c, --copies      (Default: 1) The number of copies to print.
  -f, --filename    Output filename if not printing.  File extension will be added.
```

Examples:

- list agents

```
dotnet run -- list -t Agents -p 0 -s 10
```

- list printers
    
```
dotnet run -- list -t Printers -p 0 -s 10
```

- list labels

```
dotnet run -- list -t Labels -p 0 -s 10
```

- print label no data or inputs or query parameters 
    
```
dotnet run -- print -l <label-id> -o <printer name or printer id or PDF or PNG> -f <filename is output is to PDF or PNG>
```

- print label "Production" that includes variables called "Operator" and "Batch" that can be overridden with the following, output to PDF

input.json
```
{
    "inputs": {
        "Operator": "Steve",
        "Batch": "1234"
    }
}
```

    
```
dotnet run -- print -l "Production" -o PDF -i input.json -f ProductionLabels
```

_Note: output file name should not include the file extension, it will be added automatically._
