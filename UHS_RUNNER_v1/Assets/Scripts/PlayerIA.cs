using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerIA : IA {
	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (CanRotate)
		{
			int Dir = 0;
			if (Input.GetButtonDown("Left"))
			{
				Dir = -1;
			}

			if (Input.GetButtonDown("Right"))
			{
				Dir = 1;
			}
			RotateAgent(Dir);

		}

		if (CanJump)
		{
			int Dir = 0;
			if (Input.GetButtonDown("Jump"))
			{
				Dir = 1;
			}
			JumpAgent(Dir);


		}
	}
}
