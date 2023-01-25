namespace Training.CircleSquareGame.Api.Models;

public class OXField
{
    public readonly OXFieldPosition Position;
    public OXFieldValue Value { get; set; }

    public OXField(OXFieldPosition position, OXFieldValue value = OXFieldValue.Empty)
    {
        Position = position;
        Value = value;
    }
}