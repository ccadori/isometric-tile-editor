using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Land : MonoBehaviour {
	
	//Load array from XML file.-------------------------------------------------------------------
	public static List<Tile> LoadLand(string name){
		//Init serializer component to Tile array.
		XmlSerializer serializer = new XmlSerializer (typeof(List<Tile>));
		List<Tile> land;
		//Create connection with a selected file.
		try {
			FileStream stream;
			if (Application.isEditor)
				stream = new FileStream ("Assets/DataBase/" + name + ".xml", FileMode.Open);
			else 
				stream = new FileStream (Application.dataPath + "/" + name + ".xml", FileMode.Open);
			//Serialize a array on XML.
			land = serializer.Deserialize (stream) as List<Tile>;
			//Close connection.
			stream.Close ();
		} catch {
			return null;
		}
		return land;
	}

	//Generate new land.-------------------------------------------------------------------------
	public static List<Tile> Generateland(int type){
		List<Tile> land = new List<Tile>();
		land.Add( new Tile(){ x = 0, y = 0, z = 0, type = type } );
		return land;
	}

	//Save array on XML file.---------------------------------------------------------------------
	public static bool SaveLand(string name, List<Tile> land){
		//Init serializer component to Tile array.
		XmlSerializer serializer = new XmlSerializer (typeof(List<Tile>));
		//Create connection with a selected file.
		try{
			FileStream stream;
			if (Application.isEditor)
				stream = new FileStream ("Assets/DataBase/" + name + ".xml", FileMode.Create);
			else 
				stream = new FileStream (Application.dataPath + "/" + name + ".xml", FileMode.Create);
			//Deserialize Tile array from XML.
			serializer.Serialize (stream, land);
			//Close connection.
			stream.Close ();
		} catch {
			return false;
		}
		return true;
	}

	public static void InstantiateTile(Tile tile, GameObject[] tileTypes, Material startMaterial){
		float angular = 0f;
		int layer = 0;
		if (tile.x % 2 == 0){
			angular = 0.38f; 
		} else {
			layer = 1; 
		}
		//Add colunn to layer
		layer += 2 * tile.y;

		//Instantiate current Tile.
		GameObject currentTile = Instantiate (
			//Object
			tileTypes[tile.type], 
			//Object Position.
			new Vector3 (tile.x * 0.65f, (tile.y * 0.75f - angular) - tile.z * 0.38f, layer + tile.z ), 
			//Object Rotation.
			Quaternion.Euler(0,0,0)
			) as GameObject;

		//Define layer of this Tile.
		currentTile.GetComponent<SpriteRenderer>().sortingOrder = -layer - tile.z;
		//Set stats to TileController.
		currentTile.GetComponent<TileControlller>().SetStats(tile);
		//Select start tile.
		if (currentTile.GetComponent<TileControlller>().thisTile.start == 1)
			currentTile.GetComponent<Renderer>().material = startMaterial;
		//Set name of Tile to lina, collun and depth.
		currentTile.name = "" + tile.x + tile.y + tile.z;
		//Set parent to land container.
		currentTile.transform.parent = GameObject.FindGameObjectWithTag("Land").transform;
	}

	//Instantiate array of Tile in scene.---------------------------------------------------------
	public static void InstanceLand(List<Tile> land, GameObject[] tileTypes, Material startMaterial){
		//Create GameObject container to land.
		GameObject landContainer = new GameObject ();
		//Setting name and tag to this object.
		landContainer.name = "Current Land";
		landContainer.tag = "Land";

		//0.65 | 0.75 | 0.38
		foreach (Tile temp in land){
			//Init layer controller and angular difference for pair colunns
			float angular = 0f;
			int layer = 0;
			if (temp == null)
				continue;

			//Set layer and angular differences.
			if (temp.x % 2 == 0){
				angular = 0.38f; 
			} else {
				layer = 1; 
			}

			//Add colunn to layer
			layer += 2 * temp.y;

			//Instantiate current Tile.
			GameObject currentTile = Instantiate (
				//Object
				tileTypes[temp.type], 
				//Object Position.
				new Vector3 (temp.x * 0.65f, (temp.y * 0.75f - angular) - temp.z * 0.38f, layer + temp.z ), 
				//Object Rotation.
				Quaternion.Euler(0,0,0)
			) as GameObject;

			//Define layer of this Tile.
			currentTile.GetComponent<SpriteRenderer>().sortingOrder = -layer - temp.z;
			//Set stats to TileController.
			currentTile.GetComponent<TileControlller>().SetStats(temp);
			//Select start tile.
			if (currentTile.GetComponent<TileControlller>().thisTile.start == 1)
				currentTile.GetComponent<Renderer>().material = startMaterial;
			//Set name of Tile to lina, collun and depth.
			currentTile.name = "" + temp.x + temp.y + temp.z;
			//Set parent to land container.
			currentTile.transform.parent = landContainer.transform;
		}
	}

	//Check Tile height
	public static bool CheckTileHeight(Tile currentTile, int size){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		for (int i = 0; i > -size; i-- ){
			string name = "" + currentTile.x + currentTile.y + (currentTile.z + (i - 1)).ToString();
			Debug.Log(name);
			foreach (GameObject tile in tiles){
				if (tile.name == name)
					return false;
			}
		}
		return true;
	}
}