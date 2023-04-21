:start
dotnet build PocketGPT.Webapi --no-incremental --force
dotnet watch run -c Debug --project PocketGPT.Webapi
goto start