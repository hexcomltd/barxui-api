```
 _                             _ 
| |                           (_)
| |__  _____  ____ _   _ _   _ _ 
|  _ \(____ |/ ___| \ / ) | | | |
| |_) ) ___ | |    ) X (| |_| | |
|____/\_____|_|   (_/ \_)____/|_|

### API Access  Documentation ###

```

barxui is a cloud based solution for label design and management, supporting data merge and highspeed local printing.

The API provides a connection to the barxui server to allow printing directly from any application that can make HTTP requests.

This repository contains documentation and examples to get started.

### Getting Started

1. To access the API you will need to sign-up to barxui.  Navigate to https://app.barxui.com to create your account.

2. Setup your account and create some labels.

3. From the Organisation Settings page view the API Key tab and create a new key.

4. Save the key to your local vault or secure store.

__IMPORTANT! Anyone with this key will be able to access the data in your account__

5. Use the key to authenticate requests with Basic Authorization.

This key can be regenerated at any time but only the most recent version of the key is valid.

### API

* [General Information](/api/general.md)

#### Endpoint Definitions

* [Label](/api/label.md) - List labels definitions or fetch a single label.
* [Agent](/api/agent.md) - List agents and printers.
* [Print](/api/print.md) - Print a label to and agent or download as a file (PDF or PNG (single or zipped) supported)
