using UnityEngine;
using System.Collections;

public class AstreController : MonoBehaviour {

	private Astre astre;
	private float tailleReelle;
	private float tailleEchelle;
	private float multiplyTaille=7f;
	private float multiplyDistance=1f;
	private bool racTaillActive=false;
	private bool racDistActive=false;
	private GameObject astreReel;
	private GameObject astreVu;
	private GameObject cameraOri;

	void Awake (){
		astreReel = gameObject;
		Destroy (astreReel.GetComponent<Renderer>());
		Destroy (astreReel.GetComponent<MeshFilter>());
		Destroy (astreReel.GetComponent<SphereCollider>());
		astreReel.AddComponent<Rigidbody>();
		astreReel.rigidbody.useGravity = false;
		astreReel.renderer.enabled = false;

		astreVu = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		astreVu.AddComponent<GetAstre>();
	}

	void Start () {
		tailleReelle = astre.getTailleInit();
		astreReel.transform.position = astre.getPositionInit();
		astreReel.transform.localScale = Vector3.one * astre.getTailleInit();
		astreReel.rigidbody.velocity = astre.getVitesseInit();
		astreReel.rigidbody.mass = astre.getMasseInit();
		astreReel.rigidbody.name = astre.getName();
		
		astreVu.GetComponent<GetAstre> ().setAstre (astre);
		astreVu.name = astre.getName() + "Vue";
		tailleEchelle = tailleReelle*multiplyTaille;
		astreVu.transform.localScale = Vector3.one * tailleEchelle;
		gererEchelle(1);
		gererEchelle(4); // Echelle initiale à la racine
	}

	public void calculPosEchelle() {
		Vector3 posTmp = astreReel.transform.position;
		if (racDistActive) {
						posTmp = Mathf.Pow (posTmp.sqrMagnitude, 0.20f) * posTmp.normalized * multiplyDistance;
				} else {
						posTmp = posTmp * multiplyDistance;
		}
		astreVu.transform.position=posTmp;
		if (cameraOri!=null){
			cameraOri.transform.position = posTmp;
		}
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
			if (multiplyDistance <= 1f)
				multiplyDistance = 1f;
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
				multiplyTaille=7f;
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
			multiplyTaille=multiplyTaille*1.15f;
			if (multiplyTaille>90f)
				multiplyTaille=90f;
			calculTaille();
		}
		if (action == 6)
		{
			multiplyTaille=multiplyTaille/1.15f;
			if (multiplyTaille<1f)
				multiplyTaille=1f;
			calculTaille();
		}
	}

	public void changeRacActive()
	{
		racTaillActive = (!racTaillActive);
	}

	public void calculTaille()
	{
		if (racTaillActive) {
						tailleEchelle = Mathf.Sqrt (tailleReelle) * multiplyTaille;
				} else {
						tailleEchelle = tailleReelle * multiplyTaille;
				}
		astreVu.transform.localScale = Vector3.one * tailleEchelle;
	}

	public void ajoutForce(Vector3 force)
	{
		astreReel.rigidbody.AddForce (force);
	}

	//Gets Sets
	public GameObject getAstreVu()
	{
		return astreVu;
	}

	public float getTailleEchelle()
	{
		return tailleEchelle;
	}

	public float getTailleReelle ()
	{
		return tailleReelle;
	}

	public void setTailleReelle (float t)
	{
		tailleReelle = t;
	}
	public void setAstre(Astre a)
	{
		astre = a;
	}
	public void setCameraOri(GameObject cameraOrigine)
	{
		cameraOri = cameraOrigine;

	}
}
