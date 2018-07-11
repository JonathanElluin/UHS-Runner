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

		if (IsDead) return;
		if (OnNavmesh() && DestinationCache)
		{
			SetDestination(DestinationCache);
			DestinationCache = null;
		}
		if (CanRotate)
		{
			CheckDestinationReached();
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
			CheckDestinationReached();
			int Dir = 0;
			if (Input.GetButtonDown("Jump"))
			{
				Dir = 1;
			}
			JumpAgent(Dir);


		}
	}

	protected override GameObject InstantiateChara()
	{
		GameObject chara = (GameObject)Instantiate(Resources.Load(GameManager.GameMngr.CharaName, typeof(GameObject)), transform.position, Quaternion.identity);
		chara.transform.SetParent(transform);
		return chara;
	}

	protected override void AgentCollide()
	{
		base.AgentCollide();
		StartCoroutine("Dying");
		
	}
	
	IEnumerator Dying()
	{
		yield return new WaitForSeconds(1.5f);
		SetAgentAnimation("IsDying", true);
		Dead();
	}

	protected override void Fall()
	{
		base.Fall();
		Dead();
	}

	void Dead()
	{
		IsDead = true;
		GameManager.GameMngr.PlayerDead();
		Destroy(this);
	}
}
