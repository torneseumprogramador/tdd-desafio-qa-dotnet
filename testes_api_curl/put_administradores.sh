echo "Digite o id"
read id

echo "Digite o nome"
read nome

echo "Digite o email"
read email

echo "Digite o senha"
read senha

curl -v -d "{\"id\":$id, \"nome\":\"$nome\", \"email\":\"$email\", \"senha\":\"$senha\"}" -H "Content-Type: application/json" -X PUT http://localhost:5135/api/administradores/$id
