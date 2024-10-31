# Rihal API Backend

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

`CountrId id not null` FK => Country

`CreatedAt DateTime auto generated`

`UpdatedAt DateTime auto generated`

#### Company

`Id int not null` PK

`Name string not null`

`Address string not null`

`PhoneNo string not null`

`Email string`

`CountrId id not null` FK => Country

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

`VehiclType string not null` => (شاحنة، ركاب)

`NoOfSeats int`

`TotalWegiht int`

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


