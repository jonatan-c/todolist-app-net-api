# API ToDoList
API ToDoList es una API creada en ASP.NET Web API para el manejo de tareas. Esta API ofrece una solución sencilla para administrar tareas, permitiendo a los usuarios crear, leer, actualizar y eliminar (CRUD) tareas de una lista de tareas. Esta API no tiene autenticación.

## Instalación
Para instalar la API ToDoList, siga estos pasos:

1.Clone el repositorio:
```
git clone <repository>
```

2. Instalar las dependencias necesarias mediante el siguiente comando:
```
dotnet restore
```

3. Configure la base de datos, cambiando los parámetros de conexión en el archivo appsettings.json .

4. Ejecute la API en su servidor web o en local

```
dotnet run
```


generate a file 'appsettings.Development.json' in the root of the project

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "BankConnection": "Server="your server",port;User="your user";Password="your password";DataBase="name database";Trusted_connection=false;TrustServerCertificate=true"
  }
}


```