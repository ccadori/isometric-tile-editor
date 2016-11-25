using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditorController : MonoBehaviour {
	
	public static EditorController editorController;

	public float velocity;
	public Vector3 landSize;

	public int tileTypeSelected;
	public int lastLandPosition;
	public Vector2 tileSelected;
	public int tilesQuantity;
	public int maxTilesQuantity;

	public GameObject mainCamera;
	public GameObject[] tileTypesButton;
	public GameObject[] tileTypes;

	public Material startMaterial;

	public int editMode;

	void Awake() {
		editorController = this;
	}

	public void ClearLand(){
		Destroy(GameObject.FindGameObjectWithTag("Land"));
		lastLandPosition = 0;
	}

	public void LoadLand(string name){
		List<Tile> land;
		land = Land.LoadLand(name);
		if (land != null){
			UIEditorController.uiController.ActiveInstantAlert("Mapa carregado com sucesso.");
			ClearLand();
			Land.InstanceLand(land, tileTypes, startMaterial);
		} else { 
			UIEditorController.uiController.ActiveInstantAlert("Nao foi encontrado mapa com este nome.");
		}
	}

	public void SaveLand(string name){
		GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("Tile");
		if (tileObjects.Length > 1){
			List<Tile> land = new List<Tile>();
			foreach (GameObject temp in tileObjects){
				land.Add(temp.GetComponent<TileControlller>().thisTile);
			}
			if (Land.SaveLand(name, land))
				UIEditorController.uiController.ActiveInstantAlert("Terreno salvo com sucesso.");	
			else 
				UIEditorController.uiController.ActiveInstantAlert("Erro ao salvar.");

		} else {
			UIEditorController.uiController.ActiveInstantAlert("Nao ha terreno para ser salvo.");
		}
	}

	public void NewLand(int size){
		ClearLand();
		List<Tile> land = Land.Generateland(0);
		Land.InstanceLand(land,tileTypes,startMaterial);
		AddTileQuantity(1);
	}

	public void Move (Vector3 direction){
		mainCamera.transform.position += direction * velocity;
	}

	public void SetSelectedTileType(int selected){
		tileTypeSelected = selected;
	}

	public int ChangeEditMode(){
		if (editMode == 0){
			editMode++;
		} else if (editMode == 1){
			editMode++;
		} else if (editMode == 2){
			editMode++;
		} else if (editMode == 3){
			editMode = 0;
		}
		return editMode;
	}

	public bool AddTileQuantity(int qt){
		if (qt > 0)
			if (maxTilesQuantity <= tilesQuantity)
				return false;
		tilesQuantity += qt;
		return true;
	}

	public void AddTile(GameObject tileObject, int face){
		Tile tile = tileObject.GetComponent<TileControlller>().thisTile;
		if (editMode == 0) {

			if (AddTileQuantity(1)){
				int y = 0;
				if (tile.x % 2 == 0)
					y = 1;
				if (face == 0){
					Land.InstantiateTile(new Tile(){x = tile.x, y = tile.y, z = tile.z-1, type = tileTypeSelected }, tileTypes, startMaterial);
				} else if (face == 1){
					Land.InstantiateTile(new Tile(){x = tile.x-1, y = tile.y-y, z = tile.z, type = tileTypeSelected }, tileTypes, startMaterial);
				} else if (face == 2){
					Land.InstantiateTile(new Tile(){x = tile.x+1, y = tile.y-y, z = tile.z, type = tileTypeSelected }, tileTypes, startMaterial);
				}
			}
		} else if (editMode == 1){

			Land.InstantiateTile(new Tile(){x = tile.x, y = tile.y, z = tile.z, type = tileTypeSelected }, tileTypes, startMaterial);
			Destroy(tileObject);
		} else if (editMode == 2){

			Destroy(tileObject);
		} else if (editMode == 3){

			Tile thisTile = tileObject.GetComponent<TileControlller>().thisTile;
			if (thisTile.start == 0){
				if (Land.CheckTileHeight(thisTile, 2)){
					thisTile.start = 1;
					Land.InstantiateTile(thisTile, tileTypes, startMaterial);
					Destroy(tileObject);
				} else {
					UIEditorController.uiController.ActiveInstantAlert("Existem blocos acima deste.");
				}
			} else {
				thisTile.start = 0;
				Land.InstantiateTile(thisTile, tileTypes, startMaterial);
				Destroy(tileObject);
			}
		}
	}
}