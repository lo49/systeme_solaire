/// <summary>
/// SystemeControler.cs
/// CONTROLER
/// créé par nicolas le 26/03 (fichier gravity.cs)
/// modifié par jonathan le 31/03
/// modifié par ...
/// </summary>

using UnityEngine;
using System.Collections;

public class SystemeControler : MonoBehaviour
{
	public Systeme systeme;

	public void setSysteme(Systeme s)
	{
		systeme=s;
	}


	void Start () {
	Time.timeScale = 10F;
	Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}

	
	void FixedUpdate ()
	{
		updateAllPos ();
		if (Time.timeScale < 0.06F) {
						Debug.Log ("Vitesse du temps : " + (Time.timeScale * 1666.666F) + "minutes/sec");
				} else if (Time.timeScale < 0.864F) {
						Debug.Log ("Vitesse du temps : " + (Time.timeScale * 27.77777F) + "heures/sec");
				} else if (Time.timeScale < 26.29744F) {
						Debug.Log ("Vitesse du temps : " + (Time.timeScale * 1.157407F) + "jours/sec");
				} else {
						Debug.Log ("Vitesse du temps : " + (Time.timeScale * 0.03802651F) + "mois/sec");
				}
		Debug.Log ("Jour numéro " + (Time.fixedTime*1.157407F));
		Debug.Log ("Vitesse Terre : " + systeme.astres [1].gameobject.rigidbody.velocity.magnitude + " km/s");
	}
	void OnGUI() {
		if (GUI.Button (new Rect (10, 10, 130, 50), "Accélerer temps")) {
			Time.timeScale = Time.timeScale*1.15F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
		if (GUI.Button (new Rect (10, 80, 130, 50), "Ralentir temps")) {
			Time.timeScale = Time.timeScale/1.15F;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
	}
	
	public void updateAllPos()
	{
			// Variables qui seront utilisées pour calculer les forces de gravité.
			Vector3 distance; // distance entre les deux astres
			Vector3 force; // force entre les deux astres
			GameObject temp1; // astre 1
			GameObject temp2; // astre 2
			int b = 0; // compteur boucle
			int pos = 0; // compteur boucle2
			
				foreach (Astre a in systeme.astres)
				{
					//Debug.Log("boucle appelée");
					temp1 = a.gameobject;							// temp 1 permet de gagner du temps vu que le a est utilisé plusieurs fois.
					pos++; 
					for(b=pos ; b<systeme.astres.Count ; b++)
					{
						temp2 = systeme.astres[b].gameobject;											// temp 2 : pareil que temp 1.
						distance = temp1.transform.position - temp2.transform.position;		// calcul de la distance entre les deux astres.
						force = 0.66742F*temp1.rigidbody.mass * temp2.rigidbody.mass * distance.normalized / distance.sqrMagnitude;
						temp1.rigidbody.AddForce(-force);
						temp2.rigidbody.AddForce(force); // Force de gravité de A/B = -Force de gravité de B/A
						//Debug.Log ("force :"+force);
					}
				}
	}
}
