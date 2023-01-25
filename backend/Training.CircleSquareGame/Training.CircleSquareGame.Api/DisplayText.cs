using Training.CircleSquareGame.Api.Abstract;
using Training.CircleSquareGame.Api.Models;

namespace Training.CircleSquareGame.Api;

public class DisplayText : IDisplayText
{
    public string CurrentText { get; set; } = string.Empty;
    
    public void SetPlayerWonText(OXFieldValue value) => CurrentText = $"Player {value.ToValueString()} won!";
    public void SetNextPlayerText(OXFieldValue value) => CurrentText = $"Player {value.ToValueString()}";
    public void SetDrawText() => CurrentText = "Game ended with draw";
}