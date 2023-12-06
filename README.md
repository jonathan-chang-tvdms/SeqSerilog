# SeqSerilog
Demo setup for .Net backend added with Seq and Serilog

## Pre-requisites
- Docker (Optional)
- Nuget Packages

| Name | Version |
| :---: | :---: |
| Seq.Extensions.Logging |　LTS |
| Serilog.AspNetCore | LTS |
| Serilog.Sinks.Console | LTS |
| Serilog.Sinks.Seq | LTS |

## Basic Concept
- Using Serilog provide structured logging service
  - No longer in plain(pain) text format
- Seq provide dashboard and UI for easy Serilog management

- - -

## Installation guide
Note: Docker is not a must, just for quick demo on container environment


### Step 1: Install the pre-requisites
Just install.


### Step 2: Add codes to the start up **programme.cs**

```
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSeq());
```

```
app.UseSerilogRequestLogging();
```

### Step 3: Add Serilog configs to **appsettings.json**

```
{
  ...

  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.Seq" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Information"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "Seq",
        "Args": {
          "serverUrl": "http://ss-seq:5341"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  }
}
```

### Step 4: (Optional) Add Seq to **docker-compose.yml**

```
services:
  ...

  seq:
    image: datalust/seq:latest
    container_name: ss-seq
    environment: 
        - ACCEPT_EULA=Y
    ports:
        - 5341:5341
        - 8081:80  
```

- - - 

## Usage
- Basic Format

```
_logger.LogInformation("You are qualified dumb: {Name}", object.name);
```
With this format, you can further query the log with ` Name = 'You' ` as the filter

- Serialized Object
For a object with this
```
{
  dumb: true,
  iq: 10
}
```

You can do this
```
_logger.LogInformation("You are {@You}", you)
```
and the resulting log will be like this
```
You are { dumb: true, iq: 10}
```
And you can query your iq with ` You.iq = 10 `

- Array object
Same same, not interested to write


## Seq dashboard
Go here
http://localhost:8081
