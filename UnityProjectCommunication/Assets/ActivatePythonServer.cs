using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ActivatePythonServer : MonoBehaviour
{
    public string path = @"D:\UnityProject\Unity3D-Python-Communication-master\PythonFiles";
    public string pythonFileName = "serverModified.py";
    // Start is called before the first frame update
    void Start()
    {
        run_cmd(path, pythonFileName);
    }

    public void run_cmd(string path, string pythonFileName){
        ProcessStartInfo psi = new ProcessStartInfo{
            FileName = @"C:\ProgramData\Anaconda3\python.exe",
            Arguments = $"\"{path}\\{pythonFileName}",
            UseShellExecute = true,
            CreateNoWindow = false,
            RedirectStandardOutput = false,
            RedirectStandardError = false
        };

        using (Process process = Process.Start(psi)){
            return ;
        }
    }
}
