using Microsoft.AspNetCore.SignalR;
using Training.CircleSquareGame.Api.Models;

namespace Training.CircleSquareGame.Api;

public class XOHub : Hub
{
    private readonly IOXGame _oxGame;

    public XOHub(IOXGame oxGame)
    {
        _oxGame = oxGame;
    }
    
    public async Task NewGame()
    {
        _oxGame.NewGame();
        var tasks = _oxGame.Fields.Select(f => Clients.All.SendAsync("CurrentFieldValue", f.Position.ToString(), f.Value.ToValueString()));
        await Task.WhenAll(tasks);
        await Clients.All.SendAsync("CurrentTextFieldValue", _oxGame.DisplayText.CurrentText);
    }

    public async Task GetText()
    {
        await Clients.All.SendAsync("CurrentTextFieldValue", _oxGame.DisplayText.CurrentText);
    }
    
    public async Task GetVictoryCount()
    {
        await Clients.All.SendAsync("CurrentVictoryCount", _oxGame.VictoryCount[OXFieldValue.O], _oxGame.VictoryCount[OXFieldValue.X]);
    }
    
    public async Task GetField(string fieldId)
    {
        var oxField = _oxGame.GetField(OXFieldPosition.FromString(fieldId));
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, oxField.Value.ToValueString());
    }
    
    public async Task SetField(string fieldId)
    {
        if (_oxGame.GameIsOver) return;
        var value = _oxGame.CurrentPlayer;
        _oxGame.PlayerSetField(OXFieldPosition.FromString(fieldId));
        _oxGame.CheckVictoryConditions();
        _oxGame.CheckForDraw();
        if (!_oxGame.GameIsOver)
        {
            _oxGame.SetNextPlayer();
        }
        else
        {
            await Clients.All.SendAsync("CurrentVictoryCount", _oxGame.VictoryCount[OXFieldValue.O], _oxGame.VictoryCount[OXFieldValue.X]);
        }
        await Clients.All.SendAsync("CurrentTextFieldValue", _oxGame.DisplayText.CurrentText);
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, value.ToValueString());
    }
}