using System.Collections;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void Execute(float d);
}
