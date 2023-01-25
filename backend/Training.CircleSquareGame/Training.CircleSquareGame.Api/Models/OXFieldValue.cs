namespace Training.CircleSquareGame.Api.Models;

public enum OXFieldValue
{
    Empty,
    O,
    X
}

public static class OXFieldValueExtensions {
    public static string ToValueString(this OXFieldValue value)
    {
        return value switch
        {
            OXFieldValue.Empty => "",
            OXFieldValue.O => "O",
            OXFieldValue.X => "X",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "OXFieldValue must be 'O' or 'X' or ''")
        };
    }
}