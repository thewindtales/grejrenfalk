using UnityEngine;
using System.Collections;


public abstract class NPCPawnBase
{
	public enum NPCBaseStates
	{
		NPCS_IDLE,
		NPCS_MOVING,
		NPCS_ATTACKING,
		NPCS_FLYING,
		NPCS_DYING
	}
	
	protected struct NPCParameters
	{
		public int Health;
		public int Energy;
		public int AgroDistance;
		public int AttackDistance;
		//TODO: add more characteristics here
	}
	
	protected NPCParameters m_Parameters;
	protected AIBase m_Brain;
	protected GameObject m_PawnObject;
	protected GameObject m_PlayerObject;
	protected NPCBaseStates m_CurrentState;
	protected AnimationManager m_AnimationManager;
	
	public abstract void Initialize();
	public abstract void Update();
	public abstract void Drop();
	
	//Functions that all children used
	public void SetCurrentState( NPCBaseStates state )
	{
		m_CurrentState = state;
	}
	
	public float GetDistanceBetweenPlayerAndPawn()
	{
		return Vector3.Distance( m_PlayerObject.transform.position, m_PawnObject.transform.position );
	}
	
	public GameObject GetPawnGameObject()
	{
		return m_PawnObject;
	}
}
