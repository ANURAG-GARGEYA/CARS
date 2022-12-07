using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public TextMeshPro textMesh;

    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        if (textMesh.text.Length > 1000)
        {
            textMesh.text = message + "\n";
        }
        else
        {
            textMesh.text += message + "\n";
        }
    }
}
