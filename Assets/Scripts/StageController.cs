using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StageController : SingletonMonoBehaviour<StageController> {

	public float destroyTimer;


	void Awake ()
	{

	}

	Stage model;
	StageView view;

	// Use this for initialization
	void Start () {
		destroyTimer = Const.DESTROY_EXPLOSION_TIMER;
		model = new Stage(Const.WIDTH,Const.HEIGHT);
		view = GetComponent<StageView>();
		view.Width = Const.WIDTH;
		view.Height = Const.HEIGHT;
		view.InitiView(model.GetField());

	}



	public bool CanPlayerMove (int x, int y){
		return model.CanPlayerMove (x, y);
	}

	public bool CanPutBomb (int x, int y){
		return model.CanPutBomb (x, y);
	}

	public void PutBomb (int x, int y,BombInfo info)
	{
		Debug.Log("PutBomb");
		if (CanPutBomb (x, y)) {
			model.PutBomb(x,y);
			view.GenerateBomb(x,y,info.timer,info);
			StartCoroutine(ExplosionBomb(x,y,info.range,info.timer));
		}
	}

	IEnumerator ExplosionBomb (int x, int y, int range,float timer)
	{
		Debug.Log("ExplosionBomb");
		yield return new WaitForSeconds(timer);
		model.ExplosionBomb(x,y,range);
		AudioManager.Instance.PlaySE(Audio.EXPLOSION);
	}

	IEnumerator DestroyExplosion (int x, int y, float timer, int item = Const.FLOOR)
	{
		yield return new WaitForSeconds (timer);
		model.SetField (x, y, item);
		if (item != Const.FLOOR) {
			GenerateItem(x,y,item);
		}
	}


	IEnumerator DestroyExplosion (int x, int y, float timer)
	{
		Debug.Log("DestroyExplosion");
		yield return new WaitForSeconds (timer);
		model.SetField (x, y, Const.FLOOR);
//		if (item != Const.FLOOR) {
//			GenerateItem(x,y,item);
//		}
	}

	public void GenerateExplosion(int x,int y,int item = Const.FLOOR){
		view.GenerateExplosion(x,y,destroyTimer);
		view.DestroyBlock(x,y);
		GameManager.Instance.ExplosionCheck(x,y);
		StartCoroutine(DestroyExplosion(x,y,destroyTimer,item));
	}

	public void GenerateExplosion(int x,int y){
		
		view.GenerateExplosion(x,y,destroyTimer);

		view.DestroyBlock(x,y);
	

		GameManager.Instance.ExplosionCheck(x,y);

		StartCoroutine(DestroyExplosion(x,y,destroyTimer));
	}

	public void GenerateItem(int x,int y,int item){
		view.GenerateItem (x, y, item);
	}



	public void GenerateItem(int x,int y){
		//view.GenerateItem (x, y);
	}
	public void CheckFieldState (int x, int y, PlayerController player)
	{
		
		int num = model.GetField () [x, y];

		if (num == Const.BOMBITEM) {
			player.AddBombCount();
			model.SetField(x,y,Const.FLOOR);
			view.DestroyBlock(x,y);
		} else if (num == Const.FIREITEM) {
			player.AddRange();
			model.SetField(x,y,Const.FLOOR);
			view.DestroyBlock(x,y);
		} else if (num == Const.SHOESITEM) {
			player.SpeedUp();
			model.SetField(x,y,Const.FLOOR);
			view.DestroyBlock(x,y);
		} else if (num == Const.HEARTITEM) {model.SetField(x,y,Const.FLOOR);
			view.DestroyBlock(x,y);
		}



	}
}
