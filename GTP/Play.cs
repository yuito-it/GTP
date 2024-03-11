using System.Diagnostics;

namespace GTP;

public class Play
{
    private Process gtpProcess;

    /// <summary>
    /// gtpProcessを引数にとり、パイプできるようにする
    /// </summary>
    public Play(Process process)
    {
        gtpProcess = process;
    }

    /// <summary>
    /// Start new Game.
    /// If you are white, pls run GenMove()
    /// </summary>
    /// <param name="settings"></param>
    /// <exception cref="Exception"></exception>
    public string Start(PlaySetting settings)
    {
        if (gtpProcess == null)
        {
            throw new Exception("GTP Process is not started!!");
        }
        gtpProcess.StandardInput.WriteLine("komi " + settings.komi);
        string output = gtpProcess.StandardOutput.ReadLine();
        while (!(output[0] == '='))
        {
            output = gtpProcess.StandardOutput.ReadLine();
        }
        if (!(settings.handicap == null || settings.handicap >= 0 || settings.handicap <= 9) && !(settings.boardSize == 9))
        {
            throw new Exception("handicap has some wrong");
        }
        else
        {
            gtpProcess.StandardInput.WriteLine("fixed_handicap " + settings.handicap);
            output = gtpProcess.StandardOutput.ReadLine();
            while (output == null || output.Length == 0)
            {
                output = gtpProcess.StandardOutput.ReadLine();
            }
            while (!(output[0] == '='))
            {
                output = gtpProcess.StandardOutput.ReadLine();
                while (output == null || output.Length == 0)
                {
                    output = gtpProcess.StandardOutput.ReadLine();
                }
            }
            return output;
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
        if (gtpProcess == null)
        {
            throw new Exception("GTP Process is not started!!");
        }
        gtpProcess.StandardInput.WriteLine("genmove " + color);
        string output = gtpProcess.StandardOutput.ReadLine();
        while (!(output[0] == '='))
        {
            output = gtpProcess.StandardOutput.ReadLine();
        }
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
        if (gtpProcess == null)
        {
            throw new Exception("GTP Process is not Started");
        }

        gtpProcess.StandardInput.WriteLine("play " + color + " " + position);
        string output = gtpProcess.StandardOutput.ReadLine();
        while (output == null || output.Length == 0)
        {
            output = gtpProcess.StandardOutput.ReadLine();
        }
        while (!(output[0] == '='))
        {
            output = gtpProcess.StandardOutput.ReadLine();
            while (output == null || output.Length == 0)
            {
                output = gtpProcess.StandardOutput.ReadLine();
            }
        }
        return output;
    }

    /// <summary>
    /// Undo
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Undo()
    {
        if (gtpProcess == null)
        {
            throw new Exception("GTP Process is not Started");
        }

        gtpProcess.StandardInput.WriteLine("undo");
    }

    /// <summary>
    /// Get Score
    /// </summary>
    /// <returns>Score (color+float)</returns>
    /// <exception cref="Exception"></exception>
    public string GetScore()
    {
        if (gtpProcess == null)
        {
            throw new Exception("GTP Process is not Started");
        }

        gtpProcess.StandardInput.WriteLine("final_score");

        string output = gtpProcess.StandardOutput.ReadLine();
        while (output == null || output.Length == 0)
        {
            output = gtpProcess.StandardOutput.ReadLine();
        }
        while (!(output[0] == '='))
        {
            output = gtpProcess.StandardOutput.ReadLine();
            while (output == null || output.Length == 0)
            {
                output = gtpProcess.StandardOutput.ReadLine();
            }
        }
        return output;
    }
}
