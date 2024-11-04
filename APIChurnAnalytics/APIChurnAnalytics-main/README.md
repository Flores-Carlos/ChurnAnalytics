### Integrantes do Grupo
- Macirander - RM551416 - 2TDSPF
- Carlos - RM97528 - 2TDSPX
- Munir - RM550893 - 2TDSPF
- Kaique - RM551165 - 2TDSPX
- Vinicius - RM98839 - 2TDSPX

### Visão Geral
Este projeto integra várias funcionalidades avançadas para analisar e gerenciar a perda de clientes, utilizando APIs RESTful, testes com xUnit, princípios de Clean Code e ML.NET para análise de sentimento.

### Funcionalidades

- **Integração com API Externa**: Adicionado um `AuthController` que integra com um serviço de autenticação externo para validar usuários via autenticação baseada em token.
  - **Endpoint**: `POST /api/auth/authenticate`
  - **Descrição**: Autentica um usuário validando o token fornecido.

- **Análise de Sentimento com ML.NET**: Adicionado um `SentimentController` para análise de sentimento de um texto fornecido usando ML.NET.
  - **Endpoint**: `POST /api/sentiment/analyze`
  - **Descrição**: Analisa o sentimento (positivo ou negativo) do texto fornecido.

- **Testes com xUnit**: Foram implementados testes unitários e de integração abrangentes usando xUnit para serviços principais, como `ExternalAuthService` e `SentimentAnalysisService`. Isso garante o funcionamento correto da aplicação em diferentes cenários.

- **Clean Code e Princípios SOLID**: Refatoração seguindo os princípios de Clean Code e SOLID:
  - **Responsabilidade Única**: Cada serviço e classe trata apenas de uma parte específica da funcionalidade da aplicação.
  - **Injeção de Dependência**: Serviços são injetados em controladores e classes, seguindo o princípio da inversão de dependência.
  - **Princípios de Segregação de Interface e Aberto-Fechado**: O código é projetado para ser extensível sem necessidade de modificação e com interfaces focadas.

### Arquitetura

- **Camada de Dados**  
  Utiliza o **Entity Framework Core** para interagir com o banco de dados. O modelo de dados é definido na classe `CadastroEmpresa` e o contexto do banco é gerenciado pela classe `DataContext`.

- **Camada de Serviços**  
  A lógica de negócios é atualmente manipulada diretamente nos controladores. Futuramente, pode ser implementada em serviços separados se necessário.

- **Camada de Apresentação**  
  A API expõe endpoints RESTful usando **ASP.NET Core**. Cada endpoint é responsável por uma operação específica (CRUD) e retorna dados no formato JSON.

### Design Patterns Utilizados

- **Repository Pattern**: Encapsula a lógica de acesso a dados e fornece uma interface para a camada de negócios interagir com o banco de dados. Nota: atualmente, o padrão não está explicitamente implementado, mas pode ser adicionado conforme necessário.
- **Unit of Work Pattern**: Garante que todas as alterações de dados sejam feitas em uma transação. O Entity Framework Core gerencia isso por padrão.
- **Controller**: Os controladores expõem as APIs e são responsáveis por gerenciar as solicitações HTTP e retornar respostas apropriadas.

### Arquitetura da API

- **Abordagem Monolítica**: O projeto está implementado como uma aplicação monolítica. Toda a lógica da aplicação é implementada em um único projeto, e todas as funcionalidades estão agrupadas dentro de uma única base de código.

### Justificativas para a Abordagem Monolítica

1. **Simplicidade na Arquitetura**
   - **Menor Complexidade Inicial**: Uma arquitetura monolítica é mais simples de implementar e gerenciar em estágios iniciais ou com escopo bem definido.
   - **Facilidade de Desenvolvimento e Testes**: Com uma única base de código, o desenvolvimento e os testes são mais diretos, sem necessidade de gerenciar múltiplos serviços.

2. **Custo e Recursos**
   - **Menor Overhead de Infraestrutura**: Menos recursos são necessários em comparação com uma abordagem de microservices, reduzindo custos operacionais.
   - **Facilidade de Implantação**: Implantar uma aplicação monolítica é mais simples, envolvendo apenas uma única unidade.

3. **Coerência e Coesão**
   - **Integração e Consistência**: A lógica e os dados estão encapsulados em uma única aplicação, o que pode garantir a consistência dos dados e a integridade das operações.

4. **Escalabilidade Inicial**
   - **Escalabilidade Vertical**: A aplicação pode escalar verticalmente no estágio inicial, o que pode ser suficiente para lidar com a carga esperada.

5. **Facilidade de Manutenção**
   - **Menos Dependências Externas**: Menos dependências entre serviços simplificam a manutenção e a solução de problemas.
   - **Desenvolvimento e Atualizações**: Atualizações e novos recursos podem ser implementados em um único projeto.

### Futuro Planejado
Integração de Microserviços: Em breve, planejamos integrar microserviços em uma nova camada dedicada a feedback e informações adicionais. Essa abordagem trará vários benefícios, como:

- **Escalabilidade Granular**: Permite escalar componentes específicos da API de forma independente, otimizando o uso de recursos.
- **Manutenção Simplificada**: Facilita a atualização e manutenção de funcionalidades específicas sem impactar o sistema como um todo.
- **Desempenho Melhorado**: Distribui a carga de trabalho, melhorando a eficiência e a resposta da API.
- **Organização Aprimorada**: Melhora a modularidade e a clareza do código, facilitando o desenvolvimento e a integração de novas funcionalidades.

### Instruções para Rodar a API

#### Pré-requisitos:
- **.NET Core SDK**: Certifique-se de ter o .NET Core SDK instalado. Baixe e instale o [.NET SDK](https://dotnet.microsoft.com/download).
- **Banco de Dados**: A API usa um banco de dados configurado no contexto `DataContext`. Certifique-se de que o banco de dados está acessível.

#### Configuração
1. **Clone o Repositório**
    ```bash
    git clone https://github.com/heKaiqueToschi/APIChurnAnalytics.git
    cd APIChurnAnalytics
    ```

2. **Restaurar Pacotes**
    ```bash
    dotnet restore
    ```

3. **Aplicar Migrações**
   Se você estiver usando Entity Framework Core e tiver migrações, aplique-as ao banco de dados:
    ```bash
    dotnet ef database update
    ```

4. **Executar a API**
    ```bash
    dotnet run
    ```
   A API estará disponível em `http://localhost:5004` para HTTP e `https://localhost:7280` para HTTPS, ou conforme configurado no arquivo `launchSettings.json`.

### Testando os Endpoints
- **Obter todas as empresas**: `GET http://localhost:5004/api/CadastroEmpresa`
- **Obter uma empresa por ID**: `GET http://localhost:5004/api/CadastroEmpresa/{id}`
- **Criar uma nova empresa**: `POST http://localhost:5004/api/CadastroEmpresa`
- **Atualizar uma empresa**: `PUT http://localhost:5004/api/CadastroEmpresa/{id}`
- **Deletar uma empresa**: `DELETE http://localhost:5004/api/CadastroEmpresa/{id}`
