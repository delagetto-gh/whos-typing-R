const playerNameInput = document.getElementById("input-player-name");
const joinGameButton = document.getElementById("btn-join-game");

const youreGuessingSection = document.getElementById("youre-guessing-section");
const youreTypingection = document.getElementById("youre-typing-section");
const waitingPlaceholder = document.getElementById("waiting-placeholder");

const gameHub = createHub("/whostyping");

let pId;

const startApp = async () => {

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
        }

        if (event.name === "Chosen") {
            waitingPlaceholder.style.display = "none";
            youreGuessingSection.style.display = "none";
            youreTypingection.style.display = "block";
        }
    });

    joinGameButton.addEventListener("click", () => {
        var name = playerNameInput.value;
        playerNameInput.disabled = true;
        gameHub.send("Join", pId, name);
    });

    // playerTypingInput.addEventListener("keydown", () => {
    //     gameHub.send("Type", pId);
    // });

    try {
        await gameHub.start();
    } catch (error) {
        console.error("Error starting app.");
        console.error({ error });
    }
}

startApp();

