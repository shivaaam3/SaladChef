using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SaladChef
{
	public class PlayerController : MonoBehaviour {

		#region Variables
	
		public List<int> pickedVegetables;
		public List<int> choppedVegetables;
		public int pickedVegetableNumber;		//index for the pickedvegetable list which will reset to zero after 
												//two vegetables are added since at one time only two vegetables can be selected
		public float playerTime;
		public float score;
		public Text timeText;
		public Text scoreText;
		public GameObject plate;
		public List<GameObject> chopBoardVegetables;

		private bool choppingVegetables;

		#endregion

		#region Monobehaviour_Methods

		void Start () {
			choppingVegetables = false;
			playerTime = GameConstants.playerTime;
			UpdateScore(0);
		}
		
		// Update is called once per frame
		void Update () {
			playerTime -= Time.deltaTime;
			timeText.text = string.Format("TIME: {0:00}:{1:00}",(int)(playerTime/60), (int)(playerTime%60));
		} 

		#endregion

		#region Custom_Methods_and_Coroutines

		public void ChopVegetables()
		{
			if(pickedVegetables.Count == 0 || choppingVegetables)
				return;
			
			StartCoroutine(ChopVegetablesCoroutine());
		}

		public void PutVegetablesInDustbin()
		{
			ClearPlate();
			choppedVegetables.Clear();
			UpdateScore(GameConstants.scoreDecrementDustbin);
		}

		public void ClearPlate()
		{
			if(choppedVegetables.Count == 0)
				return;
			for(int i =0; i<plate.transform.childCount;i++)
				Destroy(plate.transform.GetChild(i).gameObject);
		}

		/// <summary>
		/// Chops the picked vegetables and adds them to chopped vegetables list.
		/// adds the chopped vegetables sprites to the plate
		/// </summary>
		IEnumerator ChopVegetablesCoroutine()
		{
			choppingVegetables = true;
			int i = 0;
			foreach(int vegetable in pickedVegetables)
			{
				chopBoardVegetables[i].GetComponent<ChopBoardVegetables>().isChopping = true;

				yield return new WaitForSeconds(GameConstants.timeToChopVegetable);

				GameObject plateVegetable = new GameObject();
				plateVegetable.transform.SetParent(plate.transform, false);
				plateVegetable.AddComponent<Image>();
				plateVegetable.GetComponent<Image>().sprite = chopBoardVegetables[i].GetComponent<Image>().sprite;
				chopBoardVegetables[i].SetActive(false);
				++i;

				choppedVegetables.Add(vegetable);
			}
			pickedVegetables.Clear();
			choppedVegetables.Sort();
			choppingVegetables = false;
		}
			
		public void UpdateScore(int s)
		{
			score += s;
			scoreText.text = string.Format("Score: {0:00}",score);
		}
		#endregion
	}
}
