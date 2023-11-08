# Basket API

## Project Description

This project is a simple representation of an e-commerce basket api which uses redis cache as database and runs on docker containers with just one line of command.

The api has two endpoints which are for adding product to basket and getting basket data.

For simplicity, only stock validation has implemented. User and product validation has not been added. 

Again, for simplicity, stock validation is implemented as a mock service as if it's another api endpoint.

It is assumed that there are 5 products in stock with product ids from 1 to 5. Stock information is stored as hard coded in MockStockService


## Requirements

- Docker

## Run The Project

At the directory which includes docker-compose.yml:

```
docker-compose up -d
```

## How to Use The API

Root url: **localhost:5000**

### Add Product
- HTTP Method: **POST**
- Endpoint: **/basket/**
- Request body example:
```
{
  "userId": 1,
  "productId": 1,
  "productName": "socks",
  "quantity": 2
}
```
- Response example: 

```

{
  "userId": 1,
  "items": [
    {
      "productId": 2,
      "productName": "medium t-shirt",
      "quantity": 2
    },
    {
      "productId": 1,
      "productName": "socks",
      "quantity": 3
    }
  ]
}
```


### Get basket
- HTTP Method: **GET**
- Endpoint with query: **/basket/?userId=1**
- Response example: 

```

{
  "userId": 1,
  "items": [
    {
      "productId": 2,
      "productName": "medium t-shirt",
      "quantity": 2
    },
    {
      "productId": 1,
      "productName": "socks",
      "quantity": 3
    }
  ]
}
```



