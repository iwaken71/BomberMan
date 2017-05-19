using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public BombInfo info;

	void OnDestroy ()
	{
		info.player.bombCount--;

	}
	

	
}

public class BombInfo{

	public PlayerController player;
	public int range;
	public float timer;

	public BombInfo(PlayerController player,int range){
		this.player = player;
		this.range = range;
		timer = Const.BOMBTIMER;
	}
}
