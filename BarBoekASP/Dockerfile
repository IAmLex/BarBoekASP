#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM microsoft/aspnetcore-build AS builder
 WORKDIR /source
 EXPOSE 80
 EXPOSE 443

 # caches restore result by copying csproj file separately
 COPY *.csproj .
 RUN dotnet restore

 # copies the rest of your code
 COPY ["BarBoekASP/BarBoekASP.csproj", "BarBoekASP/"]
 RUN dotnet publish --output /app/ --configuration Release

 # Stage 2
 FROM microsoft/aspnetcore
 WORKDIR /app
 COPY --from=builder /app .
 ENTRYPOINT ["dotnet", "BarBoekASP.dll"]
