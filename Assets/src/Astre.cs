/// <summary>
/// Astre.cs
/// </summary>

using UnityEngine;
using System.Collections;

public class Astre
{
	protected string name;
	protected Systeme systeme;
	protected Vector3 positionInit;
	protected Vector3 vitesseInit;
	protected float masseInit;
	protected float tailleInit;
	protected GameObject astreReel;
	protected AstreController ac;
	protected GameObject astreVu;

	public Astre(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t)
	{
		name = n;
		positionInit = pos;
		vitesseInit = v;
		masseInit = m;
		tailleInit = t;
		systeme = s;
		systeme.addAstre (this); // Lorsqu'on créé un Astre on l'ajoute à son système
		setAstreReel(GameObject.CreatePrimitive (PrimitiveType.Sphere));
		astreReel.AddComponent<AstreController>();
		ac=astreReel.GetComponent<AstreController> (); 
		ac.setAstre (this);
		astreVu = ac.getAstreVu ();
	}
	
	public string getNom(){
		return nom;
	}

<<<<<<< HEAD
	public void setNom(string p){
		nom = p;
=======
	public void setName(string p){
		name = p;
>>>>>>> cfe21fed29526a129cc85081ec64b3940c26cbdc
	}	

	public Systeme getSysteme(){
		return systeme;
	}

<<<<<<< HEAD
	public void setSysteme(Systeme p){
		systeme = p;
	}

	public Vector3 getPosition(){
		return position;
	}
	public void setPosition(Vector3 p){
		position = p;
=======
	public Vector3 getPositionInit(){
		return positionInit;
>>>>>>> cfe21fed29526a129cc85081ec64b3940c26cbdc
	}

	public Vector3 getVitesseInit(){
		return vitesseInit;
	}
<<<<<<< HEAD
	public void setVitesseInit(Vector3 p){
		vitesseInit = p;
	}
	public float getMasse(){
		return masse;
	}
	public void setMasse(float p){
		masse = p;
	}

	public float getTaille(){
		return taille;
	}
	public void setTaille(float p){
		taille = p;
	}

	public GameObject getGameObject(){
		return gameobject; 
	}
	public void setGameObject(GameObject p){
		gameobject = p;
	}
	public GameObject getAstreEchelle(){
		return astreEchelle;
	}
	public void setAstreEchelle(GameObject p){
		astreEchelle = p;
	}
=======
	public float getMasseInit(){
		return masseInit;
	}
	public float getTailleInit(){
		return tailleInit;
	}
	public GameObject getAstreReel(){
		return astreReel;
	}

	public void setAstreReel(GameObject a){
		astreReel = a;
	}

>>>>>>> cfe21fed29526a129cc85081ec64b3940c26cbdc
	public AstreController getAc(){
		return ac;
	}
	public void setAc(AstreController p){
		ac = p;
	}


}
		
