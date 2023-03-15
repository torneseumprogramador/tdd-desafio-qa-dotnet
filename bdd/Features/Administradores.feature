# language: pt-BR

Funcionalidade: Gerenciamento de administradores

Cenário: Criar um novo administrador
    Dado que estou logado como administrador
    Quando eu crio um novo administrador com nome "John Doe", email "john.doe@example.com", senha "123456" e confirmação de senha "123456"
    Então o administrador com nome "John Doe" e email "john.doe@example.com" é adicionado com sucesso

Cenário: Listar administradores cadastrados
    Dado que estou logado como administrador
    E que existem os administradores cadastrados:
    Então devo ver a lista com pelo menos 1 administrador

Cenário: Atualizar um administrador existente
    Dado que estou logado como administrador
    E que o administrador com nome "Jane Smith" e email "jane.smith@example.com" já existe
    Quando eu atualizo o administrador com nome "Jane Smith" e email "jane.smith@example.com" para o nome "Jane Doe", email "jane.doe@example.com" e senha "654321"
    Então o administrador com nome "Danilo Santos" e email "danilo.santos@example.com" é atualizado com sucesso

Cenário: Excluir um administrador existente
    Dado que estou logado como administrador
    E que o administrador com nome "Bob Johnson" e email "bob.johnson@example.com" já existe
    Quando eu excluo o administrador com nome "Bob Johnson" e email "bob.johnson@example.com"
    Então o administrador com nome "Bob Johnson" e email "bob.johnson@example.com" é removido com sucesso