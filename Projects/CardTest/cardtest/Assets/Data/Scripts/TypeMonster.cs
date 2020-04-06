using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Cards/Monster")]
public class TypeMonster : CardType
{
    public override void OnSetType(CardVis vis)
	{
		base.OnSetType(vis);
		vis.statsHolder.SetActive(true);
	}
}
