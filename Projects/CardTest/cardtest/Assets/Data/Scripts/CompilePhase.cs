using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Turns&Phases/Compile Phase")]
public class CompilePhase : Phase
{
    public override bool IsComplete()
    {
        if (forceExit)
        {
            forceExit = false;
            return true;
        }
        
        return false;
    }

    public override void OnStartPhase()
    {

    }

    public override void OnEndPhase()
    {

    }
}
