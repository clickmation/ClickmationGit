using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Turns&Phases/Turn")]
public class Turn : ScriptableObject
{
    public PlayerHolder player;
    [System.NonSerialized]
    public int index = 0;
    public PhaseVariable currentPhase;
    public Phase[] phases;

    public bool Execute()
    {
        bool result = false;

        currentPhase.value = phases[index];
        phases[index].OnStartPhase();

        bool phaseIsComplete = phases[index].IsComplete();

        if (phaseIsComplete)
        {
            phases[index].OnEndPhase();
            index++;
            if (index > phases.Length - 1)
            {
                index = 0;
                result = true;
            }
        }

        return result;
    }

    public void EndCurrentPhase()
    {
        phases[index].forceExit = true;
    }
}
