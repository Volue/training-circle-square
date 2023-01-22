using Microsoft.AspNetCore.SignalR;

namespace Training.CircleSquareGame.Api;

public class XOHub : Hub
{
    private static Game game = new();
    public async Task GetField(string fieldId)
    {
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, game.Board[fieldId]);
    }

    public async Task SetField(string fieldId)
    {
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, game.CurrentPlayer);
        game.AddMarkToBoard(fieldId, game.CurrentPlayer);
        if (game.CheckIfWin())
        {
            game.Winner = game.CurrentPlayer;
            await EndOfGame();
        }
        else if (game.CheckIfDraw())
        {
            await EndOfGame();
        }
        else
        {
            game.SetCurrentPlayer();
        }

    }

    public async Task EndOfGame()
    {
        if (game.Winner != null)
        {
            await Clients.All.SendAsync("EndOfGame", "win", game.Winner);
        }
        else if (game.CheckIfDraw())
        {
            await Clients.All.SendAsync("EndOfGame", "draw");
        }

        game.Reset();
    }
}