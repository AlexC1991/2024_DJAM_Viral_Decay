using System;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;

namespace Batty251
{
    public class DestructionScript : MonoBehaviour
    {
        [Range(-100, 100)] [SerializeField] private float gridPositionX;
        [Range(-100, 100)] [SerializeField] private float gridPositionY;
        [Range(-1, 3)] [SerializeField] private float cellSize;
        [Range(-100, 100)] [SerializeField] private int gridXSize;
        [Range(-100, 100)] [SerializeField] private int gridYSize;
        [Range(-100, 100)] [SerializeField] private int cellExpandX;
        [Range(-100, 100)] [SerializeField] private int cellExpandY;
        private int[,] grid; // Represents the state of each grid cell
        [Header("Tile Prefab")]
        [SerializeField] private Object gridCellPrefab;
        private Vector3 gridCenter;
        private GameObject spawnTile;
        private Vector3 cellCenter;

        
        
        private void Start()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            grid = new int[gridXSize, gridXSize];
            gridCenter = transform.position + new Vector3((gridXSize - gridPositionX) * cellSize / 1, (gridYSize - gridPositionY) * cellSize / 1, 0f);
            
            // Initialize the grid, for example:
            for (int x = cellExpandX; x < gridXSize; x++)
            {
                for (int y = cellExpandY; y < gridYSize; y++)
                {
                    cellCenter = gridCenter + new Vector3(x * cellSize, y * cellSize, 0f);
                    spawnTile = (Instantiate(gridCellPrefab, cellCenter, Quaternion.identity) as GameObject);
                    spawnTile.name = $"Tile {x} {y}";
                    spawnTile.transform.parent = gameObject.transform;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            grid = new int[gridXSize, gridXSize];
            gridCenter = transform.position + new Vector3((gridXSize - gridPositionX) * cellSize / 1, (gridYSize - gridPositionY) * cellSize / 1, 0f);
            
            for (int x = cellExpandX; x < gridXSize; x++)
            {
                for (int y = cellExpandY; y < gridYSize; y++)
                {
                    cellCenter = gridCenter + new Vector3(x * cellSize, y * cellSize, 0f);
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, cellSize, 0f));
                }
            }
        }
    }
}
