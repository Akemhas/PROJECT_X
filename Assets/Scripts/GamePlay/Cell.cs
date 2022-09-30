using System.Collections.Generic;
using PROJECT_X.Interfaces;
using UnityEngine;

namespace PROJECT_X
{
	public class Cell : MonoBehaviour,IClickable
	{
		public Vector2 size;
		public Vector2Int cellPos;
		[SerializeField] private GameObject xModel;
		private bool _x;

		private void Awake()
		{
			xModel.SetActive(_x);
		}
		public void Click()
		{
			SetStatus(!_x);
			if(_x) CheckNeighbour();
		}

		private void SetStatus(bool status)
		{
			_x = status;
			xModel.SetActive(status);
		}

		private void CheckNeighbour()
		{
			List<Cell> cellList = new List<Cell>
			{
				this
			};

			CheckAllDirections(cellList,this);

			int count = cellList.Count;
			if(count < 3) return;
			for(int i = 0; i < count; i++) cellList[i].SetStatus(false);
		}

		private void CheckAllDirections(List<Cell> cellList,Cell cell)
		{
			CheckDirection(cellList,cell,1,0);
			CheckDirection(cellList,cell,-1,0);
			CheckDirection(cellList,cell,0,1);
			CheckDirection(cellList,cell,0,-1);
		}

		private void CheckDirection(List<Cell> cellList,Cell cell,int xDir,int yDir)
		{
			int nextColumn = cell.cellPos.x + xDir;
			int nextRow = cell.cellPos.y + yDir;

			if(nextColumn >= Grid.Cells.Length || nextColumn < 0) return;
			if(nextRow >= Grid.Cells[0].Length || nextRow < 0) return;

			Cell nextCell = Grid.Cells[nextColumn][nextRow];

			if(!nextCell._x) return;
			if(cellList.Contains(nextCell)) return;
			cellList.Add(nextCell);
			CheckAllDirections(cellList,nextCell);
		}
	}
}