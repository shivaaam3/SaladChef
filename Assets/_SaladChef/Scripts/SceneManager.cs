using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public void Play()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
	}
}
