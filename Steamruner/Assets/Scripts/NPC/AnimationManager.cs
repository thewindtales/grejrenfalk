using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager
{
	private Dictionary<AIBase.DecisionsWeight, string> m_Animations;
	private Animator m_AnimatorController;
	
	public void Initialize( Animator animator, Dictionary<AIBase.DecisionsWeight, string> animations )
	{
		m_AnimatorController = animator;
		m_Animations = animations;
	}
	
	public void PlayAnimation( AIBase.DecisionsWeight decisionAnimation )
	{
		if( m_Animations.ContainsKey( decisionAnimation ) )
		{
			m_AnimatorController.SetBool( m_Animations[decisionAnimation], true );
		}
		else
		{
			Debug.LogError( "Can't start animation. Animation for this NPC action isn't defined" );
		}
	}
	
	public void StopAnimation( AIBase.DecisionsWeight decisionAnimation )
	{
		if( m_Animations.ContainsKey( decisionAnimation ) )
		{
			m_AnimatorController.SetBool( m_Animations[decisionAnimation], false );
		}
		else
		{
			Debug.LogError( "Can't stop animation. Animation for this NPC action isn't defined" );
		}
	}
	
	public void PlayAnimation( string animationName )
	{
		m_AnimatorController.SetBool( animationName, true );
	}
	
	public void StopAnimation( string animationName )
	{
		m_AnimatorController.SetBool( animationName, false );
	}
	
	public void StopAllAnimations()
	{
		for( AIBase.DecisionsWeight index = AIBase.DecisionsWeight.AIDEC_IDLE_STAND; index < AIBase.DecisionsWeight.AIDEC_COUNT; index++ )
		{
			if( m_Animations.ContainsKey( index ) )
			{
				m_AnimatorController.SetBool( m_Animations[index], false );
			}
		}
	}
}
