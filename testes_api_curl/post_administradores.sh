echo "Digite o nome"
read nome

echo "Digite o email"
read email

echo "Digite o senha"
read senha

curl -d "{\"nome\":\"$nome\", \"email\":\"$email\", \"senha\":\"$senha\"}" -H "Content-Type: application/json" -X POST http://localhost:5135/api/administradores
