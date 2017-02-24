using UnityEngine;
using System.Collections;

public static class AnimatorExtensions
{
	/// <summary>
	/// Goes to state only if is not already in the state
	/// </summary>
	// Use this for initialization
	public static void goToStateIfNotAlreadyThere(this Animator self, int stateHash)
	{
		if(self.GetCurrentAnimatorStateInfo(0).nameHash != stateHash)
			self.Play(stateHash);
	}
}
