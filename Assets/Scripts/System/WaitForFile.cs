using UnityEngine;
using System.IO;

public class WaitForFile : CustomYieldInstruction
{
    public string FilePath { get; private set; }

    public bool IsCompleted { get; private set; }

    private float timeoutTime;

    private bool existance;

    public override bool keepWaiting
    {
        get
        {
            IsCompleted = File.Exists(FilePath) == existance;
            if (IsCompleted)
            {
                return false;
            }

            if (Time.realtimeSinceStartup >= timeoutTime)
            {
                return false;
            }

            return true;
        }
    }

    public WaitForFile(string filePath, bool existance, float timeout)
    {
        this.timeoutTime = Time.realtimeSinceStartup + timeout;
        this.existance = existance;

        FilePath = filePath;
    }
}