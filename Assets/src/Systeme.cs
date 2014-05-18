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
	private string URLimage;
	private string description;
	private string fichierXML;
	private Etoile etoile;
	private GameObject systemeController; // gameObject qui contiendra 'SystemeController'
	private List<Astre> astres;	  // Liste des astres présents dans le systeme
	private List<Planete> planetes;	  // Liste des planetes présents dans le systeme
	private GameObject MainCamera = new GameObject();

	public Systeme(string n, string u, string d)
	{
		systemeController = new GameObject (); // on créé un GameObject vide
		nom = n;
		URLimage = u;
		description = d;
		astres = new List<Astre>();
		planetes = new List<Planete>();
		systemeController.AddComponent<SystemeController>();	// on ajoute le script 'SystemeControler' à notre attribut / objet vide
		systemeController.GetComponent<SystemeController>().setSysteme(this); 	// On lie 'SystemeControler' à 'Systeme'
		MainCamera.AddComponent<Camera> ();
		MainCamera.tag = "MainCamera";
		MainCamera.AddComponent<CameraController> ();
	}

	// METHODES PUBLIQUES
	public void addAstre(Astre a)
	{
		astres.Add(a);
	}
	public void addPlanete(Planete a)
	{
		planetes.Add(a);
	}
	public void addEtoile(Etoile e)
	{
		etoile = e;
	}

	// ACCESSEURS
	public Etoile getEtoile()
	{
		return etoile;
	}
	public string getNom()
	{
		return nom;
	}
	public string getURLimage()
	{
		return URLimage;
	}
	public string getFichierXML()
	{
		return fichierXML;
	}
	public string getDescription()
	{
		return description;
	}
	public GameObject getCamera()
	{
		return MainCamera;
	}
	public List<Astre> getAstres()
	{
		return astres;
	}
	public List<Planete> getPlanetes()
	{
		return planetes;
	}
	public Astre getAstre(int pos)
	{
		return astres[pos];
	}

	// MUTATEURS
	public void setURLimage(string u)
	{
		URLimage=u;
	}
	public void setFichierXML(string u)
	{
		fichierXML=u;
	}
}
