/// <summary>
/// Astre.cs
/// </summary>

using UnityEngine;
using System.Collections;

public class Astre
{
	protected string nom;
	protected Systeme systeme;
	protected Vector3 position;
	protected Vector3 vitesseInit;
	protected float masse;
	protected float taille;
	protected GameObject gameobject; // gameObject qui représente la planète (une sphère) dans la vue 3d
	protected GameObject astreEchelle;
	protected AstreController ac;


	public Astre(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t)
	{
		nom = n;
		position = pos;
		vitesseInit = v;
		masse = m;
		taille = t;
		systeme = s;
		systeme.addAstre (this); // Lorsqu'on créé un Astre on l'ajoute à son système
		initVueAstre ();
		initGameObject ();
	}
	
	public void initGameObject() // j'aimerais que cette fonction soit dans un script AstreControler mais j'ai rencontré des probèmes ...
	{
		gameobject = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		gameobject.transform.position = position;
		gameobject.transform.localScale = new Vector3(taille,taille,taille);
		gameobject.AddComponent("Rigidbody");
		gameobject.rigidbody.useGravity = false;
		gameobject.rigidbody.velocity = vitesseInit;
		gameobject.rigidbody.mass = masse;
		gameobject.rigidbody.name = nom;
		gameobject.renderer.enabled = false;
	}

	public void initVueAstre()
	{
		astreEchelle = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		astreEchelle.collider.enabled = false;
		astreEchelle.AddComponent ("AstreController"); 
		ac = (AstreController)astreEchelle.GetComponent ("AstreController");
		ac.astre = this;
	}
	
	public string getName(){
		return name;
	}

	public void setName(string p){
		getName() = p;
	}	

	public Systeme getSysteme(){
		return systeme;
	}

	public void setSysteme(Systeme p){
		getSysteme() = p;
	}

	public Vector3 getPosition(){
		return position;
	}
	public void setPosition(Vector3 p){
		getPosition() = p;
	}

	public Vector3 getVitesseInit(){
		return vitesseInit;
	}
	public void setVitesseInit(Vector3 p){
		getVitesseInit() = p;
	}
	public float getMasse(){
		return masse;
	}
	public void setMasse(float p){
		getMasse() = p;
	}

	public float getTaille(){
		return taille;
	}
	public void setTaille(float p){
		getTaille() = p;
	}

	public GameObject getGameObject(){
		return gameobject; 
	
	}

	public void setGameObject(GameObject p){
		getGameObject() = p;
	}
	public GameObject getAstreEchelle(){
		return astreEchelle;
	}
	public void setAstreEchelle(GameObject p){
		getAstreEchelle() = p;
	}
	public AstreController getAc(){
		return ac;
	}
	public void setAc(AstreController p){
		getAc() = p;
	}


}
		
