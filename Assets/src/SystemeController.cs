/// <summary>
/// SystemeController.cs
/// CONTROLER
/// créé par nicolas le 26/03 (fichier gravity.cs)
/// modifié par jonathan le 31/03
/// modifié par ...
/// </summary>

using UnityEngine;
using System.Collections;

public class SystemeController : MonoBehaviour
{
	private bool pause=false;
	private float vitesseTemps;
	private Systeme systeme;
	private CameraController camC;
	private Astre astreSuivi=null;

	public void setSysteme(Systeme s)
	{
		systeme=s;
	}

	void Start () {
		name = "SystemeController";
		Time.timeScale = 2F;		//fixedDeltaTime est le temps virtuel qu'il se passe entre chaque frame. timeScale est le temps virtuel qu'il faut pour faire 1 seconde réelle. Je crois.
		Time.fixedDeltaTime = 0.02F * Time.timeScale;	//ici : fixedDeltaTime=0.2 timeScale=10 il y a donc 10/0.2 = 50 frames par seconde.
		//timeScale MAX = 100. fixedDeltaTime MIN = 0.0001
		camC = systeme.getCamera ().GetComponent<CameraController>();
	}
	
	
	void FixedUpdate ()	// FixedUpdate est appelée à chaque frame.
	{
		updateAllPos ();
	}

	void Update ()
	{
		showInfos ();
	}

	void LateUpdate () // LateUpdate est appelée après chaque frame.
	{ //Utile par exemple pour une caméra qui doit suivre la position d'un objet, après qu'elle ait été calculée dans FixedUpdate

		foreach (Astre a in systeme.getAstres())
		{
			a.getAc().calculPosEchelle();	//Calcul de la position à l'échelle de tous les astres.
		}
	}
	
	private void updateAllPos() //fonction appelée dans FixedUpdate
	{	// Cette fonction calcule la force de tous les astres sur tous les astres.
		// Variables qui seront utilisées pour calculer les forces de gravité.
		Vector3 distance; // distance entre les deux astres
		Vector3 force; // force entre les deux astres
		GameObject astre1; // astre 1
		GameObject astre2; // astre 2
		int b; // compteur boucle
		int pos = 0; // compteur boucle2
		
		foreach (Astre a in systeme.getAstres())
		{
			astre1 = a.getAstreReel();		// Chargement du premier astre
			pos++; 
			for(b=pos ; b<systeme.getAstres().Count ; b++)
			{
				astre2 = systeme.getAstre (b).getAstreReel();	// Chargement du deuxième astre
				distance = astre1.transform.position - astre2.transform.position;		// Calcul de la distance entre les deux astres.
				force = 0.66742F*astre1.rigidbody.mass * astre2.rigidbody.mass * distance.normalized / distance.sqrMagnitude;
				if (Time.frameCount>1){ // Ligne rajoutée pour éviter un bug bizarre.
				astre1.GetComponent<AstreController>().ajoutForce(-force);	// Doit-on mettre ces lignes dans AstreController? Ben voilà.
				astre2.GetComponent<AstreController>().ajoutForce(force);
				}
			}
		}
	}


	private void followAstre(){	//Gestion de l'astre suivi par la caméra
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction*1000, Color.yellow);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider != null)
			{
				if (astreSuivi != null)
					astreSuivi.getAc().setCameraOri(null);
				astreSuivi = hit.collider.GetComponent<GetAstre>().getAstre();
				astreSuivi.getAc().setCameraOri(camC.getCameraOri());
				camC.setCamSuivActive(true);
			}
		}
	}

	private void stopFollowAstre()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			if (astreSuivi != null)
				astreSuivi.getAc().setCameraOri(null);
			astreSuivi = null;
			camC.setCamSuivActive(false);
		}
	}

	private void gererPause(){
		if(pause)
		{
			Time.timeScale = vitesseTemps;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
		else
		{
			vitesseTemps = Time.timeScale;
			Time.timeScale = 0f;
		}
		
		pause = !pause;
	}


	private void showInfos()	//Fonction appelée dans FixedUpdate
	{	
		if (Time.timeScale < 0.06F) {
			Debug.Log ("Vitesse du temps : " + (Time.timeScale * 1666.666F) + " minutes/sec");
		} else if (Time.timeScale < 0.864F) {
			Debug.Log ("Vitesse du temps : " + (Time.timeScale * 27.77777F) + " heures/sec");
		} else if (Time.timeScale < 26.29744F) {
			Debug.Log ("Vitesse du temps : " + (Time.timeScale * 1.157407F) + " jours/sec");
		} else {
			Debug.Log ("Vitesse du temps : " + (Time.timeScale * 0.03802651F) + " mois/sec");
		}
		Debug.Log ("Jour numéro " + (Time.fixedTime*1.157407F));
		Debug.Log ("Vitesse Terre : " + systeme.getAstre (3).getAstreReel().rigidbody.velocity.magnitude + " km/s");
		Debug.Log (Input.GetAxis ("Mouse ScrollWheel"));
	}

	
	public void OnGUI() //GUI gère les boutons et Events
	{
		Event e = Event.current;
		if (e.isMouse)
		{
			if (e.clickCount==2) // if double click
				followAstre();  // Fonction qui gère le suivi des astres

			if (e.button==0){ //left clic
				camC.rotateZ();
			}

			if (e.button==1) //right clic
			{
				camC.rotateXYCamera();
			}
			if (e.button==2) // middle clic
			{
				camC.deplacerCamera();
			}
		}

		if (e.type == EventType.scrollWheel) {
				camC.avanceCamera ();
		}

		if (e.type == EventType.keyDown) {

			if (e.keyCode==KeyCode.Escape)
			{
				stopFollowAstre();
			}
			if (e.keyCode==KeyCode.Space)
			{
				gererPause();	// gère la pause(c'est vrai? lol)
			}

		}


		if (GUI.Button (new Rect (10, 10, 120, 50), "Accélerer temps")) { // à mettre en raccourci clavier?
			if(Time.timeScale*1.15F>100f){
				Time.timeScale=100f;
			}else{
				Time.timeScale = Time.timeScale*1.15F;
			}
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
		if (GUI.Button (new Rect (10, 60, 120, 50), "Ralentir temps")) {
			if (Time.timeScale/1.15f<0.005f)
			{
				Time.timeScale = 0.005f;
			}else{
				Time.timeScale = Time.timeScale/1.15F;
			}
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
		
		if (GUI.Button (new Rect (10, 110, 300, 50), "activer/désactiver échelle racine"))
		{
			foreach (Astre a in systeme.getAstres())
			{
				a.getAc().gererEchelle(1);
				a.getAc().gererEchelle(4);
			}
		}
		if (GUI.Button (new Rect (10, 160, 250, 50), "augmenter échelle taille"))
		{
			foreach (Astre a in systeme.getAstres())
			{
				a.getAc().gererEchelle(5);
			}
		}
		if (GUI.Button (new Rect (10, 210, 250, 50), "diminuer échelle taille"))
		{
			foreach (Astre a in systeme.getAstres())
			{
				a.getAc().gererEchelle(6);
			}
		}
	}


}

