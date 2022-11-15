# Testes unitários em .Net 6
Este é o projeto usado na live de **Testes Unitários em .Net 6**.

## Como rodar o projeto?
- Tenha o [Docker](https://www.docker.com/) instalado
- Rodar o docker-compose para subir o banco de dados. O arquivo está na raiz do repositório.
```powershel
docker-compose -f docker-sqlserver.yml up -d
```
- Rodar as migrações no banco de dados
- Rodar o projeto no Visual Studio 2022 ou via CLI

## Licença
[MIT](https://choosealicense.com/licenses/mit/)
