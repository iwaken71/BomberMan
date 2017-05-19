using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Point CurrentPoint;
	public Point Destination;
	int playerID = 0;
	float speed = 3;
	int bombRange = 3;
	int bombMaxCount = 1;
	public int bombCount = 0;

	// Use this for initialization
	void Start () {
		CurrentPoint = Current(transform.position);
		Destination = Current(transform.position);
		bombRange = 3;
		bombMaxCount = 1;
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
		if (speed <= 3.0f) {
			speed *= 1.5f;
		}
	}
}
