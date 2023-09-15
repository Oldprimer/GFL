# Test task for GFL .NET Cources
Web application that able to display hierarchical directory structure with Import/Export of hierarchy via JSON file.
Used dependecies: `Newtonsoft.JSON`, `SQLClient`, `Dapper`.
## Start
Use prepared .sql files inside [SQL]("https://github.com/Oldprimer/GFL/tree/main/sql") folder to create table with proper columns and fill it with data.
To fill database you can also use `Data.json` file with Import funtion.


Add dependecies with `NuGet`.
	- GFL: `Newtonsoft.JSON`;
	- GFL.Data: `SQLClient`, `Dapper`;

Add your connection string at `Appsettings.json`.

##Images

![Table](/img/Table.png)

![Index](/img/Index.png)

![Folder](/img/Folder.png)