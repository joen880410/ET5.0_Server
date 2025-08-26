cd %cd%/DBView/netcoreapp3.1

dotnet build -o ../netcoreapp3.1 ../../Server/DBViewGenerator/Server.DBViewGenerator.csproj
del "..\..\Server\Model\Module\DB\DBView.cs"
del "..\..\Server\Model\Module\DB\DBViewCommand.cs"
dotnet Server.DBViewGenerator.dll

pause