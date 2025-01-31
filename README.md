# Hardwork Medicina - API Sistema Gerenciador de Usuarios
### Projeto desenvolvido em APS.NET Core integrado a banco de dados MySQL em Docker

### Pré-requisitos
- Docker
- .NET SDK 8.0 ou superior
- MySQL Workbench ou qualquer outra ferramenta gerenciadora de banco de dados MySQL (Opcional)

### Configuração
1. Após clonar este repositório confira se os arquivos docker-compose.yml e Dockerfile estão configurados corretamente para rodar a aplicação.

2. Verifique as configurações de conexão com o MySQL no arquivo `appsettings.json`

### Execução
1. Abra um terminal na raiz do projeto e execute o seguinte comando para construir e iniciar os conteineres:

`docker-compose up -d --build`

2. Aguarde até que todos os serviços estejam em execução, apos isso a aplicação estara disponivel em `http://localhost:8080` para consumo das APIs
 e parar acessar a documentação da API no Swagger acesse `http://locahost:8080/swagger`

3. Migrações do Banco de Dados
Para executar as migrações, siga estes passos:
- No terminal execute o comando abaixo para instalar a ferramenta Entity Framework Core para realizar as migrações:
  
`dotnet tool install --global dotnet-ef`

Execute o comando para rodar as migrações:

`dotnet ef database update`