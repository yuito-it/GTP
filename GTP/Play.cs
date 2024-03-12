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
        gtpProcess = process ?? throw new ArgumentNullException(nameof(process));
    }

    /// <summary>
    /// Start new Game.
    /// If you are white, pls run GenMove()
    /// </summary>
    /// <param name="settings"></param>
    /// <exception cref="Exception"></exception>
    public string Start(PlaySetting settings)
    {
        ValidateSettings(settings);

        SendCommand($"komi {settings.komi}");
        ReceiveResponse();

        SendCommand($"fixed_handicap {settings.handicap}");
        string output = ReceiveResponse();

        return output;
    }

    /// <summary>
    /// Generate new stone position
    /// </summary>
    /// <param name="color">your color</param>
    /// <returns>the position</returns>
    /// <exception cref="Exception"></exception>
    public string GenMove(string color)
    {
        SendCommand($"genmove {color}");
        string output = ReceiveResponse();

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
        SendCommand($"play {color} {position}");
        string output = ReceiveResponse();
        return output;
    }

    /// <summary>
    /// Undo
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void Undo()
    {
        SendCommand("undo");
        ReceiveResponse();
    }

    /// <summary>
    /// Get Score
    /// </summary>
    /// <returns>Score (color+float)</returns>
    /// <exception cref="Exception"></exception>
    public string GetScore()
    {
        SendCommand("final_score");
        string output = ReceiveResponse();

        return output;
    }

    /// <summary>
    /// コマンド送信
    /// </summary>
    /// <param name="command"></param>
    /// <exception cref="InvalidOperationException"></exception>
    private void SendCommand(string command)
    {
        if (gtpProcess == null || gtpProcess.HasExited)
        {
            throw new InvalidOperationException("GTP Process is not running.");
        }

        gtpProcess.StandardInput.WriteLine(command);
    }

    /// <summary>
    /// output取得
    /// </summary>
    /// <returns></returns>
    private string ReceiveResponse()
    {
        string output;
        do
        {
            output = gtpProcess?.StandardOutput.ReadLine();
        } while (string.IsNullOrEmpty(output) || output[0] != '=');

        return output;
    }

    /// <summary>
    /// Playsettingのnullチェック
    /// </summary>
    /// <param name="setting"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void ValidateSettings(PlaySetting setting)
    {
        if (setting == null) throw new ArgumentNullException(nameof(setting));
        if (setting.handicap.HasValue && (setting.handicap < 0 || setting.handicap > 9))
        {
            throw new ArgumentOutOfRangeException(nameof(setting.handicap), "Handicap must be between 0 and 9.");
        }
    }
}
