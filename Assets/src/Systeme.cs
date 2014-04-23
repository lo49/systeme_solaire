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
//	private GameObject camera = new GameObject();
	protected string nom;
	protected GameObject gameobject; // gameObject vide qui ne sert qu'a appeler 'SystemeControler'
	protected List<Astre> astres;
	protected SystemeControler sc; // script lié à cet objet Systeme
	protected string URLImage;
	protected string description;
	protected Etoile etoile;
	protected List<Planete> planetes;
	protected string fichierXML;

	public Systeme(string n)
	{
		gameobject = new GameObject (); // on créé un object vide
		nom = n;
		astres = new List<Astre>();
		gameobject.AddComponent ("SystemeControler"); // on ajoute le script 'SystemeControler' à notre attribut / objet vide
		sc = (SystemeControler)gameobject.GetComponent ("SystemeControler"); // on récupère ce 'SystemeControler' et on le met dans l'attribut sc
		sc.systeme = this; // On lie 'SystemeControler' à 'Systeme' par attribut
//		camera.AddComponent<Camera> ();
//		camera.AddComponent<CameraController> ();
	}

	public void addAstre(Astre a)
	{
		astres.Add(a);
	}

	public string getNom(){
		return nom;
	}
	public string getURLimage(){
		return URLImage;
	}
	public string getDescription()
	{
		return description;
	}
	public Etoile getEtoile(){
		return etoile;
	}
	public List<Planete> getListPlanetes(){
		return planetes;
	}
	public string getFichierXML(){
		return fichierXML;
	}
	public void setFichierXML(string nomfichier){
		fichierXML = nomfichier;
	}


}

