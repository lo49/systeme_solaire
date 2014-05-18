/// <summary>
/// Astre.cs
/// </summary>

using UnityEngine;
using System.Collections;

public class Astre
{
	protected string nom;
	protected string texture;
	protected string categorie;
	protected string description;
	protected Systeme systeme;
	protected Vector3 positionInit;
	protected Vector3 vitesseInit;
	protected float masseInit;
	protected float masse;
	protected float tailleInit;
	protected float taille;
	protected GameObject astreReel;
	protected AstreController ac;
	protected GameObject astreVu;
	protected AstreVuController avc;

	public Astre(string n, Systeme s, Vector3 pos, Vector3 v, float m, float t, string tex)
	{
		nom = n;
		positionInit = pos;
		vitesseInit = v;
		masseInit = m;
		masse = masseInit;
		tailleInit = t;
		taille = tailleInit;
		texture = tex;
		systeme = s;
		systeme.addAstre (this); // Lorsqu'on créé un Astre on l'ajoute à son système

		setAstreReel(GameObject.CreatePrimitive (PrimitiveType.Sphere));
		astreReel.AddComponent<AstreController>();
		ac=astreReel.GetComponent<AstreController> ();
		ac.setAstre (this);
		ac.setNom (nom);
		ac.setMasse (masse);
		ac.setPosition (positionInit);
		ac.setVitesse (vitesseInit);
		ac.setTaille (taille);
		astreReel = ac.getAstreReel();

		setAstreVu(GameObject.CreatePrimitive (PrimitiveType.Sphere));
		astreVu.AddComponent<AstreVuController>();
		avc=astreVu.GetComponent<AstreVuController> (); 
		avc.setAstre (this);
		astreVu = avc.getAstreVu ();
		Debug.Log(texture);
		astreVu.renderer.material = Resources.Load<Material>(texture);
	}

	public Vector3 getPositionVu(){
		return avc.getPosAstreVu (getPosition());
	}

	public float getTailleVu(){
		avc.calculTaille ();
		return avc.getTaille ();
	}

	// Accesseurs Mutateurs
	public string getNom()
	{
		return nom;
	}
	public void setNom(string p)
	{
		nom = p;
	}	
	public string getTexture()
	{
		return texture;
	}
	public void setTexture(string t)
	{
		texture = t;
	}
	public string getDescription()
	{
		return description;
	}
	public void setDescription(string d)
	{
		description = d;
	}
	public string getCategorie()
	{
		return categorie;
	}
	public void setCategorie(string c)
	{
		categorie = c;
	}
	public Systeme getSysteme(){
		return systeme;
	}

	public Vector3 getPositionInit(){
		return positionInit;
	}
	
	public Vector3 getPosition(){
		return astreReel.transform.position;
	}

	public Vector3 getVitesseInit(){
		return vitesseInit;
	}

	public float getMasseInit(){
		return masseInit;
	}

	public float getMasse(){
		return masse;
	}

	public float getTailleInit(){
		return tailleInit;
	}
	public float getTaille(){
		return taille;
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

	public void setAstreVu(GameObject a){
		astreVu = a;
	}

	public AstreVuController getAvc(){
		return avc;
	}

	public void setAvc(AstreVuController p){
		avc = p;
	}

}
		
