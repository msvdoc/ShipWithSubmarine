
public static class Score
{
    private static int _score = 0;

    public static int GetScore()
    {
        return _score;
    }
    public static void IncraseScore()
    {
        _score++;
    }


    public static void ResetScore()
    { 
        _score = 0;
    }

}
