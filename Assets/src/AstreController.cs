using UnityEngine;
using System.Collections;

public class AstreController : MonoBehaviour {

	public Astre astre;
	private float taille;
	private float multiplyTaille=1;
	private float multiplyDistance=1;
	private bool racTaillActive=false;
	private bool racDistActive=false;
	private float tailleTmp;
	// Use this for initialization

	void Start () {
		Destroy (astre.astreEchelle.GetComponent ("SphereCollider"));
		this.name = astre.nom + "Vue";
		taille = astre.taille*multiplyTaille;
		astre.astreEchelle.transform.localScale = new Vector3(taille,taille,taille);
	}

	void LateUpdate() {
		Vector3 posReelle = astre.gameobject.transform.position;
		if (racDistActive) {
			astre.astreEchelle.transform.position = Mathf.Pow (posReelle.sqrMagnitude, 0.20f) * posReelle.normalized * multiplyDistance;
				} else {
						astre.astreEchelle.transform.position = posReelle * multiplyDistance;
				}
	}

	public void gererEchelle(int bouton)
	{
		if (bouton == 1)
		{
			if(racDistActive)
			{
				multiplyDistance=1f;
			}
			else
			{
				multiplyDistance=14.1f;
			}
			racDistActive=(!racDistActive);
		}
		if (bouton == 2)
		{
			multiplyDistance=multiplyDistance/1.15f;
		}
		if (bouton == 3)
		{
			multiplyDistance=multiplyDistance*1.15f;
		}
		if (bouton == 4)
		{
			if (racTaillActive)
			{
				multiplyTaille=1f;
			}
			else
			{
				multiplyTaille=45f;
			}
			racTaillActive = (!racTaillActive);
			calculTaille();
		}
		if (bouton == 5)
		{
			multiplyTaille=multiplyTaille*1.15f;
			calculTaille();
		}
		if (bouton == 6)
		{
			multiplyTaille=multiplyTaille/1.15f;
			calculTaille();
		}
	}

	public void calculTaille()
	{
		if (racTaillActive) {
						taille = Mathf.Sqrt (astre.taille) * multiplyTaille;
				} else {
						taille = astre.taille * multiplyTaille;
				}
		astre.astreEchelle.transform.localScale = new Vector3(taille,taille,taille);
	}

}
