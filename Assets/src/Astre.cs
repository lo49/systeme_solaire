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
	
	public string getName(){
		return name;
	}

	public void setName(string p){
		name = p;
	}	

	public Systeme getSysteme(){
		return systeme;
	}

	public Vector3 getPositionInit(){
		return positionInit;
	}

	public Vector3 getVitesseInit(){
		return vitesseInit;
	}
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

	public AstreController getAc(){
		return ac;
	}
	public void setAc(AstreController p){
		ac = p;
	}


}
		
