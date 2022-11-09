import * as _ from "./lib/signalr.js";

export default function Hub(url) {

    const hub = new signalR.HubConnectionBuilder()
        .withUrl(url)
        // .configureLogging(signalR.LogLevel.Information)
        .build();

    return hub;
}