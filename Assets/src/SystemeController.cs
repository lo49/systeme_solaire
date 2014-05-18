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
	private float vitesseTemps = 2f; // Vitesse du temps initiale. Est aussi utilisé pour sauvegarder la vitesse du temps lors du mode pause.
	private Systeme systeme;
	private CameraController camC;
	private Astre astreOri; // astre suivi par la caméra
	//private Astre astreCliqué = null; //astre cliqué par la caméra (à faire)

	public void setSysteme(Systeme s)
	{
		systeme=s;
	}

	void Awake () {

	}

	void Start () {
		name = "SystemeController";
		astreOri = systeme.getEtoile ();
		Time.timeScale = vitesseTemps; //fixedDeltaTime est le temps virtuel qu'il se passe entre chaque frame. timeScale est le temps virtuel qu'il faut pour faire 1 seconde réelle. Je crois.
		Time.fixedDeltaTime = 0.02F * Time.timeScale;	//ici : fixedDeltaTime=0.2 timeScale=10 il y a donc 10/0.2 = 50 frames par seconde.
		//timeScale MAX = 100. fixedDeltaTime MIN = 0.0001
		camC = systeme.getCamera ().GetComponent<CameraController>(); // On accède aux fonctions de CameraController
		camC.setTailleAstreSuivi (astreOri.getTailleVu ());
	}
	
	
	void FixedUpdate ()	// FixedUpdate est appelée à chaque frame espacée d'un temps fixe, ce qui est nécessaire pour le calcul de force.
	{
		updateAllPos ();
		//showInfos ();
	}

	void Update () // Update est appelée à chaque frame variable, mais n'est pas affecté par timeScale ce qui permet de faire bouger la camera meme en mode pause.
	{
		camC.gererDeplacementCamera ();
		Vector3 posAstreOri = astreOri.getPositionVu();
		Vector3 pos = camC.getPosAstreSuivi();
		foreach (Astre a in systeme.getAstres())
		{
			AstreVuController astreVu=a.getAvc();
			a.getAvc().setPosAstreSuivi(pos);
			astreVu.calculPosEchelle(posAstreOri);	//Calcul de la position des astreVu.
		}
	}

	private void updateAllPos() //fonction appelée dans FixedUpdate
	{	// Cette fonction calcule la force de tous les astres sur tous les astres.

		// Variables qui seront utilisées pour calculer les forces de gravité.
		Vector3 distance; // distance entre les deux astres
		Vector3 force; // force entre les deux astres
		Astre astre2; // astre 2
		int b; // compteur boucle
		int pos = 0; // compteur boucle2
		
		foreach (Astre astre1 in systeme.getAstres())
		{
			pos++; 
			for(b=pos ; b<systeme.getAstres().Count ; b++)
			{
				astre2 = systeme.getAstre (b);	// Chargement du deuxième astre
				distance = astre1.getPosition() - astre2.getPosition ();	// Calcul de la distance entre les deux astres.
				force = 0.66742F*astre1.getMasse() * astre2.getMasse() * distance.normalized / distance.sqrMagnitude;
				//if (Time.frameCount>1){ // Ligne rajoutée pour éviter un bug bizarre.
				astre1.getAc().ajoutForce(-force);	// Ajout de la force sur chaque astre
				astre2.getAc().ajoutForce(force);
			}
		}
		
	}


	private Astre detecteAstre()	//Détection de l'astre lors d'un clic
	{
		RaycastHit hit; // RaycastHit permet de retourner des informations d'un Ray
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // On créé un rayon qui part de la position de la souris dans la direction de la caméra
		Debug.DrawRay(ray.origin, ray.direction*1000, Color.yellow); // juste pour vérifier que ça marche, dessine le rayon jaune. Non visible dans le jeu.
		if(Physics.Raycast(ray, out hit)) // Enregistre dans le RaycastHit hit ce qu'on a éventuellement touché
			if (hit.rigidbody==null)
				return hit.collider.GetComponent<AstreVuController>().getAstre();
		return null;
		
	}

	private void gererPause(){ // gère la pause (sisi je te jure)
		if(pause) // si la pause est activée
		{
			Time.timeScale = vitesseTemps; // on se rappelle du temps précédent
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
		else // si la pause est désactivée
		{
			vitesseTemps = Time.timeScale; // on enregistre la vitesse du temps
			Time.timeScale = 0f; // et on arrete le temps
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
		//Debug.Log ("Vitesse Terre : " + systeme.getAstre (3).getAstreReel().rigidbody.velocity.magnitude + " km/s");
	}

	
	public void OnGUI() //GUI gère les boutons et Events
	{
		Event e = Event.current;
		if (e.isMouse)
		{
			if (e.button==0) //si clic gauche
			{
				Astre a=detecteAstre(); 
			
				if (e.clickCount==2) // Si double click
				{
					if (a != null) // Si on a touché un collider
					{
						Vector3 ancPos = astreOri.getPositionVu();
						astreOri = a; // l'astre suivi vaut le nouvel astre qu'on a détecté
						camC.followAstre(true);
						camC.setTailleAstreSuivi (astreOri.getTailleVu());
						camC.deplaceCamera(ancPos-astreOri.getPositionVu());
					}
				}
			}
		}

		if (e.type == EventType.keyDown) // si touche appuyée
		{
			if (e.keyCode==KeyCode.Escape) // si touche appuyée est Echap
			{
				astreOri = systeme.getEtoile();
				camC.followAstre(false);
			}
			if (e.keyCode==KeyCode.Space)
			{
				gererPause();
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
			float ancTaille = astreOri.getTailleVu();
			foreach (Astre a in systeme.getAstres())
			{
				a.getAvc().gererEchelle(1);
				a.getAvc().gererEchelle(4);
			}
			camC.changeTaille(ancTaille, astreOri.getTailleVu());
		}

		if (GUI.Button (new Rect (10, 160, 250, 50), "augmenter échelle taille"))
		{
			foreach (Astre a in systeme.getAstres())
			{
				a.getAvc().gererEchelle(5);
			}
			camC.setTailleAstreSuivi (astreOri.getTailleVu());
		}
		if (GUI.Button (new Rect (10, 210, 250, 50), "diminuer échelle taille"))
		{
			foreach (Astre a in systeme.getAstres())
			{
				a.getAvc().gererEchelle(6);
			}
			camC.setTailleAstreSuivi (astreOri.getTailleVu());
		}
	}


}

