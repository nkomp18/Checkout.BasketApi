# Checkout.BasketApi (Part 1)

BasketApi is Checkouts new prototype that allows customers to manage a basket of items. 

Use cases
- The API allows users to set up and manage an order of items.  
- The API allows users to add and remove items
- The API allows users to change the quantity of the items they want.  
- The API allows users to clear out all items from their order and start again.

There is no other functionality provided by the API other than the basket management.
There is no database - all values are stored in memory.

#  Checkout.BasketApi.HttpClient (Part 2)

`BasketApi.HttpClient` is a client library that makes use of the API endpoints exposed by BasketApi
It can be easily imported into projects to make use of the BasketApi.
It provides both synchronous and asynchronous methods.

# Technology
`Checkout.BasketApi` and `Checkout.BasketApi.HttpClient` run on .NET Core 2.1 SDK

# Instructions
After cloning the repository, and on a command prompt, run `dotnet build` to build the project. 
Once the build completes, the API can be started by running the produced dll in the command prompt.
```
dotnet Checkout.BasketApi/bin/Debug/netcoreapp2.1/Checkout.BasketApi.dll
```
The host and port will be displayed in the output and are currently set to `localhost:2000`. 

# Swagger
This API uses swagger which can be reach by nagivating to `/swagger`.
This acts both as a documentation but also as a front end to use the API.

# Assumptions
- User exists and has permissions to use the API. Any user will work
- There are no limits to the quantity or number of the items in the basket
- Only 1 basket per User. The quantities of duplicate items will aggregate

# Future Improvements
- User authentication (JWT) + Authorization policies
- Idempotency checks using a CorrelationId
- Audit tables for action auditability
- Adding a retry framework like Polly
- BDD acceptance testing against the use cases
- Versioning
- Better logging (Serilog or log4Net)
- The client should be a Nuget package for quick distribution