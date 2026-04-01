Persistência dos dados

Neste projeto, o banco de dados foi configurado utilizando o InMemory Database.

Isso significa que os dados dos filmes ficam armazenados apenas na memória enquanto a aplicação está rodando.

Quando o servidor é desligado (por exemplo, ao dar Ctrl + C) e iniciado novamente, todos os dados cadastrados anteriormente são perdidos.

Ou seja, a lista de filmes volta a ficar vazia e é necessário cadastrar os filmes novamente.

Isso acontece porque o banco InMemory não salva os dados em arquivo ou banco físico, sendo utilizado apenas para testes e desenvolvimento.