using UnityEngine;
using System.Collections;

public class AstreVuController : MonoBehaviour {
	
	private Astre astre;
	private GameObject astreVu;
	private float factTaille = 190f; // plus cette valeur est faible, plus les astres éloignés seront représentés gros
	private Vector3 posAstreSuivi; // correspond à la distance entre la caméra et l'astre suivi

	private float tailleEchelle;
	private float echelleGlobale=1000f;
	private float multiplyTaille=45f;
	private float multiplyDistance=14.1f;
	private bool racTaillActive=true;
	private bool racDistActive=true;

	void Awake()
	{
		astreVu = gameObject; // astreVu est le GameObject auquel ce script est attaché
		astreVu.collider.isTrigger = true;
	}

	void Start()
	{
		astreVu.name = astre.getNom() + "Vue";
		calculTaille ();
	}


	public void calculPosEchelle(Vector3 posAstreOri) {
		Vector3 posAstreEchelle = getPosAstreVu(astre.getPosition()) - posAstreOri - posAstreSuivi;
		float distCamera = posAstreEchelle.magnitude;

		if (distCamera / tailleEchelle > 2500f * factTaille) {
			transform.localScale = distCamera * 0.25f / factTaille * Vector3.one;
			astreVu.renderer.enabled = false;
			transform.GetComponent<SphereCollider>().radius = 2f;
		} else {
			astreVu.renderer.enabled = true;
			if (distCamera / tailleEchelle > 10f * factTaille) {
				transform.localScale = distCamera * 0.65f / factTaille * Vector3.one;
				transform.GetComponent<SphereCollider>().radius = 2f;
			} else if (distCamera / tailleEchelle > factTaille) {
				transform.localScale = distCamera * 1f / factTaille * Vector3.one;
				transform.GetComponent<SphereCollider>().radius = 1f;
			} else {
				transform.localScale = tailleEchelle * Vector3.one;
				transform.GetComponent<SphereCollider>().radius = 0.5f;
			}
		}
		astreVu.transform.position = posAstreEchelle;
	}
	
	public Vector3 getPosAstreVu(Vector3 pos){
		if (racDistActive)
			pos = Mathf.Pow (pos.sqrMagnitude, 0.2f) * multiplyDistance * pos.normalized;
		else
			pos = pos * echelleGlobale;
		return pos;
	}
	
	public void calculTaille()
	{
		if (racTaillActive) {
			tailleEchelle = Mathf.Sqrt (astre.getTaille()) * multiplyTaille;
		} else {
			tailleEchelle = astre.getTaille() * multiplyTaille * echelleGlobale;
		}
		astreVu.transform.localScale = Vector3.one * tailleEchelle;
	}

	
	public void gererEchelle(int action)
	{
		if (action == 1)
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
		if (action == 2)
		{
			multiplyDistance=multiplyDistance/1.15f;
			if (multiplyDistance <= 0.01f)
				multiplyDistance = 0.01f;
		}
		if (action == 3)
		{
			multiplyDistance=multiplyDistance*1.15f;
			if (multiplyDistance >= 90f)
				multiplyDistance = 90f;
		}
		if (action == 4)
		{
			if (racTaillActive)
			{
				multiplyTaille=2f;
			}
			else
			{
				multiplyTaille=45f;
			}
			racTaillActive = (!racTaillActive);
			calculTaille();
		}
		if (action == 5)
		{
			multiplyTaille=multiplyTaille*1.5f;
			if (multiplyTaille>10000f)
				multiplyTaille=10000f;
			calculTaille();
		}
		if (action == 6)
		{
			multiplyTaille=multiplyTaille/1.5f;
			if (multiplyTaille<0.00002f)
				multiplyTaille=0.00002f;
			calculTaille();
		}
	}

	public void setPosAstreSuivi(Vector3 pos)
	{
		posAstreSuivi = pos;
	}

	public Astre getAstre()
	{
		return astre;
	}
	
	public void setAstre(Astre a)
	{
		astre = a;
	}
	
	public GameObject getAstreVu()
	{
		return astreVu;
	}

	public float getTaille(){
		return tailleEchelle;
	}

	public void setTaille(float t){
		tailleEchelle = t * multiplyTaille;
	}
}
