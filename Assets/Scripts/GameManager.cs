using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :SingletonMonoBehaviour<GameManager>{

	PlayerController player1;
	void Awake ()
	{
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		} else {
			DontDestroyOnLoad(this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		GeneratePlayer(1,1);
		//GeneratePlayer(Const.WIDTH-2,Const.HEIGHT-2);
	}
	void GeneratePlayer(int x,int y){
		GameObject player =  Instantiate(Resources.Load("Player"),new Vector3(x,0.5f,y),Quaternion.identity) as GameObject;
		player1 = player.GetComponent<PlayerController>();
	}

	public void ExplosionCheck (int x, int y)
	{
		int now_x = player1.CurrentPoint.x;
		int now_y = player1.CurrentPoint.y;

		if (x == now_x && y == now_y) {
			DiePlayer();
		}

	}


	void DiePlayer(){
		player1.gameObject.SetActive(false);
	}

}
