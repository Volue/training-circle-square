namespace Training.CircleSquareGame.Api.Models;

public record struct OXFieldPosition
{
    public string Row {get; init;}
    public string Column {get; init;}
    
    public static OXFieldPosition FromString(string position)
    {
        return new OXFieldPosition
        {
            Row = position.Substring(0, 1),
            Column = position.Substring(1, 1)
        };
    }

    public override string ToString() => $"{Row}{Column}";
}