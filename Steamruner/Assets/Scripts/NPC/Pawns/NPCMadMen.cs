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
		
		m_Brain = new AIMadMen();
		m_Brain.Initialize();
		
		//Parameters
		m_Parameters.Health = 100;
		m_Parameters.Energy = 100;
		m_Parameters.AgroDistance = 40;
		m_Parameters.AttackDistance = 5;
		
		//Animation
		Animator animatiorComponent = m_PawnObject.GetComponent<Animator>();
		Dictionary<AIBase.DecisionsWeight, string> npcAnimations = new Dictionary<AIBase.DecisionsWeight, string>()
		{
			{ AIBase.DecisionsWeight.AIDEC_IDLE_STAND, "Idle" },
			{ AIBase.DecisionsWeight.AIDEC_ATTACK_STANCE, "Stance" },
			{ AIBase.DecisionsWeight.AIDEC_ATTACK_ARMKICK, "PunchHandLeft" },
			{ AIBase.DecisionsWeight.AIDEC_ATTACK_LEGKICK, "PunchRightLeg" },
			{ AIBase.DecisionsWeight.AIDEC_MOVEMENT_WALK, "Walk" },
			{ AIBase.DecisionsWeight.AIDEC_MOVEMENT_SPRINT, "Run" },
			{ AIBase.DecisionsWeight.AIDEC_MOVEMENT_JUMP, "Jump" }
		};
		m_AnimationManager = new AnimationManager();
		m_AnimationManager.Initialize( animatiorComponent, npcAnimations );
		
		SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_FLYING );
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
			case NPCBaseStates.NPCS_ATTACKING:
				UpdateAttacking();
				break;
			case NPCBaseStates.NPCS_FLYING:
				UpdateFlying();
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
		bool isPawnLoockAtPlayer = RotatePawnToPlayer();
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
		if( pawnDecision == AIBase.DecisionsWeight.AIDEC_IDLE_STAND )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_IDLE_STAND );
			if( GetDistanceBetweenPlayerAndPawn() < m_Parameters.AgroDistance && isPawnLoockAtPlayer )
			{
				SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_MOVING );
				return;
			}
			if( GetDistanceBetweenPlayerAndPawn() < m_Parameters.AttackDistance && isPawnLoockAtPlayer )
			{
				SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_ATTACKING );
				return;
			}
		}
	}
	
	void UpdateMovement()
	{
	 	// Smoothly rotates towards target
		bool isPawnLoockAtPlayer = RotatePawnToPlayer();
		float distanceBetwenPlayerAndPawn = GetDistanceBetweenPlayerAndPawn();
		if( distanceBetwenPlayerAndPawn < m_Parameters.AttackDistance )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_IDLE_STAND );
			SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_ATTACKING );
			return;
		}
		else if( !isPawnLoockAtPlayer )
		{
			return;
		}
		
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
	
	void UpdateAttacking()
	{
		RotatePawnToPlayer();
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
		
		//AlWays in attack stance
		m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_ATTACK_STANCE );
		
		float distanceBetwenPlayerAndPawn = GetDistanceBetweenPlayerAndPawn();
		if( distanceBetwenPlayerAndPawn > m_Parameters.AttackDistance )
		{
			SetCurrentState( NPCPawnBase.NPCBaseStates.NPCS_IDLE );
			return;
		}
		
		if( pawnDecision == AIBase.DecisionsWeight.AIDEC_ATTACK_ARMKICK )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_ATTACK_ARMKICK );
		}
		else if( pawnDecision == AIBase.DecisionsWeight.AIDEC_ATTACK_LEGKICK )
		{
			m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_ATTACK_LEGKICK );
		}
	}
	
	void UpdateDying()
	{
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
	}
	
	bool RotatePawnToPlayer()
	{
		Vector3 targetPoint = m_PlayerObject.transform.position;
		Quaternion targetRotation = Quaternion.LookRotation( targetPoint - m_PawnObject.transform.position );
		//Rotate only by Y axis
		targetRotation.x = 0;
		targetRotation.z = 0;
		Quaternion oldRotation = m_PawnObject.transform.rotation;
		m_PawnObject.transform.rotation = Quaternion.Slerp(m_PawnObject.transform.rotation, targetRotation, Time.deltaTime * 3.0f); 
		
		bool isPawnRotatedToPlayer = oldRotation == m_PawnObject.transform.rotation;
		return isPawnRotatedToPlayer;
	}
	
	void UpdateFlying()
	{
		m_AnimationManager.PlayAnimation( AIBase.DecisionsWeight.AIDEC_IDLE_STAND );
		m_PawnObject.transform.position = m_PawnObject.transform.position + ( ( Vector3.down.normalized * 3.8f ) * Time.deltaTime );
	}
	
	void SetCurrentState( NPCBaseStates state )
	{
		base.SetCurrentState( state );
		m_Brain.ForceDecision();
	}
}
