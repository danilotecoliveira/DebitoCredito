FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["DebitoCredito.Aplicacao/DebitoCredito.Aplicacao.csproj", "DebitoCredito.Aplicacao/"]
RUN dotnet restore "DebitoCredito.Aplicacao/DebitoCredito.Aplicacao.csproj"
COPY . .
WORKDIR "/src/DebitoCredito.Aplicacao"
RUN dotnet build "DebitoCredito.Aplicacao.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DebitoCredito.Aplicacao.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DebitoCredito.Aplicacao.dll"]