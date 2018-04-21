using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace SaladChef
{
	public class CustomerManager : MonoBehaviour {


		#region Variables
		[SerializeField]
		private string order;
		[SerializeField]
		private Text orderText;
		[SerializeField]
		private Image avatarImage;
		[SerializeField]
		private Slider waitingTimeSlider;
		[SerializeField]
		private Image waitingTimeSliderFill;
		[SerializeField]
		PlayerController playerControllerLeft;
		[SerializeField]
		PlayerController playerControllerRight;

		private float counterRate;
		private bool changePlayer;
		public bool isAngry,rightPlayerAngry,leftPlayerAngry;
		private List<int> menuVegetables;

		public  List<int> vegetablesPicked;
		#endregion


		#region Monobehaviour_Methods
		void OnEnable()
		{
			waitingTimeSliderFill.color = Color.green;
			counterRate = 1;
			isAngry = false;
			rightPlayerAngry = false;
			leftPlayerAngry = false;
			changePlayer = true;
			menuVegetables = new List<int>{1,2,3,4,5,6};	// Resets the list everytime the gameobject is enabled
			ChangeAvatar();
			MakeOrder();
			waitingTimeSlider.maxValue = GameConstants.customerWaitingTimeForOneVeg * vegetablesPicked.Count;
			waitingTimeSlider.value = waitingTimeSlider.maxValue;
		}

		void OnDisable()
		{
			if(UIManager.Instance != null)
			UIManager.Instance.avatarSprites.Insert(0,avatarImage.sprite);		//Adds the sprite back to the list for others to use it
		}

		
		// Update is called once per frame
		void Update () 
		{
			if(waitingTimeSlider.value>0)
			{
				waitingTimeSlider.value -= Time.deltaTime*counterRate;
			}
			else
			{
				if(changePlayer)
				{
					UpdateScore();
					changePlayer = false;
					ChangePlayer();
				}
			}
		}
		#endregion 


		#region Custom_Methods
		private void MakeOrder()
		{
			order = string.Empty;
			int temp = Random.Range(2,4);		//Customer can choose a minimum of 2 and a maximum of 3 vegetables. Can be changed in the future according to the requirements.
			vegetablesPicked = new List<int>();

			for(int i=0; i<temp; i++)
			{
				int rand = Random.Range(0,menuVegetables.Count);
				vegetablesPicked.Add(menuVegetables[rand]);
				menuVegetables.RemoveAt(rand);

				if(i == temp-1)
					order += vegetablesPicked[i].ToString();
				else 
					order += vegetablesPicked[i].ToString() + ",";
			}

			vegetablesPicked.Sort();

			orderText.text = order;
		}

		private void ChangeAvatar()
		{
			int temp = Random.Range(0,UIManager.Instance.avatarSprites.Count);
			avatarImage.sprite = UIManager.Instance.avatarSprites[temp];
			UIManager.Instance.avatarSprites.RemoveAt(temp);
		}

		public void CompareOrder(PlayerController playerController)
		{

			if(vegetablesPicked.Count == playerController.choppedVegetables.Count)
			{
				for(int i=0; i<playerController.choppedVegetables.Count;i++)
				{
					if(playerController.choppedVegetables[i] != vegetablesPicked[i])
					{
						WrongOrder(playerController);
						break;
					}
					RightOrder(playerController);
				}
			}
			else
				WrongOrder(playerController);
		}

		void RightOrder(PlayerController player)
		{
			Debug.Log("Right order");
			player.UpdateScore(GameConstants.scoreIncrement);
			player.ClearPlate();
			player.choppedVegetables.Clear();
			ChangePlayer();
		}

		void WrongOrder(PlayerController player)
		{
			Debug.Log("Wrong order");
			if(player.gameObject.tag == "RightPlayer")
				rightPlayerAngry = true;
			else if(player.gameObject.tag == "LeftPlayer")
				leftPlayerAngry = true;
			
			isAngry = true;
			counterRate += 0.5f;
			waitingTimeSliderFill.color = Color.red;
		}

		void ChangePlayer()
		{
			this.enabled = false;
			this.enabled = true;
		}

		void UpdateScore()
		{
			if(!isAngry)
			{
				playerControllerRight.UpdateScore(GameConstants.scoreDecrementCustomer);
				playerControllerLeft.UpdateScore(GameConstants.scoreDecrementCustomer);
			}
			else
			{
				if(rightPlayerAngry && leftPlayerAngry)
				{
					playerControllerRight.UpdateScore(GameConstants.scoreDecrementCustomer*2);
					playerControllerLeft.UpdateScore(GameConstants.scoreDecrementCustomer*2);
				}

				else if(rightPlayerAngry && !leftPlayerAngry)
				{
					playerControllerRight.UpdateScore(GameConstants.scoreDecrementCustomer*2);
				}
				else if(!rightPlayerAngry && leftPlayerAngry)
				{
					playerControllerLeft.UpdateScore(GameConstants.scoreDecrementCustomer*2);
				}
			}
		}
		#endregion
	}
}
