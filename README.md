# Trabalho de Programação Distribuída - Parte II

## Explicação

### Atividade 02: Desenvolvimento de um sistema para gerenciamento de carteiras de criptomoedas utilizando Web Services e interoperabilidade com RPC

As criptomoedas, como **Bitcoin** e **Ethereum**, já ganharam popularidade e valor. Com isso, surge a necessidade de desenvolver ferramentas que facilitem o gerenciamento dessas criptomoedas, como:

- Controle de compras e vendas
- Transferências
- Consultas de saldo

Objetivo
O objetivo desta atividade é desenvolver um **sistema de gerenciamento de carteiras de criptomoedas**, implementando:

1. **Operações básicas de CRUD (Create, Read, Update e Delete)**
2. **Integração com serviço consumido via chamada remota de procedimento (RPC)**
3. **Proposição de novas funcionalidades para agregar valor ao sistema**

Implementação do Web Services
As operações básicas do sistema devem ser implementadas utilizando **HTTP** e **JSON** para troca de dados. As operações incluem:

- **Criação** de uma nova carteira de criptomoedas
- **Consulta** do saldo da carteira
- **Adição** ou **remoção** de criptomoedas
- **Transferência** de criptomoedas entre carteiras
- **Exclusão** de uma carteira

Disponibilização de serviço via Chamada Remota de Procedimento
Deve-se implementar serviços que serão **consumidos internamente** para atender às funcionalidades do usuário. Esses serviços não estarão expostos diretamente.

Proposta de novas funcionalidades
Algumas sugestões de funcionalidades adicionais incluem:

- **Histórico de transações:** permitir que usuários consultem todas as transações realizadas, incluindo datas e valores.
- **Integração com corretoras ("de mentirinha" 😊):** permitir compras e vendas de criptomoedas diretamente no sistema, utilizando APIs de corretoras.

---

## Estrutura do Projeto
A arquitetura do sistema é dividida em **3 partes**:

1. **Controller** - Gerenciamento das requisições HTTP
2. **Client** - Interface para o usuário final
3. **GrpcService** - Serviço de comunicação remota
![{E39E3767-DBCF-487E-A175-7749C9CCC6DA}](https://github.com/user-attachments/assets/f7056faf-fc62-4918-9cd2-33c2bf708c1b)


### Cliente gRPC - Criptomoedas e Carteiras Digitais

Aqui fazemos a implementação de um **cliente gRPC** que se comunica com um servidor para realizar operações relacionadas a **criptomoedas** e **carteiras**.

---

### Estrutura do Código

O código segue a estrutura básica de um cliente gRPC, onde:

- Cria-se um **canal de comunicação** para se conectar ao servidor.
- Instancia-se um **cliente gRPC** baseado no serviço `Greeter`.
- Realizam-se **chamadas RPC (Remote Procedure Call)** para interagir com o servidor.

---

### Criação do Canal gRPC

```csharp
using var channel = GrpcChannel.ForAddress("https://localhost:7276");
```
Define um canal de comunicação gRPC apontando para um servidor rodando na porta `7276` via **HTTPS**.

---

### Criação do Cliente gRPC

```csharp
var client = new Greeter.GreeterClient(channel);
```
Instancia um cliente gRPC que permite chamar os métodos remotos definidos no servidor.

---

### Execução de Chamadas gRPC

A seguir, alguns métodos executam requisições enviadas ao servidor para **criar e gerenciar moedas e carteiras**.

---

### Criar Moedas (`PostMoedas`)

```csharp
var moeda1 = new PostMoedaRequest 
{ 
    Id = 1, 
    Name = "BitCoin", 
    Valor = "100" 
}; 

var moeda2 = new PostMoedaRequest 
{ 
    Id = 2, 
    Name = "CeciCon", 
    Valor = "10" 
}; 

var response1 = await client.PostMoedasAsync(moeda1); 
var response2 = await client.PostMoedasAsync(moeda2); 
```

#### O que acontece?

- São criadas duas criptomoedas: **BitCoin** e **CeciCon**.
- Cada moeda tem um **ID**, **nome** e **valor inicial** definido.
- Envia-se uma **requisição gRPC do tipo POST** para adicionar essas moedas no servidor.

---

### Criar Carteiras (`PostCarteira`)

```csharp
var carteira1 = new PostCarteiraRequest 
{ 
    NumeroConta = "001", 
    IdMoeda = 2, 
    CodResponsavel = "1234", 
    NomeResponsavel = "Bia", 
    QtdMoedas = "10", 
    StatusConta = "Positivo", 
}; 

var inseriCart = await client.PostCarteiraAsync(carteira1);
```

#### O que acontece?

- Criamos uma carteira associada à moeda `CeciCon` e ao usuário `"Bia"`.
- Cada carteira contém os seguintes dados:
  - **Número da conta** (`NumeroConta`)
  - **ID da moeda** (`IdMoeda`)
  - **Código do responsável** (`CodResponsavel`)
  - **Nome do responsável**
  - **Quantidade inicial de moedas** (`QtdMoedas`)
  - **Status da conta** (`StatusConta`), como `Positivo` ou `Neutro`
- A requisição **gRPC POST** é enviada para salvar a carteira no servidor.
