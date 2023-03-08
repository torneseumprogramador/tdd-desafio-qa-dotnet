using tdd_desafio_qa_dotnet;

IHostBuilder CreateHostBuilder(string[] args){
  return Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    });
}

CreateHostBuilder(args).Build().Run();