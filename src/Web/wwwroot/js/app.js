let pId;

const playerNameInput = document.getElementById("input-player-name");

const joinBtn = document.getElementById("btn-join");
joinBtn.disabled = true;

async function start() {

    const connection = Connection();

    connection.on("Event", event => {
        // const li = document.createElement("li");
        // li.textContent = `${user}: ${message}`;
        // document.getElementById("messageList").appendChild(li);
        if (event.name === "Connected") {
            pId = event.pId;
            console.log(`Connected. PID: ${pId}`);
            joinBtn.disabled = false;
        }
    });

    joinBtn.addEventListener("click", () => {

        var name = playerNameInput.value;
        connection.send("Join", pId, name);
    });

    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log("SignalR NOT Connected.");
        console.log(err);
        // setTimeout(start, 5000);
    }
}

start();