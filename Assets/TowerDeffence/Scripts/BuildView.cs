using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildView : MonoBehaviour {

    public Button sellBtn;
    public Button updateBtn;
    Build currentBuild;
    public TowersShop shop;
    public void Activate(Build build)
    {
        transform.position = build.transform.position + new Vector3(0,3,0);
        gameObject.SetActive(true);
        currentBuild = build;
        updateBtn.GetComponentInChildren<Text>().text = "Update " + build.costUpdate;
        sellBtn.GetComponentInChildren<Text>().text = "Sell " + build.cost;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
        currentBuild = null;
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    public void OnSellClick()
    {
        Debug.Log("Sell" + currentBuild );
        shop.SellTower(currentBuild);
        Deactivate();
    }
    public void OnUpdateClick()
    {
        shop.UpdateTower(currentBuild);
        Activate(currentBuild);
        Debug.Log("Update");
    }
}
