FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src

RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

COPY ["InterviewPlay/InterviewPlay.csproj", "InterviewPlay/"]
RUN dotnet restore "InterviewPlay/InterviewPlay.csproj"
COPY . .
WORKDIR "/src/InterviewPlay"
RUN dotnet build "InterviewPlay.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "InterviewPlay.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "InterviewPlay.dll"]