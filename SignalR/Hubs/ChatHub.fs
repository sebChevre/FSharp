namespace SignalR.Hubs

open Microsoft.AspNetCore.SignalR;
open System.Threading.Tasks;


type ChatHub () = 
    inherit Hub ()

    member x.Send (name : string) (message : string) =
        base.Clients.All.SendAsync ("ReceiveMessage",name,message)