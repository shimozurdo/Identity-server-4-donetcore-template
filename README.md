# Identity Server 4, .NET Core 3.1 and MySql

## Add a connection string in “appsettings.json” 

Create a database and add the corresponding connection string.

`   "ConnectionStrings": {
		""DefaultConnection": "Server=localhost;port=3306;Database=<DATABASE>;Uid=<USER>;Pwd=<PASSWORD>;CharSet=utf8;Allow User Variables=True;""
	}`

## Add Migrations


Add the corresponding migrations.

- `Add-Migration InitialApplicationDbContext -c ApplicationDbContext -o Data/Migrations/IdentityServer/ApplicationDb`
- `Add-Migration InitialPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb`
- `Add-Migration InitialConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb`

## Update Migrations

- Update-Database -Context ApplicationDBContext
- Update-Database -Context PersistedGrantDbContext
- Update-Database -Context ConfigurationDbContext

*Maybe you need to use `EntityFrameworkCore\` before Add/Update-Migration

## Run the project

You should see on this url:

[https://localhost:5001/.well-known/openid-configuration](https://localhost:5001/.well-known/openid-configuration)

this json:

![](https://res.cloudinary.com/shimozurdo/image/upload/v1607555316/markdown/well-known_hsvszh.png)

*It may also be the port :5000, depending on the configuration of your project.
### Source

https://deblokt.com/2020/01/24/02-identityserver4-entityframework-net-core-3-1/
 
