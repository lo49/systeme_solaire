/// <summary>
/// Main.cs
/// CONTROLER (MAIN)
/// créé par nicolas le 26/03
/// modifié par jonathan le 31/03
/// modifié par ...
/// </summary>

using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour // Main attaché à un objet vide pour etre appelé 
{
	// Use this for initialization
	void Start ()
	{
		Systeme s =XML.recupererSysteme ("solaire.xml");
	}
	

}
