using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Point CurrentPoint;
	public Point Destination;
	int playerID = 0;
	float speed;
	int bombRange;
	int bombMaxCount;
	public int bombCount;

	// Use this for initialization
	void Start () {
		CurrentPoint = Current(transform.position);
		Destination = Current(transform.position);
		speed = Const.DEFAULT_PLAYER_SPEED;
		bombRange = Const.DEFAULT_BOMB_RANGE;
		bombMaxCount = Const.DEFAULT_BOMB_MAXCOUNT;
		bombCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CurrentPoint = Current (transform.position);
	 	StageController.Instance.CheckFieldState(CurrentPoint.x,CurrentPoint.y,this);

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		//transform.position = Vector3.Lerp (transform.position, new Vector3 (Destination.x, 0.5f, Destination.y), Time.deltaTime * speed);

		if (h > 0) {
			Point next = CurrentPoint + Point.Delta (Direction.Right);
		
			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				transform.position += h*Vector3.right * speed * Time.deltaTime;
				//Destination = next;
			} else {
				if (CurrentPoint.x > transform.position.x) {
					transform.position += Vector3.right * speed * Time.deltaTime;
				}
			}
		} else if (h < 0) {
			Point next = CurrentPoint + Point.Delta (Direction.Left);
		
			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				transform.position +=h*Vector3.right * speed * Time.deltaTime;
				//Destination = next;
			} else {
				if (CurrentPoint.x < transform.position.x) {
					transform.position += h*Vector3.right * speed * Time.deltaTime;
				}
			}
		}

		if (v > 0) {
			Point next = CurrentPoint + Point.Delta (Direction.Up);
		
			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				transform.position += v*Vector3.forward * speed * Time.deltaTime;
				//Destination = next;
			} else {
				if (CurrentPoint.y > transform.position.z) {
					transform.position += v*Vector3.forward * speed * Time.deltaTime;
				}
			}
		} else if (v < 0) {
			Point next = CurrentPoint + Point.Delta (Direction.Down);
		
			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				transform.position += v*Vector3.forward  * speed * Time.deltaTime;
				//Destination = next;
			} else {
				if (CurrentPoint.y < transform.position.z) {
					transform.position += v*Vector3.forward * speed * Time.deltaTime;
				}
			}

		}


		if (Input.GetKeyDown (KeyCode.Z)) {
			PutBomb();
		}
	}

	void Move(){
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Point next = CurrentPoint + Point.Delta (Direction.Right);
		
			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				Destination = next;
			}
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Point next = CurrentPoint + Point.Delta (Direction.Left);

			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				Destination = next;
			}
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Point next = CurrentPoint + Point.Delta (Direction.Up);

			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				Destination = next;
			}
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			Point next = CurrentPoint + Point.Delta (Direction.Down);

			if (StageController.Instance.CanPlayerMove (next.x, next.y)) {
				Destination = next;
			}
		}
	}

	Point Current (Vector3 pos)
	{
		return new Point(Mathf.RoundToInt(pos.x),Mathf.RoundToInt(pos.z));
	}

	void PutBomb ()
	{
		Debug.Log(bombMaxCount);
		if (bombCount < bombMaxCount) {
			BombInfo info = new BombInfo(this,bombRange);
			StageController.Instance.PutBomb (CurrentPoint.x, CurrentPoint.y,info);
			bombCount++;
			AudioManager.Instance.PlaySE(Audio.UNITYCAHN2);
		}
	}

	public void AddRange(){
		bombRange++;
	}
	public void AddBombCount(){
		bombMaxCount++;
	}

	public void SpeedUp ()
	{
		if (speed <= Const.DEFAULT_PLAYER_SPEED) {
			speed *= 1.5f;
		}
	}
}
