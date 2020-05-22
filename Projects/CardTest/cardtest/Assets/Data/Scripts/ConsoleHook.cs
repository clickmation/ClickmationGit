using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Console/Hook")]
public class ConsoleHook : ScriptableObject
{
    [System.NonSerialized]
    public ConsoleManager consoleManager;

    public void RegisterEvent(string e, Color color)
    {
        consoleManager.RegisterEvent(e, color);
    }
}
