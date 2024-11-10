# Endpoint: Agent

Get a list of agents.

## POST agent/list

List the agents.

### Parameters

|Parameter|Description|Type|
|--|--|--|
|page|Page of agent list|INT|
|pageSize|Number of records per page.  Max 20.|INT|

### Examples

```
GET {{host}}/agent/list?page={{page}}&pageSize={{pageSize}}
Accept: application/json
Authorization: Token {{apiKey}}
```

### Response

|Status Code|Data|
|--|--|
|OK|PagedResponse of [Agent](#AgentResponse) |
|NotFound|no body|

## GET agent/{{id}}

Get a single agent

### Parameters

|Parameter|Description|Type|
|--|--|--|
|id|Id of the agent|UUID|

### Examples

```
GET {{host}}/agent/{{id}}
Accept: application/json
Authorization: Token {{apiKey}}
```

### Response

|Status Code|Data|
|--|--|
|OK|[Agent](#AgentResponse) |
|NotFound|no body|

# Data

## Responses

<a name="AgentResponse" />

### Agent

|Property|Description|Type|
|--|--|--|
|id|Unique ID of Agent|UUID|
|name|Name of the Agent|TEXT|
|hostName|Host name where Agent is installed|TEXT|
|createDate|Date Agent was first installed |DATE|
|updateDate|Date Agent was last updated. Null if not udpated |DATE|
|installedVersion|Version of installed Agent|TEXT|

#### Examples

```
{
  "id": "3e4126af-acb0-4ca0-b56e-ab9f8bbffa0e",
  "name": "Agent 1",
  "hostName": "MYSERVER",
  "createDate": "2021-03-09T11:28:54.1933496+13:00",
  "installedVersion": "5.1.0.1"
}
```
