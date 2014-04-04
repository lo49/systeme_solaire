/// <summary>
/// Systeme.cs
/// MODELE
/// créé par jonathan le 31/03
/// modifié par ...
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Systeme
{
	private string nom;
	public GameObject gameobject; // gameObject vide qui ne sert qu'a appeler 'SystemeControler'
	public List<Astre> astres;
	private SystemeControler sc; // script lié à cet objet Systeme

	public Systeme(string n)
	{
		gameobject = new GameObject (); // on créé un object vide
		nom = n;
		astres = new List<Astre>();
		gameobject.AddComponent ("SystemeControler"); // on ajoute le script 'SystemeControler' à notre attribut / objet vide
		sc = (SystemeControler)gameobject.GetComponent ("SystemeControler"); // on récupère ce 'SystemeControler' et on le met dans l'attribut sc
		sc.systeme = this; // On lie 'SystemeControler' à 'Systeme' par attribut
	}

	public void addAstre(Astre a)
	{
		astres.Add(a);
	}
}
