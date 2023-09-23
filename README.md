
# CAR PARK PROJECT

The aim of the project is to add and remove a new vehicle from an existing parking lot.

## API Kullanımı

#### To open the parking lot to add or remove the vehicles
```http
  GET https://localhost:xxx/api/carpark
  (xxxx => port number )
```
#### To close the parking lot to not be able to add or remove the vehicles
```http
  GET https://localhost:xxx/api/carpark
  (xxxx => port number )
```



#### To get all vehicles in the parking lot

```http
  GET https://localhost:xxx/api/carpark
```


#### To list all vehicles with 1st class

```http
  GET https://localhost:xxx/api/carpark/class1
```
#### To list all vehicles with 2nd class

```http
  GET https://localhost:xxx/api/carpark/class2
```

#### To list all vehicles with 3rd class

```http
  GET https://localhost:xxx/api/carpark/class3
```
#### To add a new vehicle

```http
  POST https://localhost:xxx/api/carpark

  Json raw example to add first class vehicle:
{     
    "class": 1,
    "colour": "black",
    "licensePlate": "34 RE 1234",
    "modelYear": 2013,
    "modelName": "POLO",
    "autoPilot": true,
    "price": 3058000,
    "spareTyre": true,
    "carWash": true (it can be false too),
    "kilowatt": 100,
}

Json raw example to add second class vehicle:
{     
    "class": 2,
    "colour": "black",
    "licensePlate": "34 DRE 123",
    "modelYear": 2013,
    "modelName": "Passat",
    "spareTyre": true,
    "kilowatt": 100,
    "luggageCappacity": 300,
    "spareTyre": true,
    "tireChange": true (it can be false too),
}

Json raw example to add third class vehicle:
{     
    "class": 3,
    "colour": "white",
    "licensePlate": "16 E 12",
    "modelYear": 2017,
    "modelName": "Golf",
    "kilowatt": 150,
}

```
#### To delete the vehicle by id and to see the fee the vehicle will pay

```http
  DELETE https://localhost:xxx/api/carpark/{id}


```
#### To see the HP (horsepower) of the vehicle whose id is specified

```http
  GET https://localhost:xxx/api/carpark/hourse-power/{id}
```