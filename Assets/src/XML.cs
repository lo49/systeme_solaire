/// <summary>
/// XML.cs
/// MODELE
/// créé par jonathan le 19/04
/// modifié par ...
/// </summary>

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public static class XML
{
	/*
	 ******** PROTOTYPE DES FONCTIONS DISPONIBLES POUR XML :
	 *
	 **** PROTOTYPE DES ACCESSEURS POUR XML
	 * 
	 * string getValeurRang2(string fichier, string rang1, string rang2);
	 * string getValeurPlanete(string fichier, string planete, string rang2);
	 * List<string> getListPlanetes(string fichier);
	 * List<string> getListAstres(string fichier);
	 * string getNomSysteme(string fichier);
	 * string getImageSysteme(string fichier);
	 * string getDescriptionSysteme(string fichier);
	 * string getNomEtoile(string fichier);
	 * float getMasseEtoile(string fichier);
	 * float getTailleEtoile(string fichier);
	 * string getTextureEtoile(string fichier);
	 * string getCategorieEtoile(string fichier);
	 * float getTemperatureEtoile(string fichier);
	 * float getLuminositeEtoile(string fichier);
	 * string getDescriptionEtoile(string fichier);
	 * string getNom(string fichier, string planete);
	 * float getMasse(string fichier, string planete);
	 * float getTaille(string fichier, string planete);
	 * float getPosX(string fichier, string planete);
	 * float getPosY(string fichier, string planete);
	 * float getPosZ(string fichier, string planete);
	 * string getTexture(string fichier, string planete);
	 * string getCategorie(string fichier, string planete);
	 * string getComposition(string fichier, string planete);
	 * string getDescription(string fichier, string planete);
	 * List <string> getFiles();
	 * void recupererSysteme(string fichier);
	 * bool sauverSysteme(Systeme s);
	*/

	// Retourne une valeur (string) de rang 2 (c-a-d <systeme><rang1><rang2>x)
	public static string getValeurRang2(string fichier, string rang1, string rang2)
	{
		string chemin = "Assets/src/Donnees/" + fichier;
		System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(chemin);
		string valeur = "";
		bool rang1Ouvert = false;
		bool rang2Ouvert = false;
		while (reader.Read()) 
		{
			reader.MoveToContent();
			if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name==rang1)
			{
				rang1Ouvert = true;
			}
			if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name==rang2 && rang1Ouvert == true)
			{
				rang2Ouvert = true;
			}
			if (rang1Ouvert == true && rang2Ouvert == true && reader.NodeType == System.Xml.XmlNodeType.Text)
			{
				valeur = reader.Value;
				rang2Ouvert = false;
				rang1Ouvert = false;
			}
		}
		return valeur;
	}


	// Retourne une valeur (string) d'une planete (c-a-d <systeme><planete><rang2>x)
	public static string getValeurPlanete(string fichier, string planete, string rang2)
	{
		string chemin = "Assets/src/Donnees/" + fichier;
		System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(chemin);
		string valeur = "";
		bool planeteOuvert = false;
		bool nomOuvert = false;
		bool nomOK = false;
		bool rang2Ouvert = false;
		while (reader.Read()) 
		{
			reader.MoveToContent();
			if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name=="planete")
			{
				planeteOuvert = true;
			}
			if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name=="nom" && planeteOuvert == true)
			{
				nomOuvert = true;
			}
			if(nomOuvert == true && planeteOuvert == true && reader.NodeType == System.Xml.XmlNodeType.Text && reader.Value == planete)
			{
				nomOK = true;
			}
			if (nomOK == true && reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name == rang2)
			{
				rang2Ouvert = true;
			}
			if (rang2Ouvert == true && reader.NodeType == System.Xml.XmlNodeType.Text)
			{
				valeur = reader.Value;
				rang2Ouvert = false;
				nomOK = false;
				nomOuvert = false;
				planeteOuvert = false;
			}
		}
		return valeur;
	}


	//Retourne un vecteur contenant les noms de toutes les planètes
	public static List<string> getListPlanetes(string fichier)
	{
		// attention, ne renvoie pas l'étoile, pour cela il faut utiliser getListAstres(string)
		List<string> planetes = new List<string>();
		string chemin = "Assets/src/Donnees/" + fichier;
		System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(chemin);
		bool planeteOuvert = false;
		bool nomOuvert = false;
		while (reader.Read()) 
		{
			reader.MoveToContent();
			if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name=="planete")
			{
				planeteOuvert = true;
			}
			if (reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name=="nom" && planeteOuvert == true)
			{
				nomOuvert = true;
			}
			if(nomOuvert==true && reader.NodeType == System.Xml.XmlNodeType.Text)
			{
				planetes.Add (reader.Value);
				nomOuvert=false;
				planeteOuvert=false;
			}
		}
		return planetes;
	}


	//Retourne un vecteur contenant les noms de tous les astres
	public static List<string> getListAstres(string fichier)
	{
		List<string> planetes = getListPlanetes(fichier);
		planetes.Insert (0, getNomEtoile (fichier));
		return planetes;
	}


	// Retourne le nom du systeme décrit dans le fichier
	public static string getNomSysteme(string fichier)
	{
		return getValeurRang2(fichier,"general","nom");
	}


	// Retourne l'url de l'image du systeme décrit dans le fichier
	public static string getImageSysteme(string fichier)
	{
		return getValeurRang2(fichier,"general","URLimage");
	}


	// Retourne la descrition du systeme décrit dans le fichier
	public static string getDescriptionSysteme(string fichier)
	{
		return getValeurRang2(fichier,"general","description");
	}


	// Retourne le nom de l'étoile du systeme décrit dans le fichier
	public static string getNomEtoile(string fichier)
	{
		return getValeurRang2(fichier,"etoile","nom");
	}


	// Retourne la masse de l'étoile du systeme décrit dans le fichier
	public static float getMasseEtoile(string fichier)
	{
		return Convert.ToSingle(getValeurRang2(fichier,"etoile","masse"));
	}


	// Retourne la taille de l'étoile du systeme décrit dans le fichier
	public static float getTailleEtoile(string fichier)
	{
		return Convert.ToSingle(getValeurRang2(fichier,"etoile","taille"));
	}


	// Retourne la texture de l'étoile du systeme décrit dans le fichier
	public static string getTextureEtoile(string fichier)
	{
		return getValeurRang2(fichier,"etoile","texture");
	}


	// Retourne la catégorie de l'étoile du systeme décrit dans le fichier
	public static string getCategorieEtoile(string fichier)
	{
		return getValeurRang2(fichier,"etoile","categorie");
	}


	// Retourne la température de l'étoile du systeme décrit dans le fichier
	public static float getTemperatureEtoile(string fichier)
	{
		return Convert.ToSingle(getValeurRang2(fichier,"etoile","temperature"));
	}


	// Retourne la luminosité de l'étoile du systeme décrit dans le fichier
	public static float getLuminositeEtoile(string fichier)
	{
		return Convert.ToSingle(getValeurRang2(fichier,"etoile","luminosite"));
	}


	// Retourne la description de l'étoile du systeme décrit dans le fichier
	public static string getDescriptionEtoile(string fichier)
	{
		return getValeurRang2(fichier,"etoile","description");
	}


	// Retourne le nom de la planete
	public static string getNom(string fichier, string planete)
	{
		// Impossible de retourner le nom de la planete en utilisant getValeurPlanete
		// car cette fonction utilise le nom pour vérifier que la planète est la bonne.
		// La fonction getNom ne sert donc pas à grand chose ...
		return planete;
	}


	// Retourne la masse de la planete
	public static float getMasse(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "masse"));
	}


	// Retourne la taille de la planete
	public static float getTaille(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "taille"));
	}


	// Retourne la positionX de la planete
	public static float getPosX(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "positionX"));
	}


	// Retourne la positionY de la planete
	public static float getPosY(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "positionY"));
	}


	// Retourne la positionZ de la planete
	public static float getPosZ(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "positionZ"));
	}


	// Retourne la vitesseIniX de la planete
	public static float getVitX(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "vitesseIniX"));
	}


	// Retourne la vitesseIniY de la planete
	public static float getVitY(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "vitesseIniY"));
	}


	// Retourne la vitesseIniZ de la planete
	public static float getVitZ(string fichier, string planete)
	{
		return Convert.ToSingle(getValeurPlanete(fichier, planete, "vitesseIniZ"));
	}


	// Retourne la texture de la planete
	public static string getTexture(string fichier, string planete)
	{
		return getValeurPlanete(fichier, planete, "texture");
	}


	// Retourne la categorie de la planete
	public static string getCategorie(string fichier, string planete)
	{
		return getValeurPlanete(fichier, planete, "categorie");
	}


	// Retourne la composition de la planete
	public static string getComposition(string fichier, string planete)
	{
		return getValeurPlanete(fichier, planete, "composition");
	}


	// Retourne la description de la planete
	public static string getDescription(string fichier, string planete)
	{
		return getValeurPlanete(fichier, planete, "description");
	}


	//Retourne la liste de tous les fichiers XML contenus dans Assets/src/Donnees/
	public static List<string> getFiles()
	{
		string[] files;
		List<string> filesXML = new List<string>();
		files = Directory.GetFiles("Assets/src/Donnees/");
		foreach(string f in files)
		{
			if(Path.GetExtension(f)==".xml")
			{
				filesXML.Add(f);
			}
		}
		return filesXML;
	}


	// créé le systeme décrit dans le fichier XML
	public static Systeme recupererSysteme(string fichier)
	{
		Systeme s1 = new Systeme(getNomSysteme(fichier),getImageSysteme(fichier),getDescriptionSysteme(fichier));
		Astre etoile = new Etoile (getNomEtoile (fichier), s1, new Vector3 (0f, 0f, 0f), new Vector3 (0f, 0f, 0f), getMasseEtoile (fichier), getTailleEtoile (fichier),getTextureEtoile(fichier));
		foreach(string planete in getListPlanetes(fichier))
		{
			Planete p = new Planete(getNom(fichier, planete),s1,new Vector3(getPosX(fichier,planete),getPosY(fichier,planete),getPosZ(fichier,planete)), new Vector3(getVitX(fichier,planete),getVitY(fichier,planete),getVitZ(fichier,planete)),getMasse(fichier,planete),getTaille(fichier,planete),getTexture(fichier,planete));
		}
		return s1;
	}            


	// enregistrer sous le systeme dans un fichier XML
	public static bool sauverSystemeSous(Systeme s, string file)
	{
		// CREATION DU FICHIER XML
		bool OK = false;
		string fichier = "Assets/src/Donnees/" + file + ".xml";
		s.setFichierXML (fichier); // le systeme connait son fichier .xml
		bool cond = getFiles ().Contains (fichier);
		// si un fichier du meme nom n'éxiste pas déjà
		if(!cond)
		{
			FileStream fs = File.Create(fichier);
			OK = true; // fichier créé
			fs.Close();
		}
		// ECRITURE DANS LE FICHIER
		if(OK)
		{
			XmlTextWriter myXmlTextWriter = new XmlTextWriter (fichier, null);
			myXmlTextWriter.Formatting = Formatting.Indented;
			myXmlTextWriter.WriteStartDocument(false);
			myXmlTextWriter.WriteDocType("DTD", null, "systeme.dtd", null);
			
			myXmlTextWriter.WriteStartElement("systeme");

				myXmlTextWriter.WriteComment("caractéristiques générale du système");
				myXmlTextWriter.WriteStartElement("general");
					myXmlTextWriter.WriteElementString("nom", null,s.getNom());
					myXmlTextWriter.WriteElementString("URLimage", null,s.getURLimage());
					myXmlTextWriter.WriteElementString("description", null,s.getDescription());
				myXmlTextWriter.WriteEndElement();

				myXmlTextWriter.WriteStartElement("etoile");
					myXmlTextWriter.WriteElementString("nom", null,s.getEtoile().getNom()); 
					myXmlTextWriter.WriteElementString("masse", null,Convert.ToString(s.getEtoile().getMasseInit()));
					myXmlTextWriter.WriteElementString("taille", null,Convert.ToString(s.getEtoile().getTailleInit()));
					myXmlTextWriter.WriteElementString("texture", null,s.getEtoile().getTexture());
					myXmlTextWriter.WriteElementString("categorie", null,s.getEtoile().getCategorie());
					myXmlTextWriter.WriteElementString("temperature", null,Convert.ToString(s.getEtoile().getTemperature()));
					myXmlTextWriter.WriteElementString("luminosite", null,Convert.ToString(s.getEtoile().getLuminosite()));
					myXmlTextWriter.WriteElementString("description", null,s.getEtoile().getDescription());
				myXmlTextWriter.WriteEndElement();


			foreach(Planete planete in s.getPlanetes ())
			{
				Debug.Log("FOREACH OUVERT");
				myXmlTextWriter.WriteStartElement("planete");
					myXmlTextWriter.WriteElementString("nom", null,planete.getNom()); 
					myXmlTextWriter.WriteElementString("masse", null,Convert.ToString(planete.getMasseInit()));
					myXmlTextWriter.WriteElementString("taille", null,Convert.ToString(planete.getTailleInit()));
					// positions et vitesses INITIALES!
					myXmlTextWriter.WriteElementString("positionX", null,Convert.ToString(planete.getPositionInit().x));
					myXmlTextWriter.WriteElementString("positionY", null,Convert.ToString(planete.getPositionInit().y));
					myXmlTextWriter.WriteElementString("positionZ", null,Convert.ToString(planete.getPositionInit().z));
					myXmlTextWriter.WriteElementString("vitesseIniX", null,Convert.ToString(planete.getVitesseInit().x));
					myXmlTextWriter.WriteElementString("vitesseIniY", null,Convert.ToString(planete.getVitesseInit().y));
					myXmlTextWriter.WriteElementString("vitesseIniZ", null,Convert.ToString(planete.getVitesseInit().z));
					myXmlTextWriter.WriteElementString("texture", null,planete.getTexture());
					myXmlTextWriter.WriteElementString("categorie", null,planete.getCategorie());
					myXmlTextWriter.WriteElementString("composition", null,planete.getComposition());
					myXmlTextWriter.WriteElementString("description", null,planete.getDescription());
				myXmlTextWriter.WriteEndElement();
			}
				
			myXmlTextWriter.Flush();
			myXmlTextWriter.WriteEndElement();

			myXmlTextWriter.Flush();
			myXmlTextWriter.Close();
			Console.ReadLine();
		}
		return OK;
	}

	
	// enregistrer le systeme
	public static bool sauverSysteme(Systeme s)
	{
		bool OK = false;
		File.Delete (s.getFichierXML ()); // on supprime l'ancien fichier
		OK = sauverSystemeSous (s, s.getFichierXML ());
		return OK;
	}


}