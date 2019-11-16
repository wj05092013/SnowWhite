using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour {

	public enum VoiceType
	{
		Type1, Type2, Type3
	}
	public VoiceType vType;

	public void PlayVoiceSound()
	{
		switch (vType)
		{
			case VoiceType.Type1:
				SoundManager.instance.PlayVoiceSound1();
				break;
			case VoiceType.Type2:
				SoundManager.instance.PlayVoiceSound2();
				break;
			case VoiceType.Type3:
				SoundManager.instance.PlayVoiceSound3();
				break;
		}
	}
}
