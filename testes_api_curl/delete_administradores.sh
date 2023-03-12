echo "Digite o id do modelo"
read id
curl -H "Content-Type: application/json" -X DELETE http://localhost:5135/api/administradores/$id
