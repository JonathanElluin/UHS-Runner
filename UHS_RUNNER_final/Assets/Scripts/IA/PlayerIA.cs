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
		string Name = GameManager.GameMngr.CharaName;
		GameObject chara = (GameObject)Instantiate(Resources.Load(Name, typeof(GameObject)), transform.position, Quaternion.identity);
		chara.transform.SetParent(transform);
		Clips = new AudioClip[1];
		Clips[0] = (AudioClip)Resources.Load(Name + "Mort", typeof(AudioClip));
		return chara;
	}

	protected override void AgentCollide()
	{
		IsDead = true;
		base.AgentCollide();
		StartCoroutine("Dying");
		
	}
	
	IEnumerator Dying()
	{
		yield return new WaitForSeconds(1.5f);
		SetAgentAnimation("IsDying", true);
		yield return new WaitForSeconds(0.2f);
		Dead();
	}

	protected override void Fall()
	{
		IsDead = true;
		base.Fall();
		Dead();
	}

	void Dead()
	{
		
		PlayAgentAudio(0);
		GameManager.GameMngr.PlayerDead();
		Destroy(this);
	}
}
