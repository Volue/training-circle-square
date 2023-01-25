using Training.CircleSquareGame.Api.Abstract;
using Training.CircleSquareGame.Api.Models;

namespace Training.CircleSquareGame.Api;

public class OXGame : IOXGame
{
    public IDisplayText DisplayText { get; }
    public List<OXField> Fields { get; private set; }= new();
    public OXFieldValue CurrentPlayer { get; private set; }= OXFieldValue.Empty;
    public Dictionary<OXFieldValue, int> VictoryCount { get; } = new()
    {
        {OXFieldValue.O, 0},
        {OXFieldValue.X, 0}
    };
    public bool GameIsOver { get; private set; }

    public OXGame(IDisplayText displayText)
    {
        DisplayText = displayText;
        NewGame();
    }

    public void NewGame()
    {
        var positions = new List<OXFieldPosition>();
        positions.AddRange(OXFieldMap.FullBoard);
        Fields = positions.Select(p => new OXField(p)).ToList();
        CurrentPlayer = OXFieldValue.O;
        DisplayText.SetNextPlayerText(CurrentPlayer);
        GameIsOver = false;
    }

    public void PlayerSetField(OXFieldPosition position)
    {
        if (GameIsOver) return;
        var oxField = GetField(position);
        ValidateThatFieldIsEmpty(oxField);
        oxField.Value = CurrentPlayer;
    }

    public void SetNextPlayer()
    {
        CurrentPlayer = CurrentPlayer == OXFieldValue.O ? OXFieldValue.X : OXFieldValue.O;
        DisplayText.SetNextPlayerText(CurrentPlayer);
    }

    public void CheckVictoryConditions()
    {
        var victoryConditionChecks = new List<bool>
        {
            CheckLine(OXFieldMap.RowA, CurrentPlayer),
            CheckLine(OXFieldMap.RowB, CurrentPlayer),
            CheckLine(OXFieldMap.RowC, CurrentPlayer),
            CheckLine(OXFieldMap.Column1, CurrentPlayer),
            CheckLine(OXFieldMap.Column2, CurrentPlayer),
            CheckLine(OXFieldMap.Column3, CurrentPlayer),
            CheckLine(OXFieldMap.Diagonal, CurrentPlayer),
            CheckLine(OXFieldMap.CounterDiagonal, CurrentPlayer)
        };
        if (!victoryConditionChecks.Any(v => v)) return;
        GameIsOver = true;
        DisplayText.SetPlayerWonText(CurrentPlayer);
        VictoryCount[CurrentPlayer]++;
    }
    
    public void CheckForDraw()
    {
        if (Fields.Any(f => f.Value == OXFieldValue.Empty)) return;
        GameIsOver = true;
        DisplayText.SetDrawText();
    }

    public OXField GetField(OXFieldPosition position) => Fields.First(f => f.Position == position);

    private bool CheckLine(List<OXFieldPosition> positions, OXFieldValue value)
    {
        return positions.All(p => Fields.Single(f => f.Position == p).Value == value);
    }

    private static void ValidateThatFieldIsEmpty(OXField field)
    {
        if (field.Value != OXFieldValue.Empty) throw new InvalidOperationException("Field is not empty");
    }
    
    
}