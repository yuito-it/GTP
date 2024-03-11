using System.Diagnostics;

namespace GTP;

/// <summary>
/// GTPの全体的な制御
/// </summary>
public class GTP2
{
    public Play play;
    public Process? GTPProcess;

    /// <summary>
    /// Start GTP Process
    /// </summary>
    /// <param name="GTPPath">Path of GTP.exe</param>
    /// <param name="Param">Cmd param</param>
    public void Run(string GTPPath, string? Param)
    {
        if (GTPProcess == null)
        {
            ProcessStartInfo psInfo = new()
            {
                FileName = GTPPath,
                Arguments = Param,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };

            GTPProcess = Process.Start(psInfo);
        }

        play = new Play(GTPProcess);
    }

    /// <summary>
    /// GTPの終了
    /// </summary>
    public void Close()
    {
        GTPProcess?.Close();
    }
}
