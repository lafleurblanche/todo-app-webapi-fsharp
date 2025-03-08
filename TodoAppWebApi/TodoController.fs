namespace TodoAppWebApi

open TodoAppWebApi
open Microsoft.AspNetCore.Mvc
open System

[<ApiController>]
[<Route("[controller]")>]
type TodoController(repository: TodoRepository) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetAll() =
        this.Ok(repository.GetAll())

    // [<HttpGet("{id}")>]
    // member this.GetById(id: int) =
    //     match repository.GetById(id) with
    //     | Some todo -> this.Ok(todo)
    //     | None -> this.NotFound()


    [<HttpPost>]
    member this.Add(todo: Todo) =
        let newTodo = repository.Add(todo)
        this.CreatedAtAction("GetById", [| new System.Collections.Generic.KeyValuePair<string, obj>("id", newTodo.Id) |], newTodo)

    // [<HttpPut("{id}")>]
    // member this.Update(id: int, todo: Todo) =
    //     match repository.Update(id, todo) with
    //     | Some updatedTodo -> this.Ok(updatedTodo)
    //     | None -> this.NoContent()

    [<HttpDelete("{id}")>]
    member this.Delete(id: int) =
        repository.Delete(id)
        this.NoContent()
