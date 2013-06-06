using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NPCMadMen : NPCPawnBase
{
	
	public override void Initialize ()
	{
		m_PawnObject = GameObject.Instantiate( Resources.LoadAssetAtPath( "Assets/Prefabs/NPCMadMan.prefab", typeof( GameObject ) ) ) as GameObject;
		
		m_CurrentState = NPCPawnBase.NPCBaseStates.NPCS_IDLE;
		m_Brain = new AIMadMen();
		
		//Parameters
		m_Parameters.Health = 100;
		m_Parameters.Energy = 100;
		
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
		}
	}
	
	void UpdateMovement()
	{
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
