# scripts/setup-workspace.ps1
Write-Host "Création de l'arborescence..."
New-Item -ItemType Directory -Force -Path "src", "tests", "scripts", "doc", "specs"

Write-Host "Création de la solution Catamailer..."
dotnet new sln -n Catamailer --force

Write-Host "Création des projets source..."
dotnet new classlib -n Catamailer.Domain -o src/Catamailer.Domain -f net10.0 --force
dotnet new classlib -n Catamailer.Application -o src/Catamailer.Application -f net10.0 --force
dotnet new classlib -n Catamailer.Infrastructure -o src/Catamailer.Infrastructure -f net10.0 --force
dotnet new maui-blazor -n Catamailer.UI -o src/Catamailer.UI -f net10.0 --force
dotnet new console -n Catamailer.Migrator -o src/Catamailer.Migrator -f net10.0 --force

Write-Host "Ajout des projets à la solution..."
dotnet sln add src/Catamailer.Domain/Catamailer.Domain.csproj
dotnet sln add src/Catamailer.Application/Catamailer.Application.csproj
dotnet sln add src/Catamailer.Infrastructure/Catamailer.Infrastructure.csproj
dotnet sln add src/Catamailer.UI/Catamailer.UI.csproj
dotnet sln add src/Catamailer.Migrator/Catamailer.Migrator.csproj

Write-Host "Création des projets de tests (xUnit)..."
dotnet new xunit -n Catamailer.Domain.Tests -o tests/Catamailer.Domain.Tests -f net10.0 --force
dotnet new xunit -n Catamailer.Application.Tests -o tests/Catamailer.Application.Tests -f net10.0 --force
dotnet new xunit -n Catamailer.Infrastructure.Tests -o tests/Catamailer.Infrastructure.Tests -f net10.0 --force

Write-Host "Ajout des projets de tests à la solution..."
dotnet sln add tests/Catamailer.Domain.Tests/Catamailer.Domain.Tests.csproj
dotnet sln add tests/Catamailer.Application.Tests/Catamailer.Application.Tests.csproj
dotnet sln add tests/Catamailer.Infrastructure.Tests/Catamailer.Infrastructure.Tests.csproj

Write-Host "Ajout des références projets (Clean Architecture)..."
# Tests
dotnet add tests/Catamailer.Domain.Tests/Catamailer.Domain.Tests.csproj reference src/Catamailer.Domain/Catamailer.Domain.csproj
dotnet add tests/Catamailer.Application.Tests/Catamailer.Application.Tests.csproj reference src/Catamailer.Application/Catamailer.Application.csproj
dotnet add tests/Catamailer.Infrastructure.Tests/Catamailer.Infrastructure.Tests.csproj reference src/Catamailer.Infrastructure/Catamailer.Infrastructure.csproj

# Core Métier
dotnet add src/Catamailer.Application/Catamailer.Application.csproj reference src/Catamailer.Domain/Catamailer.Domain.csproj
dotnet add src/Catamailer.Infrastructure/Catamailer.Infrastructure.csproj reference src/Catamailer.Application/Catamailer.Application.csproj

# Présentation & Outillage
dotnet add src/Catamailer.UI/Catamailer.UI.csproj reference src/Catamailer.Infrastructure/Catamailer.Infrastructure.csproj
dotnet add src/Catamailer.UI/Catamailer.UI.csproj reference src/Catamailer.Application/Catamailer.Application.csproj
dotnet add src/Catamailer.Migrator/Catamailer.Migrator.csproj reference src/Catamailer.Infrastructure/Catamailer.Infrastructure.csproj

# Outillage
dotnet new console -n AiDocGenerator -o src/Tools/AiDocGenerator -f net10.0 --force
dotnet new xunit -n Tools.AiDocGenerator.Tests -o tests/Tools.AiDocGenerator.Tests -f net10.0 --force
dotnet sln add src/Tools/AiDocGenerator/AiDocGenerator.csproj
dotnet sln add tests/Tools.AiDocGenerator.Tests/Tools.AiDocGenerator.Tests.csproj
dotnet add tests/Tools.AiDocGenerator.Tests/Tools.AiDocGenerator.Tests.csproj reference src/Tools/AiDocGenerator/AiDocGenerator.csproj

Write-Host "Workspace initialisé avec succès."