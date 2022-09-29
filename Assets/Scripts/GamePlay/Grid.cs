using System.Collections.Generic;
using UnityEngine;

namespace PROJECT_X
{
	public class Grid : MonoBehaviour
	{
		[SerializeField] private Cell cellPrefab;
		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private Vector2 gridOffset;
		[SerializeField] [Range(-2.25f,2.25f)] private float cameraOffset;

		private List<Cell> _cells = new List<Cell>();
		private Camera _mainCam;

		private void Awake()
		{
			_mainCam = Camera.main;
			var res = Screen.currentResolution;
			var ratio = res.height / (float)res.width;

			float xSize = (gridSize.x - cameraOffset) * (cellPrefab.size.x + gridOffset.x) * ratio;
			float ySize = (gridSize.y - cameraOffset) * (cellPrefab.size.y + gridOffset.y) * (1 - ratio);

			float currentSize = xSize > ySize ? xSize : ySize;

			_mainCam!.orthographicSize = currentSize;

			InitializeGrid();
		}
		[ContextMenu("Initialize Grid")]
		private void InitializeGrid()
		{
			DestroyGrid();
			_cells = new List<Cell>();
			Vector3 startOffset = new Vector3((gridSize.x - 1) * gridOffset.x * .5f,(gridSize.y - 1) * gridOffset.y * .5f,0);
			Vector3 startPos = transform.position - startOffset;

			for(int i = 0; i < gridSize.x * gridSize.y; i++)
			{
				Vector3 insPos = startPos + new Vector3(i % gridSize.x * gridOffset.x,i / gridSize.x * gridOffset.y);
				Cell insCell = Instantiate(cellPrefab,insPos,Quaternion.identity,transform);
				_cells.Add(insCell);
			}
		}

		private void DestroyGrid()
		{
			while(transform.childCount > 0) DestroyImmediate(transform.GetChild(0).gameObject);
		}
	}
}