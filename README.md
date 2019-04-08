This application is a composite microservice sample that simulates the communication through the Eventbus(Masstransit   RabbitMQ) to present shopping, ordering and payment transactions and, that caches the basket information on Redis instantly.

Please, start the applications in the specified order:

* dotnet run .src / Basket.API / Basket.API.csproj
* dotnet run .src / Shop / Shop.csproj
* dotnet run .src / Ordering / Ordering.csproj
* dotnet run .src / Payment / Payment.csproj