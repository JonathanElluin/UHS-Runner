using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour {
	NavMeshAgent Agent;
	Animator AgentAnimator;
	AudioSource AgentSoundSource;
	protected bool CanRotate = false;
	protected bool CanJump = false;
	protected bool IsDead = false;
	protected EnvElement CurrentTile;
	public  AudioClip[] Clips;
	protected Transform DestinationCache;

	// Use this for initialization
	protected virtual void Init () {
		AgentAnimator = InstantiateChara().GetComponent<Animator>();
		AgentSoundSource = GetComponent<AudioSource>();
		Agent = GetComponent<NavMeshAgent>();
	}
	
	//check on navmesh
	protected bool OnNavmesh()
	{
		return Agent.isOnNavMesh;
	}

	//set agent destination
	public void SetDestination(Transform _destination)
	{
		if (OnNavmesh())
		{
			Agent.SetDestination(_destination.position);
			StopAgent(false);
		}
		else DestinationCache = _destination;
		
	}

	//check remaining distance to goal
	public void CheckDestinationReached()
	{
		if(Agent.remainingDistance <= Agent.stoppingDistance)
		{
			if (CanJump) {
				if (Agent.remainingDistance <= 1)Fall();
			} 
			else StopAgent(true);
			
		}
	}

	public void CheckDestinationReached(float _currentDistance)
	{
		if (_currentDistance <= Agent.stoppingDistance)
		{
			StopAgent(true);
		}
		else if (Agent.isStopped) StopAgent(false);
	}

	//stop/unstop agent
	void StopAgent(bool _stop)
	{
		Agent.isStopped = _stop;
		SetAgentAnimation("IsStop", _stop);
	}

	//agent turn on turn tilr
	protected virtual void RotateAgent(int _dir)
	{
		Transform nextDestination = null;

		nextDestination = CurrentTile.GetDestination(_dir);
		if (nextDestination)
		{
			CanRotateAgent(false);
			SetDestination(nextDestination);
		}
	}

	//agent jump on jump tile
	protected void JumpAgent(int _dir)
	{
		Transform nextDestination = null;

		nextDestination = CurrentTile.GetDestination(_dir);
		if (nextDestination)
		{
			SetAgentAnimation("IsJump", CanJump);
			CanJumpAgent(false);
			StartCoroutine("EndJump");
			SetDestination(nextDestination);
		}
	}

	IEnumerator EndJump()
	{
		yield return new WaitForSeconds(0.5f);
		SetAgentAnimation("IsJump", CanJump);
	}

	//action bool
	public void CanRotateAgent(bool _canRotate)
	{
		CanRotate = _canRotate;
	}
	public void CanJumpAgent(bool _canJump)
	{
		CanJump = _canJump;
	}

	//set the tile where agent are
	public void SetCurrentTile(EnvElement _CurrentTile)
	{
		CurrentTile = _CurrentTile;
	}

	protected float GetAgentSpeed()
	{
		return Agent.speed;
	}

	protected void SetAgentSpeed(float _value)
	{
		Agent.speed += _value;
	}


	protected virtual GameObject InstantiateChara()
	{
		return null;
	}


	protected virtual void AgentCollide()
	{
		StopAgent(true);
		gameObject.GetComponent<Collider>().enabled = false;
	}

	private void OnCollisionEnter(Collision collision)
	{
	   if(collision.collider.tag == "Monster" || collision.collider.tag == "Player")
		{
			AgentCollide();
			
		}
	}


	protected void SetAgentAnimation(string _param, bool _value)
	{
		if (AgentAnimator) AgentAnimator.SetBool(_param, _value);
	}

	protected void PlayAgentAudio(int _index)
	{
		AgentSoundSource.PlayOneShot(Clips[_index]);
	}

	protected virtual void Fall()
	{
		CanJumpAgent(false);
		SetAgentAnimation("IsFall", true);
	}
}
