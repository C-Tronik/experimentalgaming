CREATE TABLE `slot`.`userinfo` ( `UserId` INT NOT NULL AUTO_INCREMENT , `firstname` VARCHAR(30) NOT NULL , 
`lastnamename` VARCHAR(30) NOT NULL , 
`username` VARCHAR(30) NOT NULL , `email` VARCHAR(50) NOT NULL , `password` VARCHAR(20) NOT NULL , 
`createdate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP , `score` INT NOT NULL DEFAULT '100' , 
PRIMARY KEY (`UserId`)) ENGINE = MyISAM; 


This package helps create database context and model classes from the database.
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.0

It provides support for creating and validating a JWT token.
Install-Package IdentityModel.Tokens.Jwt -Version 5.6.0

This is the middleware that enables an ASP.NET Core application to receive a bearer token in the request pipeline.
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 3.1.0

Pomelo.EntityFrameworkCore.MySql connettore entity per mysql

Npgsql.EntityFrameworkCore.PostgreSQL connettore postgres -----> aggiungere nel--> configservices() -->in startup.cs
---->services.AddDbContext<slotContext>(options =>
            options.UsePostgreSQL(Configuration.GetConnectionString("MysqlConnection")));

Chiama endpoin api/token e usare il token per accedere alle route senno togliere [Authorized] nel controller 




