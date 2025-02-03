# Employee API

API для управления сотрудниками с использованием PostgreSQL и Dapper.

## Описание

Это веб-приложение позволяет выполнять CRUD-операции над сотрудниками, паспортами и отделами. Проект использует следующие технологии:
- **ASP.NET Core** — для создания RESTful API.
- **PostgreSQL** — как базу данных.
- **Dapper** — ORM для работы с базой данных.
- **FluentMigrator** — для управления миграциями базы данных.

---

## Требования

1. **.NET SDK** версии 8.0 или выше.
2. **PostgreSQL** сервер (версия 12+).
3. **pgAdmin** или другой клиент PostgreSQL для просмотра базы данных (опционально).

---

## Настройка и запуск

1. **Клонируйте репозиторий:**
   ```bash
   git clone https://github.com/your-repository-url.git
   cd EmployeeApi
2.**Установите зависимости:**
  ```bach
  dotnet restore
3.**Настройте строку подключения:**
  Откройте файл appsettings.json и укажите параметры подключения к вашей базе данных PostgreSQL:
  ```json
  {
    "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Username=postgres;Password=yourpassword;Database=employee_db"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*"
  }
  Замените yourpassword и employee_db на актуальные данные.
4. **Примените миграции:**
  При первом запуске приложения миграции автоматически применяются. Если вы хотите применить их вручную, выполните следующую      команду:
  ```bach
  dotnet run --project .\Migrations\
5. **Запустите приложение:**
  ```bach
  dotnet run
