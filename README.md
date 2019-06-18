# API Debito Credito

Para executar a API é necessário os seguintes passos:

1 - Executar o arquivo debitocredito.sql em um banco de dados SQL Server;

2 - Ajustar a string de conexão do banco de dados na class VariaveisGlobais.cs para o ambiente que será executado (todo o desenvolvimento deste projeto foi feito utilizando AWS Secrets Manager e não há registros de conexões neste repositório);

De

	_conn = VariaveisSecretas.ObterValor("CONN-DEBITOCREDITO");
    
Para

	_conn = "SUA_CONNECTION_STRING";


### Docker
Para executar a aplicação no Docker basta executar os seguintes comando no mesmo diretório do Dockerfile:

	docker build -t api-debitocredito .
	docker run --rm api-debitocredito
