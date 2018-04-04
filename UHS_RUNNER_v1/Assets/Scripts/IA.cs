using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour {
	NavMeshAgent Agent;
	protected bool CanRotate = false;
	protected bool CanJump = false;
	protected EnvElement CurrentTile; 
	// Use this for initialization
	protected virtual void Init () {
		Agent = GetComponent<NavMeshAgent>();
		StopAgent(true);
	}
	

	public void SetDestination(Transform _destination)
	{
		Agent.SetDestination(_destination.position);
		StopAgent(false);
	}

	void StopAgent(bool _stop)
	{
		Agent.isStopped = _stop;
		//anim
	}

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

	protected void JumpAgent(int _dir)
	{
		Transform nextDestination = null;

		nextDestination = CurrentTile.GetDestination(_dir);
		if (nextDestination)
		{
			CanJumpAgent(false);
			SetDestination(nextDestination);
		}
	}

	public void CanRotateAgent(bool _canRotate)
	{
		CanRotate = _canRotate;
	}
	public void CanJumpAgent(bool _canJump)
	{
		CanJump = _canJump;
	}

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

}
