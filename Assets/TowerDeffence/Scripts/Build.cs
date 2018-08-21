using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour {

    public Transform[] buildPoints;
    public int cost;
    public int costUpdate;
    public MeshRenderer meshRender;
    public Material inBuildMaterial;
    public Color canBuildColor = Color.green;
    public Color notBuildColor = Color.red;
    public List<GridNode> stayNodes = new List<GridNode>();    

    public Material standartMaterial;

    public void SetInBuildColor(bool canBuild)
    {
        meshRender.material = inBuildMaterial;
        if (canBuild)
            meshRender.material.color = new Color(canBuildColor.r, canBuildColor.g, canBuildColor.b, 0.5f);
        else
            meshRender.material.color = new Color(notBuildColor.r, notBuildColor.g, notBuildColor.b, 0.5f);
    }
    public void StayBuild(GridNode[] nodes)
    {
        meshRender.material = standartMaterial;
        foreach(GridNode node in nodes)
        {
            node.empty = false;
        }
        stayNodes = new List<GridNode>(nodes);
        ActivateTower(true);
        //transform.position = pos;
    }
    public void UpBuild() //перенос постройки 
    {
        ActivateTower(false);
        foreach (GridNode node in stayNodes)
        {
            node.empty = true;
        }
    }
    public virtual void ActivateTower(bool activate)
    {
        //if (towerAnim)
        //{
        //    towerAnim.enabled = activate;
        //}
    }
    public void Sell()
    {
        UpBuild();
        Destroy(this.gameObject);
        
    }
    public virtual void UpdateTower()
    {
        costUpdate = (int)(costUpdate*1.5f);
        cost = (int)(cost* 1.5f);
    }
    
    }
