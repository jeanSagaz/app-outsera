## Tecnologias implementadas:

- ASP.NET Core 8.0
- .NET Core Native DI
- Web Api
- Domain Notification
- Entity Framework Core - InMemory
- CsvHelper
- FluentValidation
- xunit
- Testes de Integração

## Executando o projeto 'Web Api':

1. DockerFile  
Na pasta raiz do projeto onde está a solution 'sln', execute o comando:  
docker-compose up -d --build  
Após finalizar o comando e ser executado com sucesso pode visualizar as rotas da aplicação pelo swagger:  
http://localhost:8080/swagger/index.html

2. Rodando a aplicação local:  
Na pasta raiz do projeto onde está a solution 'GoldenRaspberryAwards.sln'. Após abrir o projeto selecionar 'Services.Api' como 'Set as Startup Project'.  
Selecionar a opção 'https' vai exibir as rotas da aplicação pelo swagger:
https://localhost:7227/swagger/index.html

3. Testando a aplicação:  
Na pasta 'src/Services.Api' tem o arquivo 'Services.Api.http' pode ser usado para testar os endpoints da aplicação dependendo do modo como for executar  deve-se alterar o protocolo e a porta.

## Executando o projeto de 'Testes de Integração':

Na pasta raiz do projeto onde está a solution 'sln', execute o comando:  
dotnet test  ./tests/WebApp.Tests/WebApp.Tests.csproj

## Documentação Testes de integração no ASP.NET Core:

Documentação da microsoft sobre como desenvolver testes de integração:  
https://learn.microsoft.com/pt-br/aspnet/core/test/integration-tests?view=aspnetcore-10.0&pivots=xunit

## Documentação Inicializar o BD com os dados de teste:

Documentação da microsoft com exemplo de como inicializar o DB:  
https://learn.microsoft.com/pt-br/aspnet/core/data/ef-mvc/intro?view=aspnetcore-10.0