FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /Projektas/RentARaceCar

# Copy everything
COPY /Projektas/RentARaceCar/RentARaceCar/. ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /Projektas/RentARaceCar
COPY --from=build-env /Projektas/RentARaceCar/out .
ENTRYPOINT ["dotnet", "RentARaceCar.dll"]