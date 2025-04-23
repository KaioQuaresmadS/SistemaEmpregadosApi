# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ✅ Corrigido aqui: caminho direto ao .csproj na raiz
COPY ["EmpregadoApi.csproj", "./"]
RUN dotnet restore "EmpregadoApi.csproj"

# Copia todos os arquivos
COPY . .

# ✅ Não precisa mudar de pasta, pois já está na raiz
RUN dotnet build "EmpregadoApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica a build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EmpregadoApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmpregadoApi.dll"]
