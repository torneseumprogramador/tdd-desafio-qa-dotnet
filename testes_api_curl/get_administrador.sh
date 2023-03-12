echo "Digite o id do modelo"
read id
curl -v -H "Content-Type: application/json" -X GET http://localhost:5135/api/administradores/$id
# caso você queira passar o token no header de uma api autenticada
# curl -H "Content-Type: application/json" -H "Authorization: Bearer 12122121267117612852176125" -X GET http://localhost:5135/api/administradores/$id



