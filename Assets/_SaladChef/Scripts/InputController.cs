using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaladChef
{

	[System.Serializable]
	public class Columns
	{
		public string columnName;
		public GameObject[] rows;
	}

	public class InputController : MonoBehaviour {

		public KeyCode[] navigationKeys;
		public KeyCode enterKey;
		public Columns[] uiColumns;
		public GameObject[] customerRow;
		public bool inCustomerPanel, inUIPanel;
		public int uiColumnCount, uiRowCount, customerColumnCount;

		private PlayerController playerController;
		// Use this for initialization
		void Start () {
			inUIPanel = true;
			inCustomerPanel = false;

			playerController = this.gameObject.GetComponent<PlayerController>();
			this.transform.GetComponent<RectTransform>().position = uiColumns[uiColumnCount].rows[uiRowCount].GetComponent<RectTransform>().position;
		}
		
		// Update is called once per frame
		void Update () {
			if(inUIPanel)
			{
				UINavigationHandler();
			}

			else if(inCustomerPanel)
			{
				CustomerNavigationHandler();
			}

			if(Input.GetKeyDown(enterKey))
			{
				PressButton();
			}
		}

		private void UINavigationHandler()
		{
			if (Input.GetKeyDown(navigationKeys[0]))
			{
				if (uiRowCount == uiColumns[uiColumnCount].rows.Length - 1)
					uiRowCount = 0;
				else
					++uiRowCount;
				
				this.transform.GetComponent<RectTransform>().position = uiColumns[uiColumnCount].rows[uiRowCount].GetComponent<RectTransform>().position;
			}
			else if (Input.GetKeyDown(navigationKeys[1]))
			{
				if (uiRowCount == 0)
					uiRowCount = uiColumns[uiColumnCount].rows.Length - 1;
				else
					--uiRowCount;
				
				this.transform.GetComponent<RectTransform>().position = uiColumns[uiColumnCount].rows[uiRowCount].GetComponent<RectTransform>().position;
			}
			else if (Input.GetKeyDown(navigationKeys[2]))
			{
				if (uiColumnCount == uiColumns.Length -1)
					uiColumnCount = 0;
				else 
					++uiColumnCount;

				if (uiColumns[uiColumnCount].rows.Length <= uiRowCount)
				{
					uiRowCount = uiColumns[uiColumnCount].rows.Length - 1;
				}
				this.transform.GetComponent<RectTransform>().position = uiColumns[uiColumnCount].rows[uiRowCount].GetComponent<RectTransform>().position;
			}
			else if (Input.GetKeyDown(navigationKeys[3]))
			{
				if (uiColumnCount == 0)
					uiColumnCount = uiColumns.Length -1;
				else
					--uiColumnCount;

				if (uiColumns[uiColumnCount].rows.Length <= uiRowCount)
				{
					uiRowCount = uiColumns[uiColumnCount].rows.Length - 1;
				}
				this.transform.GetComponent<RectTransform>().position = uiColumns[uiColumnCount].rows[uiRowCount].GetComponent<RectTransform>().position;
			}
		}

		private void CustomerNavigationHandler()
		{
			if (Input.GetKeyDown(navigationKeys[2]))
			{
				if (customerColumnCount == customerRow.Length -1)
					customerColumnCount = 0;
				else 
					++customerColumnCount;

				this.transform.GetComponent<RectTransform>().position = customerRow[customerColumnCount].GetComponent<RectTransform>().position;
			}
			else if (Input.GetKeyDown(navigationKeys[3]))
			{
				if (customerColumnCount == 0)
					customerColumnCount = customerRow.Length-1;
				else 
					--customerColumnCount;

				this.transform.GetComponent<RectTransform>().position = customerRow[customerColumnCount].GetComponent<RectTransform>().position;
			}
		}
		private void PressButton()
		{
			RaycastHit2D hit = Physics2D.Raycast(this.transform.position, this.transform.position);
			if(hit != null)
			{
				if(hit.collider.tag == "Vegetable")
				{
					hit.collider.gameObject.GetComponent<MenuVegetables>().OnVegetableButtonPress(playerController);
				}

				else if(hit.collider.tag == "Customer")
				{
					hit.collider.gameObject.GetComponent<CustomerManager>().CompareOrder(playerController);

					inCustomerPanel = false;
					inUIPanel = true;
					uiColumnCount = 0;
					uiRowCount = 0;
					this.transform.GetComponent<RectTransform>().position = uiColumns[uiColumnCount].rows[uiRowCount].GetComponent<RectTransform>().position;
				}

				else if(hit.collider.tag == "ChopButton")
				{
					playerController.ChopVegetables();
				}

				else if(hit.collider.tag == "TrashButton")
				{
					playerController.PutVegetablesInDustbin();
				}	
				else if(hit.collider.tag == "ServeButton")
				{
					inCustomerPanel = true;
					inUIPanel = false;
					this.transform.GetComponent<RectTransform>().position = customerRow[0].GetComponent<RectTransform>().position;
				}
			}
		}
	}
}
