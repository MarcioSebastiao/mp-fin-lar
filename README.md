# Sistema de Controle Financeiro

Sistema web para controle financeiro pessoal, permitindo o gerenciamento de Pessoas, Categorias e Transações, com cálculo automático de totais e saldo por pessoa.

---

## Descrição

A aplicação permite:

- Cadastro de Pessoas
- Cadastro de Categorias
- Registro de Transações (Receitas e Despesas)
- Consulta de totais por pessoa
- Cálculo de saldo

O sistema aplica regras de negócio no backend e utiliza um frontend moderno com React + TypeScript.

---

## Arquitetura

O projeto é dividido em:

### Backend

- ASP.NET Core (.NET 10)
- Entity Framework Core
- PostgreSQL
- Validações de domínio (Notification Pattern)
- Relacionamentos com integridade referencial
- Regras de negócio aplicadas na camada de dominio

**Estrutura do Projeto:**

O projeto foi estruturado com base nos princípios da **Clean Architecture**, priorizando separação de responsabilidades e isolamento das regras de negócio.

**Camadas:**

- **Domínio** → Contém regras de negócio, entidades e validações.
- **Aplicação** → Orquestra fluxos e casos de uso.
- **Infraestrutura** → Persistência (EF Core + PostgreSQL).

    (separado em pastas no projeto da API)

- **API** → Exposição dos endpoints HTTP.

---

**Notification Pattern:**

O projeto utiliza o **Notification Pattern** para validações de domínio.

**Conceito**

No Notification Pattern, a entidade pode ser instanciada em estado inválido — isso é intencional.

Ao invés de lançar exceções imediatamente, o domínio:

- Registra violações de regra
- Armazena notificações de erro
- Permite que a aplicação decida o fluxo

Isso garante que:

- Todas as regras de negócio sejam avaliadas
- Todos os erros sejam retornados de uma vez
- Garante que nenhum estado inválido seja persistido

A aplicação verifica a propriedade `EhValido` antes de realizar qualquer persistência.

**Princípios Aplicados**

- Separação de responsabilidades
- Domínio rico (Rich Domain Model)
- Validações centralizadas na entidade
- Persistência desacoplada das regras
- Uso de EF Core com mapeamento explícito

---

**Estratégia de Cache**

Foi implementado cache em memória utilizando IMemoryCache para reduzir consultas repetidas ao banco em operações de leitura, como listagem de pessoas e categorias.

O cache é invalidado automaticamente em operações de criação, edição ou exclusão, garantindo consistência dos dados.

---

### Frontend

- React
- TypeScript
- react-router-dom
- react-hook-form (gerenciamento e validação de formulários)

**Conceitos Aplicados**

- Componentização reutilizável
- Separação entre serviços e utilitários
- Design System mínimo próprio

---

## Regras de Negócio

### Pessoa

- Identificador único (GUID)
- Nome (máx. 200 caracteres)
- Idade
- Ao excluir uma pessoa → todas as transações são removidas (Cascade Delete)

---

### Categoria

- Identificador único
- Descrição (máx. 400 caracteres)
- Finalidade:
    - Despesa
    - Receita
    - Ambas

---

### Transação

- Identificador único
- Descrição (máx. 400 caracteres)
- Valor positivo
- Tipo:
    - Receita
    - Despesa
- Categoria compatível com o tipo
- Pessoa vinculada

#### Regra especial

Se a pessoa for menor de 18 anos:

- Apenas transações do tipo **Despesa** são permitidas.

---

## Relacionamentos

- Pessoa → possui várias Transações (1:N)
- Categoria → pode ser utilizada em várias Transações (1:N)
- Exclusão de Pessoa → exclusão em cascata das Transações

---

## Funcionalidades

### Pessoas

- Criar
- Editar
- Excluir
- Listar
- Acessar transações da pessoa

### Categorias

- Criar
- Listar

### Transações

- Criar
- Listar por pessoa
- Listar por categoria

### Consulta de Totais

- Total de receitas por pessoa
- Total de despesas por pessoa
- Saldo individual (Receita - Despesa)

---

## Como Rodar o Projeto Localmente

Clone o repositório:

```bash
git clone https://github.com/MarcioSebastiao/mp-fin-lar.git
```

### Pré-requisitos

Certifique-se de ter instalado:

- Node.js (versão 18 ou superior)
- npm
- .NET SDK (versão compatível com o projeto - atualmente 10.0)
- PostgreSQL

**Verifique as versões:**

```bash
node -v
npm -v
dotnet --version
psql --version

```

### Rodando o Backend (.NET + PostgreSQL):

**Certifique-se de que:**

O PostgreSQL esteja rodando

A string de conexão esteja configurada corretamente no appsettings.json

Exemplo:

`"ConnectionStrings": { "DefaultConnection": Host=localhost;Port=5432;Database=MpFinLarDB;Username=postgres;Password=sua_senha" }`

**Criando o Banco de Dados:**

Navegue para a pasta da api e rode o comando:

```bash

dotnet ef database update

```

**Rodando a aplicação:**

Execute o seguinte comando na pasta da api:

```bash

dotnet run
```

A API estará disponível em:

`https://localhost:7196 e http://localhost:5149`

**Rodando o Frontend (React + Vite):**

```bash
1 - Acesse a pasta do frontend:

cd MpFinLar.Web


2 - Instale as dependências:

npm install


3 - Execute o projeto:

npm run dev


4 - Abra no navegador:

http://localhost:3333

```

# Como Rodar o Projeto com Docker

## Pré-requisitos

- Docker
- Docker Compose

Verifique se estão instalados:

```bash
docker --version
docker compose version
```

## Subindo o Ambiente

**Na raiz do projeto, execute:**

```bash
cd ./docker
docker compose up -d --build
```

## Após subir os containers:

- API → http://localhost:5149/ (Swagger ui em: http://localhost:5149/swagger/index.html)

- Frontend → http://localhost:3055/ 

## Pronto! Só acessar:

http://localhost:3055/