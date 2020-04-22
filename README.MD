C# Customers API

Criar uma API em .NET Core 3.1:
- Criar um CRUD (Customers por exemplo).
- Deve ser feito armazenamento no MongoDb.
- Deve ser feito o consumo de uma API externa (Exemplo, armazenando e consultando na Mundipagg).
- Fazer as chamadas na API da Mundipagg usando RestSharp - Não vale a SDK da Mundipagg.
- Usar bem o IoC;
- Tentar ao máximo usar recursos como Linq/Tasks.
- Usar Newtonsoft como serializador Json - Snakecase.
- Usar Serilog para log de Request/Response no SEQ.
- Healthcheck validando MongoDb.
- Documentação com Swagger.
- Aplicação rodando em Docker.