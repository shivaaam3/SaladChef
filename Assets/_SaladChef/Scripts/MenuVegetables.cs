using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SaladChef
{
	public class MenuVegetables : MonoBehaviour {

		public string vegetableName;
		public int vegetableNumber;



		/// <summary>
		/// Picks the vegetable from the vegetable menu and adds them to picked vegetables list(atmost 2)
		/// If less than two vegetables are selected then adds two vegetables to the list
		/// else changes the vegetables that are already in the script
		/// Enables the picked vegetables' sprite on the chopping board 
		/// </summary>
		public void OnVegetableButtonPress(PlayerController playerController)
		{

			if(playerController.pickedVegetables.Count<2)			//If picked vegetables are less than two then adds to the list 
			{
				Debug.LogFormat("Name: {0}, Number: {1}, GameObjectName: {2}",vegetableName,vegetableNumber,this.gameObject.name);
				playerController.pickedVegetables.Add(vegetableNumber);

				//Adds the Vegetable on the chopping board!
				playerController.chopBoardVegetables[playerController.pickedVegetables.Count-1].GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
				playerController.chopBoardVegetables[playerController.pickedVegetables.Count-1].SetActive(true);
			}

			else                    								//Else replace the first and second element of the picked vegetables
			{													
				if(playerController.pickedVegetableNumber>=2)
				{
					playerController.pickedVegetableNumber = 0;
				}
				playerController.chopBoardVegetables[playerController.pickedVegetableNumber].GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
				++(playerController.pickedVegetableNumber);
			}

		}
	}
}
