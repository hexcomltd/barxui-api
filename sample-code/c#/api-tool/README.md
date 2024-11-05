
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

- list agents

    ```bash
    dotnet run list -t Agents
    ```

- list printers
    
    ```bash
    dotnet run list -t Printers
    ```

- list labels

```bash
dotnet run list -t Labels
```

- print label  (coming soon)

