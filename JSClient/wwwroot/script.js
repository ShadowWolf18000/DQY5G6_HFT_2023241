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
    await fetch('http://localhost:37523/Launcher')
        .then(x => x.json())
        .then(y => {
            launchers = y;
            console.log("launchers GET successful");
            console.log(launchers);
        });
}

async function getGames() {
    await fetch('http://localhost:37523/Game')
        .then(x => x.json())
        .then(y => {
            games = y;
            console.log("games GET successful");
        });
}

async function getDevelopersByLauncher(launcherName) {
    await fetch('http://localhost:37523/DeveloperNonCrud/DevelopersByLauncher/' + launcherName)
        .then(x => x.json())
        .then(y => {
            developersByLauncher = y;
            console.log("DevelopersByLauncher GET successful");
            console.log(y);
        });
}

async function getGamesByDeveloper(developerName) {
    await fetch('http://localhost:37523/GameNonCrud/GamesByDeveloper/' + developerName)
        .then(x => x.json())
        .then(y => {
            gamesByDeveloper = y;
            console.log("GamesByDeveloper GET successful");
            console.log(y);
        });
}

async function getTopGamesByDeveloperOnPlatform(developerName, launcherName) {
    await fetch('http://localhost:37523/GameNonCrud/TopGamesByDeveloperOnPlatform/' + developerName + '/' + launcherName)
        .then(x => x.json())
        .then(y => {
            topGamesByDeveloperOnPlatform = y;
            console.log("TopGamesByDeveloperOnPlatform GET successful");
            console.log(y);
        });
}

async function getGamesByRatingRange(minRating, maxRating, developerName) {
    await fetch('http://localhost:37523/GameNonCrud/GamesByRatingRange/' + minRating + '/' + maxRating + '/' + developerName)
        .then(x => x.json())
        .then(y => {
            gamesByRatingRange = y;
            console.log("GamesByRatingRange GET successful");
            console.log(y);
        });
}

async function getLaunchersForDeveloper(developerName) {
    await fetch('http://localhost:37523/GameNonCrud/LaunchersForDeveloper/' + developerName)
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
    document.getElementById('launcherwindow').style.display = 'none';
    document.getElementById('gamewindow').style.display = 'none';

    document.getElementById('updateDeveloper').style.display = 'none';
    document.getElementById('developersByLauncherWindow').style.display = 'flex';

    document.getElementById('updateLauncher').style.display = 'none';
    document.getElementById('updateGame').style.display = 'none';

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

async function addDeveloper() {
    let developerID = document.getElementById('developerid').value;
    let developerName = document.getElementById('developername').value;
    let foundingYear = document.getElementById('foundingyear').value;
    if (doesDeveloperExist(developerID) == false) {
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
    else {
        console.log("Error: cannot add Developer, because Developer with the same id already exists in the database.");
    }
}

async function removeDeveloper(id) {
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

function doesDeveloperExist(id) {
    let k = false;
    let i = 0;
    while (k != true && i < developers.length) {
        k = developers[i].developerID == id;
        i++;
    }
    return k;
}
//#endregion

// Launchers
//#region Launchers
let launcherUpdateId = 0;

function displayLaunchers() {
    document.getElementById('developerwindow').style.display = 'none';
    document.getElementById('launcherwindow').style.display = 'flex';
    document.getElementById('gamewindow').style.display = 'none';

    document.getElementById('updateDeveloper').style.display = 'none';
    document.getElementById('developersByLauncherWindow').style.display = 'none';

    document.getElementById('updateLauncher').style.display = 'none';
    document.getElementById('updateGame').style.display = 'none';

    getLaunchers();
    listLaunchers();
}

function listLaunchers() {
    document.getElementById('launchers').innerHTML = "";
    launchers.forEach(t => {
        document.getElementById('launchers').innerHTML +=
            `<tr>
                    <td><input type="radio" name="selectLauncherRadio" onclick='showUpdateLauncher("${t.launcherID}", "${t.launcherName}", "${t.owner}")'></input></td>
                    <td>${t.launcherID}</td> 
                    <td>${t.launcherName}</td>
                    <td>${t.owner}</td>
                    <td><button type="button" onclick='removeLauncher(${t.launcherID})'>Delete</button></td>
                </tr>`;
    });
}

async function addLauncher() {
    let launcherID = document.getElementById('launcherid').value;
    let launcherName = document.getElementById('launchername').value;
    let owner = document.getElementById('owner').value;
    if (doesLauncherExist(launcherID) == false) {
        fetch('http://localhost:37523/launcher', {
            method: 'POST',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                launcherID: launcherID,
                launcherName: launcherName,
                owner: owner
            })
        })
            .then(x => {
                console.log(x);
                displayLaunchers();
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }
    else {
        console.log("Error: cannot add Launcher, because Launcher with the same id already exists in the database.");
    }
}

async function removeLauncher(id) {
    await fetch('http://localhost:37523/launcher/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    }).then(data => {
        console.log(data);
        console.log("launcher DELETE successful");
        return getLaunchers();
    })
        .then(() =>
            displayLaunchers())
        .catch((error) => {
            console.error('Error:', error);
        });
}

function updateLauncher() {
    let launcherID = document.getElementById('launcheridUpdate').value;
    let launcherName = document.getElementById('launchernameUpdate').value;
    let owner = document.getElementById('ownerUpdate').value;

    fetch('http://localhost:37523/launcher', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            launcherID: launcherID,
            launcherName: launcherName,
            owner: owner
        })
    })
        .then(data => {
            console.log(data);
            return getLaunchers();
        })
        .then(() => {
            displayLaunchers();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    document.getElementById('updateLauncher').style.display = 'none';
}

function showUpdateLauncher(launcherID, launcherName, Owner) {
    document.getElementById('updateLauncher').style.display = 'flex';
    document.getElementById('launcheridUpdate').value = launcherID;
    document.getElementById('launchernameUpdate').value = launcherName;
    document.getElementById('ownerUpdate').value = Owner;
    launcherUpdateId = launcherID;
}

function doesLauncherExist(id) {
    let k = false;
    let i = 0;
    while (k != true && i < launchers.length) {
        k = launchers[i].launcherID == id;
        i++;
    }
    return k;
}
//#endregion

// Games
// #region Games
let gameUpdateId = 0;

function displayGames() {
    document.getElementById('developerwindow').style.display = 'none';
    document.getElementById('launcherwindow').style.display = 'none';
    document.getElementById('gamewindow').style.display = 'flex';

    document.getElementById('updateDeveloper').style.display = 'none';
    document.getElementById('developersByLauncherWindow').style.display = 'none';

    document.getElementById('updateLauncher').style.display = 'none';
    document.getElementById('updateGame').style.display = 'none';

    getGames();
    listGames();
}

function listGames() {
    document.getElementById('games').innerHTML = "";
    games.forEach(t => {
        document.getElementById('games').innerHTML +=
            `<tr>
                    <td><input type="radio" name="selectGameRadio" onclick='showUpdateGame("${t.gameID}", "${t.title}", "${t.developerID}", "${t.launcherID}", "${t.rating}")'></input></td>
                    <td>${t.gameID}</td> 
                    <td>${t.title}</td>
                    <td>${t.developerID}</td>
                    <td>${t.launcherID}</td>
                    <td>${t.rating}</td>
                    <td><button type="button" onclick='removeGame(${t.gameID})'>Delete</button></td>
            </tr>`;
    });
}

async function addGame() {
    let gameID = document.getElementById('gameid').value;
    let title = document.getElementById('gametitle').value;
    let developerID = document.getElementById('game_developerid').value;
    let launcherID = document.getElementById('game_launcherid').value;
    let rating = document.getElementById('rating').value;

    if (doesGameExist(gameID) == false) {
        fetch('http://localhost:37523/game', {
            method: 'POST',
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                gameID: gameID,
                title: title,
                developerID: developerID,
                launcherID: launcherID,
                rating: rating
            })
        })
            .then(x => {
                console.log(x);
                displayGames();
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    }
    else {
        console.log("Error: cannot add Game, because Game with the same id already exists in the database.");
    }
}

async function removeGame(id) {
    await fetch('http://localhost:37523/game/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: null
    }).then(data => {
        console.log(data);
        console.log("game DELETE successful");
        return getGames();
    })
        .then(() =>
            displayGames())
        .catch((error) => {
            console.error('Error:', error);
        });
}

function updateGame() {
    let gameID = document.getElementById('gameidUpdate').value;
    let title = document.getElementById('gametitleUpdate').value;
    let developerID = document.getElementById('game_developeridUpdate').value;
    let launcherID = document.getElementById('game_launcheridUpdate').value;
    let rating = document.getElementById('ratingUpdate').value;

    fetch('http://localhost:37523/game', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            gameID: gameID,
            title: title,
            developerID: developerID,
            launcherID: launcherID,
            rating: rating
        })
    })
        .then(data => {
            console.log(data);
            return getGames();
        })
        .then(() => {
            displayGames();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    document.getElementById('updateGame').style.display = 'none';
}

function showUpdateGame(gameID, title, developerID, launcherID, rating) {
    document.getElementById('updateGame').style.display = 'flex';
    document.getElementById('gameidUpdate').value = gameID;
    document.getElementById('gametitleUpdate').value = title;
    document.getElementById('game_developeridUpdate').value = developerID;
    document.getElementById('game_launcheridUpdate').value = launcherID;
    document.getElementById('ratingUpdate').value = rating;
    gameUpdateId = gameID;
}

function doesGameExist(id) {
    let k = false;
    let i = 0;
    while (k != true && i < games.length) {
        k = games[i].gameID == id;
        i++;
    }
    return k;
}
// #endregion







