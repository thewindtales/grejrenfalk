using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using NPCState = NPCPawnBase.NPCBaseStates;

public abstract class AIBase
{
	public enum DecisionsWeight
	{
		AIDEC_UNDEFINED = -1,
		
		//Idle decisions
		AIDEC_IDLE_STAND,
		
		//Attack decisions
		AIDEC_ATTACK_STANCE,
		AIDEC_ATTACK_ARMKICK,
		AIDEC_ATTACK_LEGKICK,
		
		//Movement decisions
		AIDEC_MOVEMENT_WALK,
		AIDEC_MOVEMENT_SPRINT,
		AIDEC_MOVEMENT_JUMP,
		
		AIDEC_COUNT
	}
	
	public struct NPCNeurons
	{
		public float NextDecisionTime;
		public float DecisionSpeedIncreaser; 
	}
	
	public struct DecisionWeightPair
	{
		public DecisionWeightPair( DecisionsWeight dec, int weig )
		{
			decisionName = dec;
			decisionWeight = weig;
		}
		public DecisionsWeight decisionName;
		public int decisionWeight;
	}
	
	protected List<DecisionWeightPair> m_DecisionWeightAttack;
	protected List<DecisionWeightPair> m_DecisionWeightMovement;
	protected NPCNeurons m_Neurons;
	protected bool m_ForceDecision;
	
	// Abstract
	public abstract void Initialize();
	public abstract DecisionsWeight MakeDecision( NPCState currentPawnState );
	
	// Helper functions
	protected DecisionsWeight GetDecisionFromWeight( List<DecisionWeightPair> numDecisions )
	{
		//Calculate all decision weights
		int decisionsAllWeight = 0;
		foreach( DecisionWeightPair pair in numDecisions )
		{
			decisionsAllWeight += pair.decisionWeight;
		}
		// Calculate decision
		float decision = decisionsAllWeight * Random.Range( 0.00f, 1.00f );
		
		numDecisions.Reverse();
		foreach( DecisionWeightPair pair in numDecisions )
		{
			decisionsAllWeight -= pair.decisionWeight;
			if( decision >= decisionsAllWeight )
			{
				return pair.decisionName;
			}
		}
		return DecisionsWeight.AIDEC_UNDEFINED;
	}
	
	public void ForceDecision()
	{
		m_ForceDecision = true;
	}
}
