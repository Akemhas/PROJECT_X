using UnityEngine;

namespace PROJECT_X
{
	public class Grid : MonoBehaviour
	{
		[SerializeField] private Cell cellPrefab;
		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private Vector2 gridOffset;
		[SerializeField] [Range(-2.25f,2.25f)] private float cameraOffset;

		public static Cell[][] Cells;

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

		private void InitializeGrid()
		{
			DestroyGrid();

			if(gridSize.x < 1) gridSize.x = 1;
			if(gridSize.y < 1) gridSize.y = 1;

			Cells = new Cell[gridSize.x][];

			for(int i = 0; i < gridSize.x; i++) Cells[i] = new Cell[gridSize.y];

			Vector3 startOffset = new Vector3((gridSize.x - 1) * gridOffset.x * .5f,(gridSize.y - 1) * gridOffset.y * .5f,0);
			Vector3 startPos = transform.position - startOffset;

			for(int i = 0; i < gridSize.x * gridSize.y; i++)
			{
				int column = i % gridSize.x;
				int row = i / gridSize.x;

				Vector3 insPos = startPos + new Vector3(column * gridOffset.x,row * gridOffset.y);
				Cell insCell = Instantiate(cellPrefab,insPos,Quaternion.identity,transform);

				insCell.cellPos.x = column;
				insCell.cellPos.y = row;

				insCell.name += $"_{i}";
				Cells[column][row] = insCell;
			}
		}

		private void DestroyGrid()
		{
			while(transform.childCount > 0) DestroyImmediate(transform.GetChild(0).gameObject);
		}
	}
}