using UnityEngine;
using System.Collections;

using NPCState = NPCPawnBase.NPCBaseStates;

public class AIMadMen : AIBase
{
	private const int k_DecisionTime = 100;
	
	public override void Initialize()
	{
		// Initialize weights
		m_DecisionWeightAttack.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_ATTACK_ARMKICK, 10 ) );
		m_DecisionWeightAttack.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_ATTACK_LEGKICK, 5 ) );
		
		m_DecisionWeightMovement.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_MOVEMENT_WALK, 10 ) );
		m_DecisionWeightMovement.Add( new DecisionWeightPair( AIBase.DecisionsWeight.AIDEC_MOVEMENT_SPRINT, 5 ) );
		
		m_iNextDecisionTime = k_DecisionTime;
		m_fDecisionSpeedIncreaser = 1.0f;
	}
	
	public override DecisionsWeight MakeDecision( NPCState currentPawnState )
	{
	
		m_iNextDecisionTime -= Time.deltaTime;
		if( m_iNextDecisionTime <= 0 )
		{
			m_iNextDecisionTime = k_DecisionTime;
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
