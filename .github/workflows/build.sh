dotnet build easyRBAC/easyRBAC.sln --configuration Release
dotnet publish .\easyRBAC\src\EasyRbac.Web\ -c Release  --self-contained -r linux-x64
rm -f ./easyRBAC/src/EasyRbac.Web/bin/Release/netcoreapp2.2/linux-x64/publish/appsettings.*
rm -f ./easyRBAC/src/EasyRbac.Web/bin/Release/netcoreapp2.2/linux-x64/publish/*.pdb
tar -czvf easyRBAC.tar.gz easyRBAC/src/EasyRbac.Web/bin/Release/netcoreapp2.2/linux-x64/publish