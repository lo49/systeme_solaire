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
	private GameObject systemeController; // gameObject qui contiendra 'SystemeController'
	private List<Astre> astres;	  // Liste des astres présents dans le systeme
	private GameObject MainCamera = new GameObject();

	public Systeme(string n)
	{
		systemeController = new GameObject (); // on créé un object vide
		nom = n;
		astres = new List<Astre>();
		systemeController.AddComponent<SystemeController>();	// on ajoute le script 'SystemeControler' à notre attribut / objet vide
		systemeController.GetComponent<SystemeController>().setSysteme(this); 	// On lie 'SystemeControler' à 'Systeme'
		MainCamera.AddComponent<Camera> ();
		MainCamera.tag = "MainCamera";
		MainCamera.AddComponent<CameraController> ();
	}

	public void addAstre(Astre a)
	{
		astres.Add(a);
	}
	public GameObject getCamera()
	{
		return MainCamera;
	}

	public List<Astre> getAstres()
	{
		return astres;
	}
	public Astre getAstre(int pos)
	{
		return astres[pos];
	}

	public string getNom()
	{
		return nom;
	}

}
