
# Desafio Umbler

Esta é uma aplicação web que recebe um domínio e mostra suas informações de DNS.

Este é um exemplo real de sistema que utilizamos na Umbler.

Ex: Consultar os dados de registro do dominio `umbler.com`

**Retorno:**
- Name servers (ns254.umbler.com)
- IP do registro A (177.55.66.99)
- Empresa que está hospedado (Umbler)

Essas informações são descobertas através de consultas nos servidores DNS e de WHOIS.

*Obs: WHOIS (pronuncia-se "ruís") é um protocolo específico para consultar informações de contato e DNS de domínios na internet.*

Nesta aplicação, os dados obtidos são salvos em um banco de dados, evitando uma segunda consulta desnecessaria, caso seu TTL ainda não tenha expirado.

*Obs: O TTL é um valor em um registro DNS que determina o número de segundos antes que alterações subsequentes no registro sejam efetuadas. Ou seja, usamos este valor para determinar quando uma informação está velha e deve ser renovada.*

Tecnologias Backend utilizadas:

- C#
- Asp.Net Core
- MySQL
- Entity Framework

Tecnologias Frontend utilizadas:

- Webpack
- Babel
- ES7

Para rodar o projeto você vai precisar instalar:

- .NET Core SDK (https://www.microsoft.com/net/download/windows .Net Core 2.0.3 SDK)
- Um editor de código, acoselhado Visual Studio ou VisualStudio Code. (https://code.visualstudio.com/)
- NodeJs para "buildar" o FrontEnd (https://nodejs.org/en/)
- Um banco de dados MySQL (crie um gratuitamente no app da Umbler https://app.umbler.com/)

Com as ferramentas devidamente instaladas, basta executar os seguintes comandos:

Para "buildar" o javascript

`npm install`
`npm run build`

Para Rodar o projeto:

`dotnet run` (ou clique em "play" no editor)

# Objetivos:

Se você rodar o projeto e testar um domínio, verá que ele já está funcionando. Porém, queremos melhorar varios pontos deste projeto:

# FrontEnd

 - Os dados retornados não estão formatados, e devem ser apresentados de uma forma legível. OK
 - Não há validação no frontend permitindo que seja submetido uma requsição inválida para o servidor. OK

# BackEnd

 - O projeto está rodando em .Net Core 2.0.3, porém é necessário fazer o upgrade para o .Net Core 6.0.100 OK
 - Não há validação no backend permitindo que um requisição inválida prossiga, o que ocasiona exceptions (erro 500). OK
 - A complexidade ciclomática do controller está muito alta, o ideal seria utilizar uma arquitetura em camadas. OK
 - O DomainController está retornando a própria entidade de domínio por JSON, o que faz com que propriedades como Id, Ttl e UpdatedAt sejam mandadas para o cliente web desnecessariamente. Retornar uma ViewModel neste caso seria mais aconselhado. OK

# Testes

 - A cobertura de testes unitários está muito baixa, e o DomainController está impossível de ser testado pois não há como "mockar" a infraestrutura.
 - O Banco de dados já está sendo "mockado" graças ao InMemoryDataBase do EntityFramework, mas as consultas ao Whois e Dns não. 

# Dica

- Este teste não tem "pegadinha", é algo pensado para ser simples. Aconselhamos a ler o código, e inclusive algumas dicas textuais deixas das nos testes unitários. 
- Há um teste unitário que está comentado, que obrigatoriamente tem que passar.
- Diferencial: criar mais testes.

# Entrega

- Enviei o link do seu repositório com o código atualizado.
- O repositório deve estar público.
- Modifique Este readme adicionando informações sobre os motivos das mudanças realizadas.

# Modificações:

- DESCREVA AQUI O OBJETIVO DAS MODIFICAÇÕES...
