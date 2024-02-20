using System.Diagnostics;

namespace GTP;

public class GTP
{
    public Process? GTPProcess;

    /// <summary>
    /// Start GTP Process
    /// </summary>
    /// <param name="GTPPath">Path of GTP.exe</param>
    /// <param name="Param">Cmd param</param>
    public void Run(string GTPPath, string? Param)
    {
        if (Param == null)
        {
            GTPProcess = Process.Start(GTPPath);
        }
        else
        {
            GTPProcess = Process.Start(GTPPath, Param);
        }
    }
    
    public void Close()
    {
        GTPProcess?.Close();
    }
}
