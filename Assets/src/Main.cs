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
		Systeme s1 = new Systeme ("système solaire");
		Astre soleil = new Astre ("soleil", s1, new Vector3 (0f, 0f, 0f), new Vector3 (0f, 0f, 0f), 1989100f, 10*6.96f);
		Astre Terre = new Astre ("Terre", s1, new Vector3 (1496F, 0F, 0F), new Vector3 (0F, 0F, 29.783F), 5.89F, 10*0.06367F);
		Astre Lune = new Astre ("Lune", s1, new Vector3 (1496F, 0F, 3.84399F), new Vector3 (1.022F, 0F, 29.783F), 0.073477F, 10*0.01736F);
	}
	

}
