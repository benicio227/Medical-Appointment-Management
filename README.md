## Sobre o projeto

A API de Gestão de Consultas Médicas, desenvolvida utilizando o **.NET 8**, foi construída para facilitar o gerenciamento de consultas em clínicas médicas ou hospitais. O sistema oferece funcionalidades para cadastrar, visualizar, atualizar e cancelar consultas, além de gerenciar informações de usuários, médicos, pacientes e especialidades médicas. 

Este projeto foi construído com o objetivo de praticar os conceitos de injeção de dependência, separação de camadas e boas práticas no desenvolvimento de APIs. Ele simula um cenário real de uma aplicação backend moderna, utilizando o framework **ASP.NET Core**.

Dentre os pacotes NuGet utilizados, o **FluentAssertions** é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o **FluentValidation**  é usado para implementar regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter. Por fim, o **EntityFramework** atua como um ORM(Object-Relational Mapper) que simplifica as interações como banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL.

### Features

- **Domain-Driven-Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Testes de Unidade**: Testes abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.
- **Swagger/OpenAPI**: Integração com Swagger para documentação automática da API, permitindo testar os endpoints diretamente na interface web.
- **Migrações com EF Core**: Uso de migrações do Entity Framework Core para gerenciar alterações no banco de dados de forma controlada e segura.

### Regras de Negócio

- **Horários Disponíveis**: A API verifica automaticamente se o horário solicitado para a consulta está disponível, evitando conflitos de agendamento.
- **Cadastro de Pacientes e Médicos**: Usuários administrativos podem cadastrar e gerenciar informações sobre médicos e pacientes.
- **Proibição de Conflitos de Horário para o Mesmo Médico**: Não é permitido agendar consultas para o mesmo médico no mesmo dia e horário. A aplicação verifica automaticamente se já existe uma consulta cadastrada para o médico no horário solicitado antes de confirmar o agendamento.
- **Restrição de Consultas Simultâneas para o Mesmo Paciente**: Um paciente não pode ter duas consultas no mesmo dia e horário. Antes de registrar uma nova consulta, a aplicação verifica se o paciente já possui uma consulta agendada no mesmo período.
- **Consultas Completas com Informações Relacionadas**: As informações de consultas incluem detalhes completos de médicos e pacientes devido ao uso de chaves estrangeiras e propriedades de navegação no banco de dados. 

