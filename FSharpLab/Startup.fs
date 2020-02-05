namespace FSharpLab

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Swashbuckle.AspNetCore.Swagger
open Microsoft.OpenApi.Models
open MongoDB.Driver
open Todos.TodoMongoDB
open Todos
open Giraffe
open Todos.Http



type Startup private () =


    
    new(configuration : IConfiguration) as this =
        Startup()
        then this.Configuration <- configuration
      

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services : IServiceCollection) =
        let info = OpenApiInfo()
        info.Title <- "My API V1"
        info.Version <- "v1"
        // Add framework services.
        services.AddSwaggerGen(fun c -> c.SwaggerDoc("v1",info)) |> ignore
        let mongo = MongoClient ("mongodb://localhost:27017")
        //let mongo = MongoClient (Environment.GetEnvironmentVariable "MONGO_URL")
        let db = mongo.GetDatabase "todos"


        services.AddTodoMongoDB(db.GetCollection<Todo>("todos")) |> ignore
        services.AddGiraffe() |> ignore
        services.AddControllers() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app : IApplicationBuilder, env : IWebHostEnvironment) =
        if (env.IsDevelopment()) then app.UseDeveloperExceptionPage() |> ignore

        
        //app.UseMvc() |> ignore
        app.UseSwagger() |> ignore
        app.UseSwaggerUI(fun config -> config.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")) |> ignore
        app.UseGiraffe(choose [ TodoHttp.handlers ]) |> ignore
        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore

        app.UseAuthorization() |> ignore

        app.UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore) |> ignore

    member val Configuration : IConfiguration = null with get, set



