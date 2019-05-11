Docker support linux
right click docker => settings->Deamon- experimental on (proabably not necessary)

from \SurveyWeb\InterviewPlay
docker-compose build
docker images
docker run -p 4000:80 <success image repository>

http://localhost:4000/

docker kill $(docker ps -q)
docker rmi -f $(docker images -q)


{Both following files in run from same place as .sln file}
docker-compose.yml
<------->

version: '3'
services:
  web:
    build: .
    ports:
     - "5000:5000"

<-------->

Dockerfile
<--------------->

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

<----------->




