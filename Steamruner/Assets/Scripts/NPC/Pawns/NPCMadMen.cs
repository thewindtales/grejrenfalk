using UnityEngine;
using System.Collections;


public class NPCMadMen : NPCPawnBase
{
	public override void Initialize ()
	{
		m_PawnObject = GameObject.Instantiate( Resources.LoadAssetAtPath( "Assets/Prefabs/Coo_ragd", typeof( GameObject ) ) ) as GameObject;
		m_Parameters.Health = 100;
		m_Parameters.Energy = 100;
		m_CurrentState = NPCPawnBase.NPCBaseStates.NPCS_IDLE;
		m_Brain = new AIMadMen();
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
	}
	
	void UpdateMovement()
	{
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
	}
	
	void UpdateDying()
	{
		AIBase.DecisionsWeight pawnDecision = m_Brain.MakeDecision( m_CurrentState );
	}
}
