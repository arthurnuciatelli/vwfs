VWFS Microservices Project
Descrição

Este projeto implementa microserviços para gestão de clientes e propostas, utilizando .NET 9, MongoDB, PostgreSQL e Kafka. Possui monitoramento via Prometheus/Grafana e testes unitários com xUnit.

O fluxo principal é:

Customer Service: cria clientes e publica eventos customer-created no Kafka.

Proposal Service: consome eventos do Kafka e gera propostas para os clientes.

Além disso, o projeto depende de códigos compartilhados que ficam na pasta local:
/Users/arthurnuciatelli/code/arthur/Volkswagen/shared/VWFS.BuildingBlocks
Esses códigos comuns foram publicados como pacote NuGet e utilizados pelos microserviços.

Tecnologias utilizadas

.NET 9

C#

MongoDB (Customer Service)

PostgreSQL (Proposal Service)

Kafka + Zookeeper (mensageria)

Prometheus / Grafana (monitoramento)

xUnit / Moq (testes unitários)

Swagger (documentação de API)

Serilog (logging)

VWFS.BuildingBlocks (biblioteca compartilhada via NuGet)

VWFS/
├─ VWFS.Customers/          # Serviço de clientes
├─ VWFS.Proposals/          # Serviço de propostas
├─ VWFS.Customers.Tests/    # Testes unitários do serviço de clientes
├─ docker-compose.yml       # Orquestração de containers
├─ prometheus/              # Configuração do Prometheus
├─ grafana/                 # Configuração do Grafana
├─ shared/VWFS.BuildingBlocks # Códigos compartilhados (publicados no NuGet)
└─ README.md

Pré-requisitos

Docker e Docker Compose

.NET 9 SDK

Git

Acesso ao NuGet com o pacote VWFS.BuildingBlocks

Rodando o projeto
Suba os serviços via Docker Compose:
docker-compose up --build

Acesse:

Customer Service: http://localhost:8080/swagger

Grafana: http://localhost:3000 (usuário: admin / senha: admin)

Prometheus: http://localhost:9090

Testes

Para executar os testes unitários:
cd VWFS.Customers.Tests
dotnet test

Monitoramento

Métricas do .NET estão disponíveis em /metrics:

Ex: http://localhost:8080/metrics para o Customer Service

Prometheus coleta métricas de cada serviço configurado no prometheus.yml.

Grafana exibe dashboards baseados nas métricas coletadas.

k6 Para teste de carga (k6/load.js)

Observações

Validações de CPF e CNPJ são feitas na entidade Customer.

Kafka deve estar ativo para que o Proposal Service consuma eventos.

É recomendado subir os serviços na ordem: Zookeeper → Kafka → MongoDB/PostgreSQL → Customer/Proposal.

A pasta VWFS.BuildingBlocks contém funcionalidades compartilhadas entre os projetos e é publicada como pacote NuGet.
