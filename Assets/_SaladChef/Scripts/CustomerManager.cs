using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace SaladChef
{
	public class CustomerManager : MonoBehaviour {


		#region Variables
		[SerializeField]
		private int waitingTime;
		[SerializeField]
		private string order;
		[SerializeField]
		private Text orderText;
		[SerializeField]
		private Image avatarImage;
		[SerializeField]
		private Slider waitingTimeSlider;

		private List<int> vegetables;
		private int[] vegetablesPicked;
		#endregion


		#region Monobehaviour_Methods
		void OnEnable()
		{
			vegetables = new List<int>{1,2,3,4,5,6};	// Resets the list everytime the gameobject is enabled
			ChangeAvatar();
			MakeOrder();
		}

		void OnDisable()
		{
			if(UIManager.Instance != null)
			UIManager.Instance.avatarSprites.Insert(0,avatarImage.sprite);		//Adds the sprite back to the list for others to use it
		}
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		#endregion 


		#region Custom_Methods
		private void MakeOrder()
		{
			order = null;
			int temp = Random.Range(2,4);		//Customer can choose a minimum of 2 and a maximum of 3 vegetables. Can be changed in the future according to the requirements.
			vegetablesPicked = new int[temp];

			for(int i=0; i<vegetablesPicked.Length; i++)
			{
				int rand = Random.Range(0,vegetables.Count);
				vegetablesPicked[i] = vegetables[rand];
				vegetables.RemoveAt(rand);

				if(i==vegetablesPicked.Length -1)	//Forming the order string
					order += vegetablesPicked[i].ToString();
				else 
					order += vegetablesPicked[i].ToString() + ",";
			}

			System.Array.Sort(vegetablesPicked);

			orderText.text = order;
		}

		private void ChangeAvatar()
		{
			int temp = Random.Range(0,UIManager.Instance.avatarSprites.Count);
			avatarImage.sprite = UIManager.Instance.avatarSprites[temp];
			UIManager.Instance.avatarSprites.RemoveAt(temp);
		}
		#endregion
	}
}
