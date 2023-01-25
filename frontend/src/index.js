import '@riotjs/hot-reload'
import { mount, register, install } from 'riot'
import Hub from './hub'
import GameBoard from './components/game-board/game-board.riot'
import OxField from './components/ox-field/ox-field.riot'
import NewGameButton from './components/new-game-button/new-game-button.riot'
import DisplayText from './components/display-text/display-text.riot'
import VictoryCounter from './components/victory-counter/victory-counter.riot'

var appState = {
    oxFields: {}
};

// SignalR
var hub = new Hub("http://localhost:5000/xohub");

// ----------------------------------
// SignalR calls from backend go here

hub.connection.on("CurrentFieldValue", (fieldId, value) => {
    appState.oxFields[fieldId].update({value})
})

hub.connection.on("CurrentTextFieldValue", (value) => {
    appState.displayText.update({value})
})

hub.connection.on("CurrentVictoryCount", (oVictoriesCount, xVictoriesCount) => {
    appState.victoryCounter.update({
        oVictoriesCount,
        xVictoriesCount
    })
})

// SignalR calls from backend go here
// ----------------------------------

// RiotJs
install(function(component) {
    component.appState = appState;
    component.hub = hub.connection;
})

// -----------------------------------------------
// RiotJs component registration happens here here

register('ox-field', OxField)
register('game-board', GameBoard)
register('new-game-button', NewGameButton)
register('display-text', DisplayText)
register('victory-counter', VictoryCounter)

// RiotJs component registration happens here here
// -----------------------------------------------

async function StartApp() {
    await hub.start()
    mount('game-board')
}

StartApp()
