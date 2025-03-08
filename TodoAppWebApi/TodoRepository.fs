namespace TodoAppWebApi

open TodoAppWebApi
open System.Collections.Generic

type TodoRepository() =
    let mutable todos = new Dictionary<int, Todo>()
    let mutable nextId = 1

    member this.GetAll() =
        todos.Values |> Seq.toList

    member this.GetById(id: int) =
        match todos.TryGetValue(id) with
        | true, todo -> Some todo
        | false, _ -> None

    member this.Add(todo: Todo) =
        let id = nextId
        let newTodo = { todo with Id = id }
        todos.Add(id, newTodo)
        nextId <- nextId + 1
        newTodo

    member this.Update(id: int, todo: Todo) =
        if todos.ContainsKey(id) then
            let updatedTodo = { todo with Id = id }
            todos.[id] <- updatedTodo
            Some updatedTodo
        else
            None

    member this.Delete(id: int) =
        todos.Remove(id) |> ignore
