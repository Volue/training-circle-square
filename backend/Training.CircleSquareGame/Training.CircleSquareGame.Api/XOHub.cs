using Microsoft.AspNetCore.SignalR;

namespace Training.CircleSquareGame.Api;

public class XOHub : Hub
{
    private const string O = "O";
    private const string X = "X";

    private static string _lastUsedSymbol = O;
    private static IDictionary<string, string> _gameBoard = new Dictionary<string, string>();
    private static bool _isGameOver = false;

    public async Task GetField(string fieldId)
    {
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, "");
    }
    
    public async Task SetField(string fieldId)
    {
        if(_isGameOver)
        {
            return;
        }
        if(IsFieldAlreadyUsed(fieldId))
        {
            return;
        }

        if (_lastUsedSymbol.Equals(X))
        {
            _lastUsedSymbol = O;
        }
        else 
        {
            _lastUsedSymbol = X;
        }
        
        await Clients.All.SendAsync("CurrentFieldValue", fieldId, _lastUsedSymbol);
        await Clients.All.SendAsync("NextUser", NextUser());

        _gameBoard.Add(fieldId, _lastUsedSymbol);

        if(IsAWinner())
        {
            _isGameOver = true;
            await Clients.All.SendAsync("Winner", _lastUsedSymbol);
        }
    }

    private bool IsFieldAlreadyUsed(string fieldId)
    {
        if(!_gameBoard.ContainsKey(fieldId))
        {
            return false;
        }
        if (_gameBoard[fieldId].Equals(X) ||_gameBoard[fieldId].Equals(O))
        {
            return true;
        }

        return false;
    }

    private bool IsAWinner()
    {
        if(AreTheFieldsEqual("a1", "a2", "a3")) return true;
        if(AreTheFieldsEqual("b1", "b2", "b3")) return true;
        if(AreTheFieldsEqual("c1", "c2", "c3")) return true;
        if(AreTheFieldsEqual("a1", "b1", "c1")) return true;
        if(AreTheFieldsEqual("a2", "b2", "c2")) return true;
        if(AreTheFieldsEqual("a3", "b3", "c3")) return true;
        if(AreTheFieldsEqual("a1", "b2", "c3")) return true;
        if(AreTheFieldsEqual("a3", "b2", "c1")) return true;
        return false;
    }

    private bool AreTheFieldsEqual(string field1, string field2, string field3)
    {
        if(!_gameBoard.ContainsKey(field1) || !_gameBoard.ContainsKey(field2) || !_gameBoard.ContainsKey(field3))
        {
            return false;
        }
        if (_gameBoard[field1].Equals(_gameBoard[field2]) && _gameBoard[field1].Equals(_gameBoard[field3]))
        {
            return true;
        }

        return false;
    }

    public string NextUser()
    {
        string nextUser;
        if(_lastUsedSymbol.Equals(X))
        {
            nextUser = O;
        }
        else
        {
            nextUser = X;
        }
        return nextUser;
    }

    public async Task NewGame()
    {
        _isGameOver = false;
        foreach (var key in _gameBoard.Keys)
        {
            _gameBoard.Remove(key);
            await Clients.All.SendAsync("CurrentFieldValue", key, "");
        }

        await Clients.All.SendAsync("NextUser", NextUser());
    }
}