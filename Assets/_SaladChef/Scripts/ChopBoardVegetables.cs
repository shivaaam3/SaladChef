using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SaladChef
{
	
	public class ChopBoardVegetables : MonoBehaviour {


		public bool isChopping;
		public Slider chopTime;
		// Use this for initialization
		void OnEnable () {

			chopTime = gameObject.GetComponentInChildren<Slider>();
			isChopping = false;
			chopTime.maxValue = GameConstants.timeToChopVegetable;
			chopTime.value = chopTime.maxValue;

		}
		
		// Update is called once per frame
		void Update () {

			if(isChopping && chopTime.value>0)
			{
				gameObject.GetComponentInChildren<Slider>().value -= Time.deltaTime;
			}
			
		}
	}
}
