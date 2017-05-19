using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageView : MonoBehaviour{

	public int Width,Height;
	GameObject floorPrefab,wallPrefab,brokenWallPrefab;
	GameObject bombPrefab,explosionPrefab;
	GameObject bombItemPrefab,fireItemPrefab,heartItemPrefab,shoesItemPrefab;
	//GameObject playerPrefab;

	Dictionary<int,GameObject> objectDic;

	//List<GameObject> floorList,wallList,brokenWallList;

	GameObject stageParent;
	void Awake(){
		LoadPrefab();
	}
	void Start(){	
		
	}



//	public StageView(int width,int height){
//		this.Width = width;
//		this.Height = height;
//		LoadPrefab();
//	}

	void LoadPrefab ()
	{
		floorPrefab = Resources.Load<GameObject> ("Floor");
		wallPrefab = Resources.Load<GameObject> ("Wall");
		brokenWallPrefab = Resources.Load<GameObject> ("BrokenWall");
		bombPrefab = Resources.Load<GameObject> ("Bomb");
		stageParent = new GameObject ("Stage");
		explosionPrefab = Resources.Load<GameObject> ("Explosion");
		bombItemPrefab = Resources.Load<GameObject> ("Item/BombItem");
		fireItemPrefab = Resources.Load<GameObject> ("Item/FireItem");
		heartItemPrefab = Resources.Load<GameObject> ("Item/HeartItem");
		shoesItemPrefab = Resources.Load<GameObject> ("Item/ShoesItem");
	}

	public void InitiView (int[,] field)
	{
//		floorList = new List<GameObject> ();
//		wallList = new List<GameObject> ();
//		brokenWallList = new List<GameObject> ();
		objectDic = new Dictionary<int, GameObject>();

		if (Width != field.GetLength(0)) {
			return;
		}
		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Height; j++) {

				GameObject floorObj = Instantiate (floorPrefab);
				floorObj.transform.position = new Vector3 (i, 0, j);
				floorObj.transform.SetParent(stageParent.transform);

				//floorList.Add (floorObj);

				if (field [i, j] == Const.WALL) {
					GameObject wallObj = Instantiate (wallPrefab);
					wallObj.transform.position = new Vector3 (i, 0.5f, j);
					//wallList.Add (wallObj);
					objectDic.Add(PointToInt(new Point(i,j)),wallObj);
					wallObj.transform.SetParent(stageParent.transform);
				} else if (field [i, j] == Const.BLOKENWALL || field[i,j] == Const.BOMBITEMBLOCK||field [i, j] == Const.FIREITEMBLOCK || field[i,j] == Const.HEARTITEMBLOCK||field [i, j] == Const.SHOESITEMBLOCK) {
					GameObject wallObj = Instantiate (brokenWallPrefab);
					wallObj.transform.position = new Vector3 (i, 0.5f, j);
					objectDic.Add(PointToInt(new Point(i,j)),wallObj);
					wallObj.transform.SetParent(stageParent.transform);
					//brokenWallList.Add (wallObj);
				}
			}
		}
	}

	public void UpdateView (int[,] field)
	{
		if (Width != field.Length) {
			return;
		}
		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Height; j++) {

			}
		}
	}

	public void GenerateBomb (int x, int y,float timer,BombInfo info)
	{

		GameObject obj =  Instantiate(bombPrefab,new Vector3(x,0.5f,y),Quaternion.identity) as GameObject;
		objectDic.Add(PointToInt(new Point(x,y)), obj);
		Bomb bombScript =  obj.AddComponent<Bomb>();
		bombScript.info = info;
		Destroy(obj,timer);
	}
	public void GenerateExplosion (int x, int y,float timer)
	{
		Shake(Camera.main.gameObject);
		GameObject obj =  Instantiate(explosionPrefab,new Vector3(x,0.5f,y),Quaternion.identity) as GameObject;
		//objectDic.Add(PointToInt(new Point(x,y)), obj);
		Destroy(obj,timer);
	}

	public void DestroyBlock (int x, int y)
	{
		int index = PointToInt (new Point (x, y));
		if (objectDic.ContainsKey (index)) {
			GameObject obj = objectDic[index];
			objectDic.Remove(index);
			Destroy(obj);
		}
	}



	Point IntToPoint (int input)
	{
		int x = input%Width;
		int y = input/Width;
		return new Point(x,y);

	}

	int PointToInt (Point input)
	{
		return input.x + input.y*Width;
	}


 
    public void Shake(GameObject shakeObj){
        iTween.ShakePosition(shakeObj,iTween.Hash("x",0.3f,"y",0.3f,"time",0.5f));
    }

    public void GenerateItem (int x, int y, int item)
	{
		if (item == Const.BOMBITEM) {
			GameObject obj = Instantiate (bombItemPrefab, new Vector3 (x, 0.5f, y), Quaternion.identity) as GameObject;
		}
		else if (item == Const.FIREITEM) {
			GameObject obj = Instantiate (fireItemPrefab, new Vector3 (x, 0.5f, y), Quaternion.identity) as GameObject;
		}
		else if (item == Const.HEARTITEM) {
			GameObject obj = Instantiate (heartItemPrefab, new Vector3 (x, 0.5f, y), Quaternion.identity) as GameObject;
		}
		else if (item == Const.SHOESITEM) {
			GameObject obj = Instantiate (shoesItemPrefab, new Vector3 (x, 0.5f, y), Quaternion.identity) as GameObject;
		}

	}
 

}



