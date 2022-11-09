import Hub from "./hub.js";

const playerNameInput = document.getElementById("input-player-name");
const joinGameButton = document.getElementById("btn-join-game");
const typingInput = document.getElementById("input-player-typing");
const someoneTypingAlert = document.getElementById("p-someone-typing-alert");

const youreGuessingSection = document.getElementById("youre-guessing-section");
const youreTypingection = document.getElementById("youre-typing-section");
const waitingPlaceholder = document.getElementById("waiting-placeholder");

const gameHub = new Hub("/whostyping");

let pId;
let pName;
let players;

playerNameInput.disabled = true;
joinGameButton.disabled = true;

gameHub.on("Event", event => {

    if (event.name === "Connected") {
        pId = event.pId;
        console.log(`Connected. PID = ${pId}`);
        playerNameInput.disabled = false;
        joinGameButton.disabled = false;
    }

    if (event.name === "GameStarted") {
        waitingPlaceholder.style.display = "none";
        youreTypingection.style.direction = "none";
        youreGuessingSection.style.display = "block";
        players = event.players;
        players
            .filter(o => o.id !== pId)
            .forEach(o => {
                let name = o.name;
                var btn = document.createElement("button");
                btn.innerText = name;
                btn.dataset.pid = o.id;
                youreGuessingSection.appendChild(btn);
            });
    }

    if (event.name === "Chosen") {
        waitingPlaceholder.style.display = "none";
        youreGuessingSection.style.display = "none";
        youreTypingection.style.display = "block";
    }

    if (event.name === "SomeoneTyping") {
        // display iitalic someone typing...
        someoneTypingAlert.innerText = "Someone's typing..."
    }

    if (event.name === "Countdown") {
        waitingPlaceholder.style.display = "none";
        youreGuessingSection.style.display = "none";
        youreTypingection.style.display = "block";
    }
});

joinGameButton.addEventListener("click", () => {
    pName = playerNameInput.value;
    playerNameInput.disabled = true;
    gameHub.send("Join", pId, pName);
});

typingInput.addEventListener("keydown", () => {
    gameHub.send("Type", pId);
});

try {
    await gameHub.start();
} catch (error) {
    console.error("Error starting app.");
    console.error({ error });
}




