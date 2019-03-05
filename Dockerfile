FROM microsoft/dotnet:sdk AS build-env
WORKDIR /src

COPY ./Handy.Domain/Handy.Domain.csproj ./Handy.Domain/
RUN dotnet restore ./Handy.Domain/

COPY ./Handy.Infrastructure/Handy.Infrastructure.csproj ./Handy.Infrastructure/
RUN dotnet restore ./Handy.Infrastructure/

COPY ./Handy.Bot/Handy.Bot.csproj ./Handy.Bot/
RUN dotnet restore ./Handy.Bot/

COPY ./Handy.App/Handy.App.csproj ./Handy.App/
RUN dotnet restore ./Handy.App/

COPY . .

RUN dotnet publish ./Handy.App/ -c Release -o /out

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /out .
ENTRYPOINT [ "dotnet", "Handy.App.dll" ]