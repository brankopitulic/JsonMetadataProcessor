# JsonMetadataProcessor
Steps to run application.

Necessary: We are using PostgreSQL, so you need to have installed DBeaver or PGAdmin.

1. Create connection in DBeaver/PGAdmin
Use configuration:
Host: localhost
port: 5432
DB name: ClinicalTrialsDB
username: postgres
password: postgres

2. Open JsonMetadataProcessor solution file in Visual Studio and Build application

3. Run application by running Docker Compose

4. Open WindowsPowerShell and locate directory \JsonMetadataProcessor>

5. Run init migration to create table
dotnet ef migrations add InitialCreate -p Infrastructure -s WebApi
dotnet ef database update -p Infrastructure -s WebApi

6. Open swaggerUI for testing APIs
http://localhost:8080/swagger/index.html
or
https://localhost:8081/swagger/index.html

JSON file test example

{
    "trialId": "CT-2024-001",
    "title": "A Study on the Effects of New Medication X",
    "startDate": "2024-01-15",
    "endDate": "2024-04-15",
    "participants": 150,
    "status": "Ongoing"
  }


