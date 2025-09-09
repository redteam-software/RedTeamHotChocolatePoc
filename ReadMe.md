# RedTeam Hot Chocolate GraphQL Server 

This project is a demonstration of a GraphQL server built using the Hot Chocolate library in .NET. It showcases various features of GraphQL, including queries, mutations.

# Locally Running the Server

To run the server locally, follow these steps:
1. Ensure you have .NET 9 installed
1. Ensure you have a local instance of PostgresSQL running
1. Either create a account dev/password in postgres or update the connection string in appsettings.json for the Ordering and Products services.
1. Run the initialize-database.ps1 script in the solution root folder.
1. Select profile "All" and hit f5.   
1. If you aren't using Visual Studio, make sure you start the services in the following order:
   1. Products,
   1. Ordering,
   1. Gateway
1. Finally you should see a browser window open up at http://localhost:5098/graphql/



# Test Queries - Enter in Nitro Editor
Field Level Authorization
```
{
   orders(where:  {
      id:{
        eq: 1
      }
   }){
     id,
     name,
     description,
     totalLineItems
   }
}
```

Get Order By ID
```
{
  orders(where:  {
     id:{
      eq: 99
     }
  }){
     id,
     name,
     description,
     items{
       id,
       quantity,
       product{
         name, price, sku
       }
     }
  }
}
```

Query Pulling Data From Two APIs
```
{
  orders(where:{
     id:{
       gte:  25
     },
  }){
     id,
     items{
       quantity,
       product{
         name,
         description,
         sku,
          price
       }
     }
  }
}
```

Search Products With Paging

```
{
searchProducts(
last: 50,    
where:  {
   price:  {
      gt: 500
   }
})
{
 nodes{
    id,
     name,
     price
 }
}
}
```

# Test Mutations

Create A New Order
```
mutation {
   createOrder(orderInput:  {
      description: "An example of using a mutation through Fusion",
       id: 0,
        name: "example",
        linItems:  [
          {
             productId: 3,
             id: 0,
              quantity: 2 
          }
        ]

   }){
     order{
       id
     }
   }
}
```


