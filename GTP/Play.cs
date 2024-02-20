namespace GTP;

class Play : GTP
{
    /// <summary>
    /// Start new Game.
    /// If you are white, pls run GenMove()
    /// </summary>
    /// <param name="settings"></param>
    /// <exception cref="Exception"></exception>
    public void Start(PlaySetting settings)
    {
        if (GTPProcess == null)
        {
            throw new Exception("GTP Process is not started!!");
        }
        GTPProcess.StandardInput.WriteLine("komi " + settings.komi);
        GTPProcess.StandardInput.WriteLine("boardsize " + settings.boardSize);
        if (!(settings.handicap == null || settings.handicap >= 0 || settings.handicap <= 9) && !(settings.boardSize == 9))
        {
            throw new Exception("handicap has some wrong");
        }
        else
        {
            GTPProcess.StandardInput.WriteLine("fixed_handicap " + settings.handicap);
        }
    }

    /// <summary>
    /// Generate new stone position
    /// </summary>
    /// <param name="color">your color</param>
    /// <returns>the position</returns>
    /// <exception cref="Exception"></exception>
    public string GenMove(string color)
    {
        if (GTPProcess == null)
        {
            throw new Exception("GTP Process is not started!!");
        }
        GTPProcess.StandardInput.WriteLine("genmove" + color);
        string output = GTPProcess.StandardOutput.ReadLine();
        return output;
    }

    /// <summary>
    /// Put stone and gen new stone po
    /// </summary>
    /// <param name="color">Ur stone color</param>
    /// <param name="position">The pos you putted</param>
    /// <returns>New stone pos</returns>
    /// <exception cref="Exception"></exception>
    public string Put(string color, string position)
    {
        if (GTPProcess == null)
        {
            throw new Exception("GTP Process is not Started");
        }

        GTPProcess.StandardInput.WriteLine("play " + color + " " + position);
        string output = GTPProcess.StandardOutput.ReadLine();
        return output;
    }
}
