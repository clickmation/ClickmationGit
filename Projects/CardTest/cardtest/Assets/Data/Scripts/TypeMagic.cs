using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Cards/Magic")]
public class TypeMagic : CardType
{
    public override void OnSetType(CardVis vis)
	{
		base.OnSetType(vis);
		vis.statsHolder.SetActive(false);
	}
}
