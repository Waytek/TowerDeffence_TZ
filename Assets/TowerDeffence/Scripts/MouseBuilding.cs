using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBuilding : MonoBehaviour {
    public GridBuilding gridBuilding;
    Camera camera;
    public Build currentBuild;
    public System.Action<Build> onEndBuild = delegate (Build b) { };
    public BuildView view;
    public LayerMask raycastBuildLayers;

    void Start()
    {
        camera = GetComponent<Camera>();
        if (!camera)
        {
            Debug.LogError("camera not found");
            Destroy(this);
        }
    }
    List<GridNode> prevosNodes = new List<GridNode>();
	void Update () {
        prevosNodes.Clear();
        if (currentBuild)
        {
            view.Deactivate();
            foreach (GridNode node in gridBuilding.grid)
            {
                if (node != null)
                {
                    node.ChangeView(true);
                }
            }
            currentBuild.UpBuild();
            currentBuild.stayNodes.Clear();
            RaycastHit pivotHit;
            Ray pivotRay = camera.ScreenPointToRay(Input.mousePosition + new Vector3(-35,35,0));
            
            if (Physics.Raycast(pivotRay, out pivotHit,1000, ~(raycastBuildLayers.value + 4 + 1024)))
            {
                //currentBuild.transform.position = pivotHit.point;
                int i = (int)((pivotHit.point.x - gridBuilding.leftTop.x) / gridBuilding.gridLenght);
                int j = (int)((gridBuilding.leftTop.z - pivotHit.point.z) / gridBuilding.gridWidth);
                //Debug.LogError(i + "<" + gridBuilding.grid.GetLength(0) + " " + j + "<" + gridBuilding.grid.GetLength(1)  );
                if (i < gridBuilding.grid.GetLength(0) && j < gridBuilding.grid.GetLength(1) && i >= 0 && j >= 0)
                {
                    if (gridBuilding.grid[i, j] != null)
                    {
                        currentBuild.transform.position = gridBuilding.grid[i, j].leftTop;
                    }
                    else
                    {
                        currentBuild.transform.position = pivotHit.point;
                    }
                }
                else { currentBuild.transform.position = pivotHit.point; }
                int truePoint = 0;
                foreach (Transform buildPoint in currentBuild.buildPoints)
                {
                    RaycastHit hit;
                    Ray ray = new Ray(buildPoint.position,-Vector3.up);

                    if (Physics.Raycast(ray, out hit,1000, ~(raycastBuildLayers.value + 4 + 1024)))
                    {
                        i = (int)((hit.point.x - gridBuilding.leftTop.x) / gridBuilding.gridLenght);
                        j = (int)((gridBuilding.leftTop.z - hit.point.z) / gridBuilding.gridWidth);
                        if (gridBuilding.grid[i, j] != null)
                        {
                            prevosNodes.Add(gridBuilding.grid[i, j]);
                            //gridBuilding.grid[i, j].ChangeView(true);
                            if (gridBuilding.grid[i, j].empty)
                            {
                                truePoint++;
                            }
                        }
                    }
                }
                if (truePoint == currentBuild.buildPoints.Length)
                {
                    currentBuild.SetInBuildColor(true);
                    if (Input.GetMouseButtonUp(0))
                    {
                        currentBuild.StayBuild(prevosNodes.ToArray());
                        onEndBuild.Invoke(currentBuild);
                        currentBuild = null;
                        
                    }
                }
                else
                {
                    currentBuild.SetInBuildColor(false);
                    if (Input.GetMouseButtonUp(0))
                    {
                        //onEndBuild.Invoke(currentBuild);
                        Destroy(currentBuild.gameObject);
                    }
                }
            }
        }
        else
        {
            foreach (GridNode node in gridBuilding.grid)
            {
                if (node != null)
                {
                    node.ChangeView(false);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000, raycastBuildLayers))
                {
                    Build build = hit.transform.GetComponent<Build>();
                    if (build)
                    {
                        //Debug.LogError(EventSystem.current.IsPointerOverGameObject());
                        if (!(EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject()))
                            view.Activate(build);
                    }
                }
                else
                {
                    if (!(EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject()))
                        view.Deactivate();
                }
            }
        }
        
    }
}
