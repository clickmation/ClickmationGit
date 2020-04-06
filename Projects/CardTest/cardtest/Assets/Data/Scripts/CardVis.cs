using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardVis : MonoBehaviour
{
	public Card card;
	public CardVisProperties[] properties;
	public GameObject statsHolder;
	public Image typeImage;
	public Image cardTemplate;
	
	private void Start()
	{
		LoadCard(card);
	}
	
	public void LoadCard(Card c)
	{
		if (c == null)
			return;
		
		card = c;
		
		c.cardType.OnSetType(this);
		
		for (int i = 0; i < c.properties.Length; i++)
		{
			CardProperties cp = c.properties[i];
			
			CardVisProperties p = GetProperty(cp.element);
			if(p == null)
				continue;
			
			if(cp.element is ElementInt)
			{
				p.text.text = cp.intValue.ToString();
			}
			else if(cp.element is ElementText)
			{
				p.text.text = cp.stringValue;
			}
			else if(cp.element is ElementImage)
			{
				p.img.sprite = cp.sprite;
			}
		}
	}
	
	public CardVisProperties GetProperty(Element e)
	{
		CardVisProperties result = null;
		
		for (int i = 0; i < properties.Length; i++)
		{
			if (properties[i].element == e)
			{
				result = properties[i];
				break;
			}
		}
		
		return result;
	}
}
