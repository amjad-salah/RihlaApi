# Rihla API Backend

## Models

#### Country

`Id int not null` PK

`Name string not null`

`CountryCode string not null`

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### City

`Id int not null` PK

`Name string not null`

`CountryId id not null` FK => Country

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### Company

`Id int not null` PK

`Name string not null`

`Address string not null`

`PhoneNo string not null`

`Email string`

`CountryId id not null` FK => Country

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### Driver

`Id int not null` PK

`Name string not null`

`LicenseNo string not null`

`PhoneNo string not null`

`CompanyId id not null` FK => Country

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### Vehicle

`Id int not null` PK

`Model string not null`

`PlateNo string not null`

`VehicleType string not null` => (شاحنة، ركاب)

`NoOfSeats int`

`TotalWeight int`

`CompanyId id not null` FK => Country

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### Journey

`Id int not null` PK

`DepTime DateTime not null`

`ArrTime DateTime not null`

`DepCityId int not null` FK => City

`ArrCityId int not null` FK => City

`DriverId int not null` FK => Driver

`VehicleId int not null` FK => Vehicle

`CompanyId id not null` FK => Country

`JourneyType string not null` => (شحن، ركاب)

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### Reservation

`Id int not null` PK

`CustomerName string not null`

`PhoneNo string not null`

`NoOfSeats int`

`Weight int`

`JourneyId id not null` FK => Journey

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### User

`Id int not null` PK

`FullName string not null`

`Username string not null`

`Password string not null`

`Email string`

`PhoneNo string not null`

`Roles string not null` => (Admin, Manager, User, Customer)

`CompanyId id not null` FK => Country

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

## Endpoints

#### Countries

`GET /api/countries  Get All Countries`

`POST /api/countries  Create New Country`

`GET /api/countries/:id  Get Single Country By Id`

`PUT /api/countries/:id  Update Single Country By Id`

`DELETE /api/countries/:id  Delete Single Country By Id`

#### Cities

`GET /api/cities  Get All Cities`

`POST /api/cities  Create New City`

`GET /api/cities/:id  Get Single City By Id`

`PUT /api/cities/:id  Update Single City By Id`

`DELETE /api/cities/:id  Delete Single City By Id`

#### Companies

`GET /api/companies  Get All Companies`

`POST /api/companies  Create New Company`

`GET /api/companies/:id  Get Single Company By Id`

`PUT /api/companies/:id  Update Single Company By Id`

`DELETE /api/companies/:id  Delete Single Company By Id`

#### Drivers

`GET /api/drivers  Get All Drivers`

`POST /api/drivers  Create New Driver`

`GET /api/drivers/:id  Get Single Driver By Id`

`PUT /api/drivers/:id  Update Single Driver By Id`

`DELETE /api/drivers/:id  Delete Single Driver By Id`

#### Vehicles

`GET /api/vehicles  Get All Vehicles`

`POST /api/vehicles  Create New Vehicle`

`GET /api/vehicles/:id  Get Single Vehicle By Id`

`PUT /api/vehicles/:id  Update Single Vehicle By Id`

`DELETE /api/vehicles/:id  Delete Single Vehicle By Id`

#### Journeys

`GET /api/journeys  Get All Journeys`

`POST /api/journeys  Create New Journey`

`GET /api/journeys/:id  Get Single Journey By Id`

`PUT /api/journeys/:id  Update Single Journey By Id`

`DELETE /api/journeys/:id  Delete Single Journey By Id`
