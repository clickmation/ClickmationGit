using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Turns&Phases/Player Control Phase")]
public class PlayerControlPhase : Phase
{
    public CM.GameStates.GameState PlayerControlState;

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
            Settings.gameManager.SetState(PlayerControlState);
            Settings.gameManager.onPhaseChanged.Raise();
            Settings.gameManager.currentPlayer.canUseCard = true;
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
