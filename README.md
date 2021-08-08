# Projeto Locadora

## #MeuFuturoÉTech - Programa em parceria LocalizaLabs e ShareRH

![Capa Projeto_Locadora](https://github.com/predrofreitas/projeto-locadora/blob/main/thumbnail-projeto-locadora.png)

:blue_car: O programa de formação abordou trilha de formação em desenvolvimento back end com .NET / C# em aulas síncronas com o instrutor Cristiano Rodrigues, totalizando mais de 80 horas de curso e o projeto de uma videolocadora a ser construído usando ASP.NET Core 5:blue_car:

### Desafio proposto
Criação de um sistema para suportar demanda de serviços de uma videolocadora que permite solicitações de reserva remotas.

### Componentes do squad
- [Filipe Braga](https://github.com/filipembraga)
- [Gabrielle de Oliveira](https://github.com/GabrielleDorneles)
- [Jhonatas Felipe](https://github.com/jhonatasFelipe)
- [Mallu Ferreira](https://github.com/malluqf)
- [Pedro Freitas](https://github.com/predrofreitas)
- [Weiden Mendes](https://github.com/weidenm) 

### Tecnologias utilizadas
- .NET Core 5
- MVC
- WebAPI
- SQL Server
- Entity Framework
- Docker
- RabbitMQ
- JWT
- Git
- GitHub

### Descrição do projeto
Estruturamos a solução em projetos com diferentes responsabilidade. Como parte dos requisitos apresentado pelo instrutor do programa, nosso projeto deveria utilizar da WebAPI como core do nosso sistema e MVC apenas como camada de apresentação visual e consumidora da API. A persistência de dados está localizada no projeto Locadora.Dados. As entidades e suas interfaces estão em Locadora.Dominio. 

As classes de utilização comum em diferentes projetos da solução como Enums e DTOs foram concentrados em Locadora.Comuns. Foi criado um projeto para consumo da mensageria enfileirada em métodos da WebAPI, em um Worker localizado em ProcessarSolicitacaoAluguel. Essa feature inicialmente está implementada para o cadastro de clientes, de maneira didática, sendo parte do backlog avançar para consumo das solicitações de reserva.

