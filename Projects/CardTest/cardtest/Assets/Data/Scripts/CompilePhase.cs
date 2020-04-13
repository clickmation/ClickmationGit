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
        if (!isInit)
        {
            Debug.Log(this.phaseUIText + "starts");
            Settings.gameManager.SetState(null);
            Settings.gameManager.onPhaseChanged.Raise();
            isInit = true;
        }
    }

    public override void OnEndPhase()
    {
        if (isInit)
        {
            Settings.gameManager.SetState(null);
            isInit = false;
        }
    }
}
