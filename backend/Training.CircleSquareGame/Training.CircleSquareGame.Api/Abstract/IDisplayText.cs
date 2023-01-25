using Training.CircleSquareGame.Api.Models;

namespace Training.CircleSquareGame.Api.Abstract;

public interface IDisplayText
{
    string CurrentText { get; }
    void SetPlayerWonText(OXFieldValue value);
    void SetNextPlayerText(OXFieldValue value);
    void SetDrawText();
}