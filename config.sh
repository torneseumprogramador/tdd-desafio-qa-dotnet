cd ui
export DATABASE_URL="Server=localhost;Database=desafioqa;Uid=root;Pwd=root"
dotnet ef database update
export DATABASE_URL="Server=localhost;Database=desafioqa_base_qa;Uid=root;Pwd=root"
dotnet ef database update
cd ../