
namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* Esse é o setup inicial. 
             * Ele cria um Builder para configurar tudo que o app precisa:
             *  -> serviços, middlewares, etc.
             * Ele já vem com pré-configuração baseada em appsettings.json, variáveis de ambiente, etc.
             */
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            /* Registrando Serviços (DI - Dependency Injection */
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
