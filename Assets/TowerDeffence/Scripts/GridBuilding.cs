using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilding : MonoBehaviour {

   public  MeshCollider coll;
    public float gridLenght = 1;
    public float gridWidth = 1;
    public GridNode[,] grid;
    [HideInInspector]
    public Vector3 leftTop;
    public GridNode nodePrefab;
    public List<Transform> transformForGrid = new List<Transform>();
    void Start()
    {
        coll = GetComponent<MeshCollider>();
        BuildGrid();
    }
    void BuildGrid()
    {
        leftTop = new Vector3((coll.bounds.center - coll.bounds.extents).x, coll.bounds.center.y, (coll.bounds.center + coll.bounds.extents).z);
        grid = new GridNode[(int)(coll.bounds.extents.x * 2 / gridLenght)+1,(int)(coll.bounds.extents.z * 2 / gridWidth)+1];
       // Debug.Log(grid.Length);
        for (int i = 0; i < coll.bounds.extents.x * 2 / gridLenght; i++)
        {
            for (int j = 0; j < coll.bounds.extents.z * 2 / gridWidth; j++)
            {
                Vector3 leftTop = new Vector3((coll.bounds.center - coll.bounds.extents).x + gridLenght * i, coll.bounds.center.y, (coll.bounds.center + coll.bounds.extents).z - gridWidth * j);
                //var node = new GameObject().AddComponent<GridNode>();
                var node = Instantiate(nodePrefab);
                
                node.SetPosition(leftTop, gridLenght, gridWidth, transformForGrid);
                node.transform.SetParent(transform);
                //               Debug.LogError(grid[i, j]);
                grid[i, j] = node;
//                Debug.LogError(1);
            }
        }

    }

	
}
