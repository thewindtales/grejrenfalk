using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using NPCState = NPCPawnBase.NPCBaseStates;

public class AIMadMen : AIBase
{
	private const int k_DecisionTime = 2;
	
	public override void Initialize()
	{
		// Initialize weights
		m_DecisionWeightAttack = new List<DecisionWeightPair>();
		m_DecisionWeightAttack.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_ATTACK_ARMKICK, 10 ) );
		m_DecisionWeightAttack.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_ATTACK_LEGKICK, 5 ) );
		
		m_DecisionWeightMovement = new List<DecisionWeightPair>();
		m_DecisionWeightMovement.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_MOVEMENT_WALK, 10 ) );
		m_DecisionWeightMovement.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_MOVEMENT_SPRINT, 5 ) );
		
		m_Neurons.NextDecisionTime = k_DecisionTime;
		m_Neurons.DecisionSpeedIncreaser = 1.0f;
	}
	
	public override DecisionsWeight MakeDecision( NPCState currentPawnState )
	{
		m_Neurons.NextDecisionTime -= Time.deltaTime;
		if( m_Neurons.NextDecisionTime <= 0 || m_ForceDecision )
		{
			m_Neurons.NextDecisionTime = k_DecisionTime;
			m_ForceDecision = false;
			
			switch( currentPawnState )
			{
			case NPCState.NPCS_IDLE:
				return DecisionsWeight.AIDEC_IDLE_STAND;
				break;
			case NPCState.NPCS_MOVING:
				return GetDecisionFromWeight( m_DecisionWeightMovement );
				break;
			case NPCState.NPCS_ATTACKING:
				return GetDecisionFromWeight( m_DecisionWeightAttack );
				break;
			}
		}
		
		return DecisionsWeight.AIDEC_UNDEFINED;
	}
}
