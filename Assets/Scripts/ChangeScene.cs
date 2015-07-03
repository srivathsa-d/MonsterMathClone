using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void changeSceneTo(int sceneId)
	{
		UnityEngine.Application.LoadLevel (sceneId);

	}
}
