using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NPCMadMen : NPCPawnBase
{
	
	public override void Initialize ()
	{
		m_PawnObject = GameObject.Instantiate( Resources.Load( "Prefabs/NPCMadMan" ) ) as GameObject;
		m_PlayerObject = GameObject.FindGameObjectWithTag ("Player");
		if( !m_PawnObject || !m_PlayerObject )
		{
			Debug.LogError( "Can't correct initialize NPC!" );
		}
		
		SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_IDLE );
		m_Brain = new AIMadMen();
		m_Brain.Initialize();
		
		//Parameters
		m_Parameters.Health = 100;
		m_Parameters.Energy = 100;
		m_Parameters.AgroDistance = 20;
		
		//Animation
		Animator animatiorComponent = m_PawnObject.GetComponent<Animator>();
		Dictionary<AIBase.DecisionsWeight, string> npcAnimations = new Dictionary<AIBase.DecisionsWeight, string>()
		{
			{ AIBase.DecisionsWeight.AIDEC_IDLE_STAND, "Idle" },
			{ AIBase.DecisionsWeight.AIDEC_ATTACK_ARMKICK, "KickLeftArm" },
			{ AIBase.DecisionsWeight.AIDEC_ATTACK_LEGKICK, "KickRightLeg" },
			{ AIBase.DecisionsWeight.AIDEC_MOVEMENT_WALK, "Walk" },
			{ AIBase.DecisionsWeight.AIDEC_MOVEMENT_SPRINT, "Run" },
			{ AIBase.DecisionsWeight.AIDEC_MOVEMENT_JUMP, "Jump" },
		};
		m_AnimationManager = new AnimationManager();
		m_AnimationManager.Initialize( animatiorComponent, npcAnimations );
	}
	
	public override void Update ()
	{
		switch( m_CurrentState )
		{
			case NPCBaseStates.NPCS_IDLE:
				UpdateIdle();
				break;
			case NPCBaseStates.NPCS_MOVING:
				UpdateMovement();
				break;
			case NPCBaseStates.NPCS_DYING:
				UpdateDying();
				break;
		}
	}
	
	public override void Drop ()
	{
		GameObject.Destroy( m_PawnObject );
	}
	
	void UpdateIdle()
	{
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
		if( pawnDecision == AIBase.DecisionsWeight.AIDEC_IDLE_STAND )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_IDLE_STAND );
			if( (m_PlayerObject.transform.localPosition - m_PawnObject.transform.localPosition).magnitude < m_Parameters.AgroDistance )
			{
				SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_MOVING );
			}
		}
	}
	
	void UpdateMovement()
	{
	 	// Smoothly rotates towards target
		Vector3 targetPoint = m_PlayerObject.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(targetPoint - m_PawnObject.transform.position, Vector3.up);
		m_PawnObject.transform.rotation = Quaternion.Slerp(m_PawnObject.transform.rotation, targetRotation, Time.deltaTime * 2.0f); 
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
		if( pawnDecision == AIBase.DecisionsWeight.AIDEC_MOVEMENT_WALK )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_MOVEMENT_WALK );
		}
		else if( pawnDecision == AIBase.DecisionsWeight.AIDEC_MOVEMENT_SPRINT )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_MOVEMENT_SPRINT );
		}
		else if( pawnDecision == AIBase.DecisionsWeight.AIDEC_MOVEMENT_JUMP )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_MOVEMENT_JUMP );
		}
	}
	
	void UpdateDying()
	{
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
	}
}
