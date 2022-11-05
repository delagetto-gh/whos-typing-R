Connection = () =>
{
    return new signalR.HubConnectionBuilder()
        .withUrl("/whostyping")
        .configureLogging(signalR.LogLevel.Information)
        .build();
};