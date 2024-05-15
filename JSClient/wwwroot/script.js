let developers = [];
let launchers = [];
let games = [];

let developersByLauncher = [];
let gamesByDeveloper = [];
let topGamesByDeveloperOnPlatform = [];
let gamesByRatingRange = [];
let launchersForDeveloper = [];

getDevelopers();
getLaunchers();
getGames();

// API
//#region API
async function getDevelopers() {
    await fetch('http://localhost:37523/Developer')
        .then(x => x.json())
        .then(y => {
            developers = y;
            console.log("developers GET successful");
            console.log(developers); // for testing
        });
}

async function getLaunchers() {
    fetch('http://localhost:37523/Launcher')
        .then(x => x.json())
        .then(y => {
            launchers = y;
            console.log("launchers GET successful");
        });
}

async function getGames() {
    fetch('http://localhost:37523/Game')
        .then(x => x.json())
        .then(y => {
            games = y;
            console.log("games GET successful");
        });
}

async function getDevelopersByLauncher(launcherName) {
    fetch('http://localhost:37523/DeveloperNonCrud/DevelopersByLauncher/' + launcherName)
        .then(x => x.json())
        .then(y => {
            developersByLauncher = y;
            console.log("DevelopersByLauncher GET successful");
            console.log(y);
        });
}

async function getGamesByDeveloper(developerName) {
    fetch('http://localhost:37523/GameNonCrud/GamesByDeveloper/' + developerName)
        .then(x => x.json())
        .then(y => {
            gamesByDeveloper = y;
            console.log("GamesByDeveloper GET successful");
            console.log(y);
        });
}

async function getTopGamesByDeveloperOnPlatform(developerName, launcherName) {
    fetch('http://localhost:37523/GameNonCrud/TopGamesByDeveloperOnPlatform/' + developerName + '/' + launcherName)
        .then(x => x.json())
        .then(y => {
            topGamesByDeveloperOnPlatform = y;
            console.log("TopGamesByDeveloperOnPlatform GET successful");
            console.log(y);
        });
}

async function getGamesByRatingRange(minRating, maxRating, developerName) {
    fetch('http://localhost:37523/GameNonCrud/GamesByRatingRange/' + minRating + '/' + maxRating + '/' + developerName)
        .then(x => x.json())
        .then(y => {
            gamesByRatingRange = y;
            console.log("GamesByRatingRange GET successful");
            console.log(y);
        });
}

async function getLaunchersForDeveloper(developerName) {
    fetch('http://localhost:37523/GameNonCrud/LaunchersForDeveloper/' + developerName)
        .then(x => x.json())
        .then(y => {
            launchersForDeveloper = y;
            console.log("LaunchersForDeveloper GET successful");
            console.log(y);
        });
}
// 43342
// http://localhost:43342

//#endregion

// SignalR
//#region SignalR
let connection = null;


setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:37523/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("DeveloperCreated", (user, message) => {
        return getDevelopers()
            .then(() => displayDevelopers());
    });
    connection.on("DeveloperDeleted", (user, message) => {
        return getDevelopers()
            .then(() => displayDevelopers());
    });
    connection.on("DeveloperUpdated", (user, message) => {
        return getDevelopers()
            .then(() => displayDevelopers());
    });

    connection.on("LauncherCreated", (user, message) => {
        return getLaunchers()
            .then(() => displayLaunchers());
    });
    connection.on("LauncherDeleted", (user, message) => {
        return getLaunchers()
            .then(() => displayLaunchers());
    });
    connection.on("LauncherUpdated", (user, message) => {
        return getLaunchers()
            .then(() => displayLaunchers());
    });

    connection.on("GameCreated", (user, message) => {
        return getGames()
            .then(() => displayGames());
    });
    connection.on("GameDeleted", (user, message) => {
        return getGames()
            .then(() => displayGames());
    });
    connection.on("GameUpdated", (user, message) => {
        return getGames()
            .then(() => displayGames());
    });

    connection.onclose
        (async () => {
            await start();
        });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected!");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
//#endregion

// Developers
//#region Developers
let developerUpdateId = 0;

function displayDevelopers() {
    document.getElementById('developerwindow').style.display = 'flex';
    //document.getElementById('launcherwindow').style.display = 'none';
    //document.getElementById('gamewindow').style.display = 'none';

    document.getElementById('updateDeveloper').style.display = 'none';
    document.getElementById('developersByLauncherWindow').style.display = 'none';
    //document.getElementById('updateLauncher').style.display = 'none';
    //document.getElementById('updateGame').style.display = 'none';

    getDevelopers();
    listDevelopers();
}

function listDevelopers() {
    document.getElementById('developers').innerHTML = "";
    developers.forEach(t => {
        document.getElementById('developers').innerHTML +=
            `<tr>
                    <td><input type="radio" name="selectDeveloperRadio" onclick='showUpdateDeveloper("${t.developerID}", "${t.developerName}", "${t.foundingYear}")'></input></td>
                    <td>${t.developerID}</td>
                    <td>${t.developerName}</td>
                    <td>${t.foundingYear}</td>
                    <td><button type="button" onclick='removeDeveloper(${t.developerID})'>Delete</button></td>
                </tr>`;
    });
}

function addDeveloper() {
    let developerID = document.getElementById('developerid').value;
    let developerName = document.getElementById('developername').value;
    let foundingYear = document.getElementById('foundingyear').value;

    fetch('http://localhost:37523/developer', {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            developerID: developerID,
            developerName: developerName,
            foundingYear: foundingYear
        })
    })
    .then(x => {
        console.log(x);
        displayDevelopers();
    })
    .catch((error) => {
        console.error("Error:", error);
    });
}

async function removeDeveloper(id){
    await fetch('http://localhost:37523/developer/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    }).then(data => {
        console.log(data);
        console.log("developer DELETE successful");
        return getDevelopers();
    })
        .then(() =>
            displayDevelopers())
    .catch((error) => {
        console.error('Error:', error);
    });
}

function updateDeveloper() {
    let developerID = document.getElementById('developeridUpdate').value;
    let developerName = document.getElementById('developernameUpdate').value;
    let foundingYear = document.getElementById('foundingyearUpdate').value;

    fetch('http://localhost:37523/developer', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            developerID: developerID,
            developerName: developerName,
            foundingYear: foundingYear
        })
    })
        .then(data => {
            console.log(data);
            return getDevelopers();
        })
        .then(() => {
            displayDevelopers();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    document.getElementById('updateDeveloper').style.display = 'none';
}

function showUpdateDeveloper(developerID, developerName, foundingYear) {
    document.getElementById('updateDeveloper').style.display = 'flex';
    document.getElementById('developeridUpdate').value = developerID;
    document.getElementById('developernameUpdate').value = developerName;
    document.getElementById('foundingyearUpdate').value = foundingYear;
    developerUpdateId = developerID;
}
//#endregion










