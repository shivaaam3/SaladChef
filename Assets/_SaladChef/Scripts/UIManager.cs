using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SaladChef
{
	public class UIManager : MonoBehaviour {

		#region Variables_And_Properties
		private static UIManager instance;
		public List<Sprite> avatarSprites;

		public static UIManager Instance 	//Singleton
		{
			get{
				if(instance == null)
					instance = GameObject.FindObjectOfType<UIManager>();

				return instance;
			}
		}

		#endregion
	}
}
