namespace FSharpLab.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Todos

[<ApiController>]
[<Route("/todo")>]
type TodoController(logger : ILogger<TodoController>) =
    inherit ControllerBase()

    let summaries =
        [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

    [<HttpPost>]
    member __.Post(todo: Todo) : Todo =
       // let todo = { Id="dasda"; Text="dassda"; Done=true}
        let todoToSave:Todo = todo
        //TodoMongoDB.save(todoToSave)
        printfn "todo: %s" todo.Id
        todo

