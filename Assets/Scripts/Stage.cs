using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Stage{

	/// <summary>
	/// 0:床,1:壊れない壁,2:壊れる壁,3:爆弾,4:8:Player,9:Enemy
	/// </summary>
	int Width ;
	int Height ;
	int[,] field;

	public Stage ()
	{	
		Width = 15;
		Height = 13;
		field = new int[Width,Height];
	}
	public Stage (int width,int height)
	{	
		this.Width = width;
		this.Height = height;
		field = new int[Width,Height];
		SetNormalStage ();
	}

	public int[,] GetField ()
	{
		return field;
	}


	public void SetInitStage ()
	{
		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Height; j++) {
				SetField(i,j,Const.FLOOR);
			}
		}
	}

	public void SetNormalStage ()
	{
		SetInitStage ();
		for (int i = 0; i < Width; i++) {
			SetField (i, 0, Const.WALL);
			SetField (i, Height - 1, Const.WALL);
		}
		for (int j = 0; j < Height; j++) {
			SetField (0, j, Const.WALL);
			SetField (Width - 1, j, Const.WALL);
		}

		for (int i = 2; i < Width - 2; i++) {
			for (int j = 2; j < Height - 2; j++) {
				if (i % 2 == 0 && j % 2 == 0) {
					SetField (i, j, Const.WALL);
				} else {
					SetField (i, j, Const.BLOKENWALL);
				}
			}
		}
		for (int i = 3; i < Width - 3; i++) {
			SetField (i, 1, Const.BLOKENWALL);
			SetField (i, Height - 2, Const.BLOKENWALL);
		}

		for (int j = 3; j < Height - 3; j++) {
			SetField (1, j, Const.BLOKENWALL);
			SetField (Width-2,j, Const.BLOKENWALL);
		}

		//SetItem();
	}


	public void SetItem ()
	{
		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Height; j++) {
				if (EqualField (i, j, Const.BLOKENWALL)) {
					if (Random.Range (0.0f, 1.0f) < 0.3f) {
						float random = Random.Range (0.0f, 1.0f);

						for (int k = 0; k <= 3; k++) {
							if (random < (k+1) / 4.0f) {
								SetField(i,j,Const.BOMBITEMBLOCK+k);
							}
						}
					}
				}
			}
		}

	}

	public bool CanPutBomb (int x, int y)
	{
		if (x < 0 || y < 0 || x >= Width || y >= Height) {
			return false;
		}

		if (EqualField(x,y,Const.FLOOR)) {
			return true;
		}
		return false;
	}

	public void ExplosionBomb (int x, int y, int range)
	{
		ExplosionBombDirection(x,y,1,0,range);
		ExplosionBombDirection(x,y,-1,0,range);
		ExplosionBombDirection(x,y,0,1,range);
		ExplosionBombDirection(x,y,0,-1,range);
	}

	void ExplosionBombDirection (int x,int y, int dir_x,int dir_y,int range)
	{
		if (range <= 0)
			return;
		for (int i = 0; i <= range; i++) {
			int x2 = x + i*dir_x;
			int y2 = y + i*dir_y;
			if (InField (x2, y2)) {
				if (EqualField(x2,y2,Const.FLOOR)) {
					SetField(x2,y2,Const.EXPLOSION);
				} else if (EqualField(x2,y2,Const.WALL)) {
					break;
					
				} else if(EqualField(x2,y2,Const.BLOKENWALL)){
					SetField(x2,y2,Const.EXPLOSION);
					break;
				}else if(EqualField(x2,y2,Const.BOMB)){
					SetField(x2,y2,Const.EXPLOSION);
				}
			} else {
				break;
			}
		}
	}

	public void PutBomb (int x, int y)
	{
		if (CanPutBomb (x, y)) {
			SetField(x,y,Const.BOMB);
		}

	}

	public void SetField (int x, int y, int num)
	{
		if (InField (x, y)) {
			

			if (num == Const.EXPLOSION) {
			/*
				if (EqualField (x, y, Const.BOMBITEMBLOCK)) {
					StageController.Instance.GenerateExplosion (x, y,Const.BOMBITEM);
				} else if (EqualField (x, y, Const.FIREITEMBLOCK)) {
					StageController.Instance.GenerateExplosion (x, y,Const.FIREITEM);
				} else if (EqualField (x, y, Const.SHOESITEMBLOCK)) {
					StageController.Instance.GenerateExplosion (x, y,Const.SHOESITEM);
				} else if (EqualField (x, y, Const.HEARTITEMBLOCK)) {
					StageController.Instance.GenerateExplosion (x, y,Const.HEARTITEM);
				} else {
					StageController.Instance.GenerateExplosion (x, y);
				}
				*/

				StageController.Instance.GenerateExplosion (x, y);
			}


			field [x, y] = num;

	
		}
	}

	bool EqualField(int x, int y, int num){
		return field[x,y] == num;
	}


	bool InField (int x, int y)
	{
		return (x >= 0 && x < Width && y >= 0 && y < Height);
	}

	public bool CanPlayerMove (int x, int y)
	{
		if (!InField (x, y)) {
			return false;
		}

		if (EqualField (x, y, Const.FLOOR) || EqualField (x, y, Const.EXPLOSION))  {
			return true;
		}

		if (EqualField (x, y, Const.BOMBITEM) || EqualField (x, y, Const.FIREITEM)||EqualField (x, y, Const.HEARTITEM) || EqualField (x, y, Const.SHOESITEM))  {
			return true;
		}
		return false;
	}

	public void PlayerMove (int Num, int directionX, int directionY)
	{
		int nowX = 0;
		int nowY = 0;

		for (int i = 0; i < Width; i++) {
			for (int j = 0; j < Height; j++) {
				if (EqualField (i, j, Num)) {
					nowX = i;
					nowY = j;
					break;
				}
			}
		}
	}

//	IEnumerator SetExplosion (int x, int y, float timer)
//	{
//		if (InField (x, y)) {
//			SetField(x,y,Const.EXPLOSION);
//			yield return new WaitForSeconds(timer);
//			SetField(x,y,Const.FLOOR);
//		}
//	}
}
