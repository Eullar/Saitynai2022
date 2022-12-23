# Rent-A-RaceCar - sportinių automobilių nuomos sistema

## Sistemos paskirtis
### Projekto tikslas
Palengvinti sportinių automobilių nuomos procesą, bei šių nuomos filialų valdymą.
### Veikimo principas
Sistema sudaryta iš dvejų dalių: internetinė svetainė, su kuria sistemos vartotojai, bei filialų darbuotojai sąveikaus, bei serverinė API (liet. Aplikacijos programavimo sąsaja).
Sistemos svečias galės peržiūrėti kuriose trasose yra nuomos filialai, bei kokie automobiliai yra šiose filialuose. Registruotas sistemos vartotojas gali užsisakyti automobilį dienai toje trasoje. Sistemos administratoriai yra filialo darbuotojai Jie gali pridėti naujus filialus, pridėti automobilius į jau esančius filialus, bei atšaukti užsakymus.

---

## Funkiciniai reikalavimai
### Sistemos svečias galės:
-  Peržiūrėti filialus
-  Peržiūrėti filialuose esančius automobilius
-  Registruotis į sistemą
### Registruotas sistemos vartotojas galės:
-  Atsijungti nuo sistemos
-  Prisijungti prie sistemos
-  Sukurti užsakymą:
    -  Pasirinkti filialą
    -  Pasirinkti vieną iš filiale esantį automobilį
    -  Patvirtinti užsakymą
-  Atšaukti savo užsakymus
### Sistemos administratorius galės:
- Sukurti naują filialą:
    - Įvesti trasos, kurioje yra filialas, pavadinimą
    - Įvesti vietą, kurioje bus filialas
- Pridėti automobilį į filialą:
    - Įvesti automobilio parametrus:
        - Gamintoją
        - Modelį
        - Transmisijos tipą
        - Varomuosius ratus
        - Padangų tipą
- Atšaukti vartotojų užsakymus

---
# Sistemos architektūra
### Frontend
React

### Backend
.NET 6.0 + EntityFramework

### Duomenų bazė
MySql

<img src="Images/Deployment.png" />

# Naudotojo sąsajos wireframe ir realizacija

## Pagrindinis puslapis
<img src="Images/MainPageWireframe.png" />
<img src="Images/MainPage.png" />

## Filialų puslapis
<img src="Images/RentOfficeWireframe.png" />
<img src="Images/RentOffice.png" />
<img src="Images/RentOfficeAdmin.png" />
<img src="Images/RentOfficeCreate.png">

## Automobilių puslapis
<img src="Images/CarsWireframe.png" />
<img src="Images/Cars.png" />
<img src="Images/CarsAdmin.png" />
<img src="Images/CarsCreate.png" />

## Užsakymų puslapis
<img src="Images/OrdersWireframe.png" />
<img src="Images/OrdersAdmin.png" />
<img src="Images/OrderCreate.png" />

---

# API specifikacija

| Registration | /register  |
| ----------- | ----------- |
| Response codes | 400 - bad request, 201 - Created |
| Method | POST |
| Parameters | username - string, email - string(email format), password - string |
| Authorization | No |
| Response | Json |

### Example request:
```
POST /register/
{
    "username": "Gintautas1",
    "Email": "Gintautas1@gmail.com",
    "password": "FastCar1!"
}
```
### Example response:
```
{
    "id": "0c0f23e7-e7aa-4bc7-8a50-f7118056e763",
    "username": "Gintautas1",
    "email": "Gintautas1@gmail.com"
}
```
---

| Login | /login  |
| ----------- | ----------- |
| Response codes | 400 - bad request, 200 - Ok |
| Method | POST |
| Parameters | username - string, password - string |
| Authorization | No |
| Response | Json |

### Example request:
```
POST /login/
{
    "username": "Gintautas1",
    "password": "FastCar1!"
}
```
### Example response:
```
{
    {
    "authenticationToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiR2ludGF1dGFzMSIsImp0aSI6IjczZTA5NGRlLWIxZjgtNDE1YS04MDhmLTAzNmM0MTg2OTgwYiIsInN1YiI6ImYwN2EzNWQ0LTRhOGItNDkyMi1iYmEzLWRiMTQ2NDJjMWRjMCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE2NzE3MzgwNTgsImlzcyI6IkdpblB1Y2lsYXVza2FzIiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.nmGZk1upixgmKVb_F5XSkHtmJb_DeqXQdzHRdM5HGpo",
    "roles": [
        "User"
    ]
}
}
```
---
| GetRentOffices | /RentOffices  |
| ----------- | ----------- |
| Response codes | 200 - Ok |
| Method | GET |
| Parameters |  |
| Authorization | No |
| Response | Json |

### Example request:
```
GET /RentOffices/
```
### Example response:
```
[
    {
        "id": "64cd35fa-3dd9-498b-bc63-5a734bb4e63f",
        "name": "Rent A RaceCar | Nemunas Ring",
        "location": "Nemunas Ring, Lithuania",
        "carCount": 2
    }
]
```
---
| GetRentOffice | /RentOffices/{id}  |
| ----------- | ----------- |
| Response codes | 200 - Ok, 404 - Not found |
| Method | GET |
| Parameters | id - UUID |
| Authorization | No |
| Response | Json |

### Example request:
```
GET /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f
```
### Example response:
```
{
    "id": "64cd35fa-3dd9-498b-bc63-5a734bb4e63f",
    "name": "Rent A RaceCar | Nemunas Ring",
    "location": "Nemunas Ring, Lithuania",
    "carCount": 2
}
```
---
| CreateRentOffice | /RentOffices/  |
| ----------- | ----------- |
| Response codes | 201 - Created, 401 - Unauthorized, 404 - Not found |
| Method | POST |
| Parameters | name - string, location - string |
| Authorization | Yes |
| Response | Json |

### Example request:
```
POST /RentOffices/

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY

Body:
{
    "Name": "RentARaceCar Spa",
    "Location": "Spa, Belgium"
}
```
### Example response:
```
{
    "id": "1dfde93c-623a-4323-a4e4-4330e25f985a",
    "name": "RentARaceCar Spa",
    "location": "Spa, Belgium",
    "carCount": 0
}
```
---
| UpdateRentOffice | /RentOffices/{id}  |
| ----------- | ----------- |
| Response codes | 200 - Ok, 401 - Unauthorized, 403 - Forbidden, 404 - Not found |
| Method | PUT |
| Parameters | id - UUID, name - string, location - string |
| Authorization | Yes |
| Response | Json |

### Example request:
```
PUT /RentOffices/1dfde93c-623a-4323-a4e4-4330e25f985a

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY

Body:
{
    "Name": "RentARaceCar Zolder",
    "Location": "Zolder, Belgium"
}
```
### Example response:
```
{
    "id": "1dfde93c-623a-4323-a4e4-4330e25f985a",
    "name": "RentARaceCar Zolder",
    "location": "Zolder, Belgium",
    "carCount": 0
}
```
---
| DeleteRentOffice | /RentOffices/{id}  |
| ----------- | ----------- |
| Response codes | 204 - No content, 401 - Unauthorized, 403 - Forbidden, 404 - Not found |
| Method | DELET |
| Parameters | id - UUID |
| Authorization | Yes |
| Response | Json |

### Example request:
```
DELETE /RentOffices/1dfde93c-623a-4323-a4e4-4330e25f985a

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY
```
### Example response:
```
```
---
| GetCars | /RentOffices/{rentOfficeId}/Cars  |
| ----------- | ----------- |
| Response codes | 200 - Ok |
| Method | GET |
| Parameters | rentOfficeId - UUID |
| Authorization | No |
| Response | Json |

### Example request:
```
GET /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/
```
### Example response:
```
[
    {
        "id": "9002c895-1436-4421-88d3-937020be4e51",
        "manufacturer": "Toyota",
        "model": "Mrs",
        "transmissionType": "Sequencial",
        "drivetrainType": "RR",
        "tyreCompound": "Slick",
        "rentOfficeId": "64cd35fa-3dd9-498b-bc63-5a734bb4e63f"
    },
    {
        "id": "f694f79c-b3d5-407f-a37e-e79d496f9afc",
        "manufacturer": "Volkswagen",
        "model": "Golf",
        "transmissionType": "Manual",
        "drivetrainType": "FF",
        "tyreCompound": "Road",
        "rentOfficeId": "64cd35fa-3dd9-498b-bc63-5a734bb4e63f"
    }
]
```
---
| GetCar | /RentOffices/{rentOfficeId}/Cars/{id}  |
| ----------- | ----------- |
| Response codes | 200 - Ok, 404 - Not found |
| Method | GET |
| Parameters | id - UUID |
| Authorization | No |
| Response | Json |

### Example request:
```
GET /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/9002c895-1436-4421-88d3-937020be4e51
```
### Example response:
```
{
    "id": "9002c895-1436-4421-88d3-937020be4e51",
    "manufacturer": "Toyota",
    "model": "Mrs",
    "transmissionType": "Sequencial",
    "drivetrainType": "RR",
    "tyreCompound": "Slick",
    "rentOfficeId": "64cd35fa-3dd9-498b-bc63-5a734bb4e63f"
}
```
---
| CreateCar | /RentOffices/{rentOfficeId}/Cars  |
| ----------- | ----------- |
| Response codes | 201 - Created, 401 - Unauthorized, 404 - Not found |
| Method | POST |
| Parameters | name - string, location - string |
| Authorization | Yes |
| Response | Json |

### Example request:
```
POST /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY

Body:
{
    "Manufacturer": "Toyota",
    "Model": "Mrs",
    "TransmissionType": 3,
    "DrivetrainType": 3,
    "TyreCompound": 2
}
```
### Example response:
```
{
    "id": "6f350872-83d7-4078-90aa-97deb55a8164",
    "manufacturer": "Toyota",
    "model": "Mrs",
    "transmissionType": 3,
    "drivetrainType": "RR",
    "tyreCompound": "Slick",
    "rentOfficeId": "64cd35fa-3dd9-498b-bc63-5a734bb4e63f"
}
```
---
| UpdateCar | /RentOffices/{rentOfficeId}/Cars/{carId}  |
| ----------- | ----------- |
| Response codes | 200 - Ok, 401 - Unauthorized, 403 - Forbidden, 404 - Not found |
| Method | PUT |
| Parameters | id - UUID, name - string, location - string |
| Authorization | Yes |
| Response | Json |

### Example request:
```
PUT /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/6f350872-83d7-4078-90aa-97deb55a8164

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY

Body:
{
    "Manufacturer": "Toyota",
    "Model": "Mrc",
    "TransmissionType": 3,
    "DrivetrainType": 3,
    "TyreCompound": 2
}
```
### Example response:
```
{
    "id": "6f350872-83d7-4078-90aa-97deb55a8164",
    "manufacturer": "Toyota",
    "model": "Mrc",
    "transmissionType": 3,
    "drivetrainType": "RR",
    "tyreCompound": "Slick",
    "rentOfficeId": "64cd35fa-3dd9-498b-bc63-5a734bb4e63f"
}
```
---
| DeleteRentOffice | /RentOffices/{rentOfficeId}/Cars/{carId}  |
| ----------- | ----------- |
| Response codes | 204 - No content, 401 - Unauthorized, 403 - Forbidden, 404 - Not found |
| Method | DELETE |
| Parameters | rentOfficeId - UUID, carId - UUID |
| Authorization | Yes |
| Response | Json |

### Example request:
```
DELETE /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/6f350872-83d7-4078-90aa-97deb55a8164

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY
```
### Example response:
```
```
---
| GetOrders | /RentOffices/{rentOfficeId}/Cars/{carId}/Orders  |
| ----------- | ----------- |
| Response codes | 200 - Ok |
| Method | GET |
| Parameters | rentOfficeId - UUID, cardId - UUID |
| Authorization | Yes |
| Response | Json |

### Example request:
```
GET /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/9002c895-1436-4421-88d3-937020be4e51/Orders

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY
```
### Example response:
```
[
    {
        "id": "889fcc32-3d0b-4673-b779-c744c1664276",
        "orderDate": "2022-12-19T08:31:11.063695",
        "rentDate": "2022-12-24T00:00:00",
        "userId": "bc15a827-fe10-4661-8150-90babc82a789",
        "carId": "9002c895-1436-4421-88d3-937020be4e51"
    },
    {
        "id": "e8a23dae-aa97-450e-91a3-6a20575ee231",
        "orderDate": "2022-12-19T10:19:18.121544",
        "rentDate": "2022-12-29T00:00:00",
        "userId": "26d05186-254e-425c-a4a1-3abfa1471e00",
        "carId": "9002c895-1436-4421-88d3-937020be4e51"
    }
]
```
---
| GetOrder | /RentOffices/{rentOfficeId}/Cars/{carId}/Orders/{orderId}  |
| ----------- | ----------- |
| Response codes | 200 - Ok, 404 - Not found |
| Method | GET |
| Parameters | rentOfficeId - UUID, cardId - UUID, orderId - UUID |
| Authorization | Yes |
| Response | Json |

### Example request:
```
GET /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/9002c895-1436-4421-88d3-937020be4e51/Orders/e8a23dae-aa97-450e-91a3-6a20575ee231

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY
```
### Example response:
```
{
    "id": "e8a23dae-aa97-450e-91a3-6a20575ee231",
    "orderDate": "2022-12-19T10:19:18.121544",
    "rentDate": "2022-12-29T00:00:00",
    "userId": "26d05186-254e-425c-a4a1-3abfa1471e00",
    "carId": "9002c895-1436-4421-88d3-937020be4e51"
}
```
---
| CreateOrder | /RentOffices/{rentOfficeId}/Cars/{carId}/Orders  |
| ----------- | ----------- |
| Response codes | 201 - Created, 401 - Unauthorized, 404 - Not found |
| Method | POST |
| Parameters | rentDate - Date |
| Authorization | Yes |
| Response | Json |

### Example request:
```
POST /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/9002c895-1436-4421-88d3-937020be4e51/Orders/

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY

Body:
{
    "RentDate": "2022-09-21"
}
```
### Example response:
```
{
    "id": "b0641460-0c3f-4b77-818c-48773c268c56",
    "orderDate": "2022-12-23T11:27:31.6394487+02:00",
    "rentDate": "2022-09-21T00:00:00",
    "userId": "bc15a827-fe10-4661-8150-90babc82a789",
    "carId": "9002c895-1436-4421-88d3-937020be4e51"
}
```
---
| UpdateOrder | /RentOffices/{rentOfficeId}/Cars/{carId}/Orders/{orderId}  |
| ----------- | ----------- |
| Response codes | 200 - Ok, 401 - Unauthorized, 403 - Forbidden, 404 - Not found |
| Method | PUT |
| Parameters | rentDate - Date |
| Authorization | Yes |
| Response | Json |

### Example request:
```
PUT /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/9002c895-1436-4421-88d3-937020be4e51/Orders/b0641460-0c3f-4b77-818c-48773c268c56

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY

Body:
{
    "RentDate": "2022-12-12"
}
```
### Example response:
```
{
    "id": "b0641460-0c3f-4b77-818c-48773c268c56",
    "orderDate": "2022-12-23T11:27:31.6394487+02:00",
    "rentDate": "2022-12-12T00:00:00",
    "userId": "bc15a827-fe10-4661-8150-90babc82a789",
    "carId": "9002c895-1436-4421-88d3-937020be4e51"
}
```
---
| DeleteOrder | /RentOffices/{rentOfficeId}/Cars/{carId}/Orders/{orderId}  |
| ----------- | ----------- |
| Response codes | 204 - No content, 401 - Unauthorized, 403 - Forbidden, 404 - Not found |
| Method | DELETE |
| Parameters | rentOfficeId - UUID, cardId - UUID, orderId - UUID |
| Authorization | Yes |
| Response | Json |

### Example request:
```
DELETE /RentOffices/64cd35fa-3dd9-498b-bc63-5a734bb4e63f/Cars/9002c895-1436-4421-88d3-937020be4e51/Orders/b0641460-0c3f-4b77-818c-48773c268c56

Header:
Bearer Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJqdGkiOiJlOTVkZDdkNy1kY2UwLTQyOWUtYTIzMi1lODVjODJiNjVkMjMiLCJzdWIiOiJiYzE1YTgyNy1mZTEwLTQ2NjEtODE1MC05MGJhYmM4MmE3ODkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTY3MTczOTUxNiwiaXNzIjoiR2luUHVjaWxhdXNrYXMiLCJhdWQiOiJUcnVzdGVkQ2xpZW50In0.M6DffrtwNICIutDvt4uHVlyhgxSJCoT1ht0Ue_8FFuY
```
### Example response:
```
```
---