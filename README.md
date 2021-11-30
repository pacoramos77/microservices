

### Platform Service
Sql Server
Publish to message bus

### Command Service
In-Memory
Subscribe to message bus

### API Gateway


### Message Bus
RabbitMQ


### Starting code
```cmd

dotnet new webapi -n PlatformService
cd PlatformService
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

```