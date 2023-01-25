using Training.CircleSquareGame.Api.Abstract;
using Training.CircleSquareGame.Api.Models;

namespace Training.CircleSquareGame.Api;

public interface IOXGame
{
    IDisplayText DisplayText { get; }
    List<OXField> Fields { get; }
    OXFieldValue CurrentPlayer { get; }
    bool GameIsOver { get; }
    Dictionary<OXFieldValue, int> VictoryCount { get; }
    void NewGame();
    void PlayerSetField(OXFieldPosition position);
    void SetNextPlayer();
    void CheckVictoryConditions();
    OXField GetField(OXFieldPosition position);
    void CheckForDraw();
}