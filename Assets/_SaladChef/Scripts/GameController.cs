using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SaladChef
{
	public class GameController : MonoBehaviour {

		[SerializeField]
		PlayerController playerControllerLeft;
		[SerializeField]
		PlayerController playerControllerRight;
		[SerializeField]
		GameObject gameOverPanel;
		[SerializeField]
		Text winText;

		bool gameOver;

		void Start()
		{
			gameOver = false;
		}
		// Update is called once per frame
		void Update () {

			if(playerControllerLeft.playerTime <=0 && playerControllerLeft.playerTime<=0 && !gameOver)
			{
				gameOver = true;
				if(playerControllerLeft.score>playerControllerRight.score)
				{
					winText.text = "Left Player Won!!!" +
						"\n SCORE: " + playerControllerLeft.score.ToString();
				}
				else if(playerControllerLeft.score<playerControllerRight.score)
				{
					winText.text = "Right Player Won!!!" +
						"\n SCORE: " + playerControllerRight.score.ToString();
				}
				else if(playerControllerLeft.score == playerControllerRight.score)
				{
					winText.text = "It's a TIE!!!";
				}
				gameOverPanel.SetActive(true);
			}
		}
	}
}
