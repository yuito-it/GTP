using System.Diagnostics;

namespace GTP;

/// <summary>
/// GTPの全体的な制御
/// </summary>
public class GTP
{
    protected Process? GTPProcess;

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
    }

    /// <summary>
    /// GTPの終了
    /// </summary>
    public void Close()
    {
        GTPProcess?.Close();
    }
}
