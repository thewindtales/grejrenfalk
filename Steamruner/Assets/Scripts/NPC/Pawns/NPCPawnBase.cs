using UnityEngine;
using System.Collections;


public abstract class NPCPawnBase
{
	public enum NPCBaseStates
	{
		NPCS_IDLE,
		NPCS_MOVING,
		NPCS_ATTACKING,
		NPCS_DYING
	}
	
	protected struct NPCParameters
	{
		public int Health;
		public int Energy;
		//TODO: add more characteristics here
	}
	
	protected NPCParameters m_Parameters;
	protected AIBase m_Brain;
	protected GameObject m_PawnObject;
	protected NPCBaseStates m_CurrentState;
	
	public abstract void Initialize();
	public abstract void Update();
	public abstract void Drop();
}
