namespace Training.CircleSquareGame.Api.Models;

public static class OXFieldMap
{
    public static readonly List<OXFieldPosition> RowA = new List<string> {"a1", "a2", "a3"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> RowB = new List<string> {"b1", "b2", "b3"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> RowC = new List<string> {"c1", "c2", "c3"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> Column1 = new List<string> {"a1", "b1", "c1"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> Column2 = new List<string> {"a2", "b2", "c2"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> Column3 = new List<string> {"a3", "b3", "c3"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> Diagonal = new List<string> {"a1", "b2", "c3"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> CounterDiagonal = new List<string> {"a3", "b2", "c1"}.Select(OXFieldPosition.FromString).ToList();
    public static readonly List<OXFieldPosition> FullBoard;

    static OXFieldMap()
    {
        FullBoard = new List<OXFieldPosition>();
        FullBoard.AddRange(RowA);
        FullBoard.AddRange(RowB);
        FullBoard.AddRange(RowC);
    }
}