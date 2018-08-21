using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersShop : MonoBehaviour {

    public List<Build> towers;
    public MouseBuilding mouseBuilding;
    


    public void GetTower(int towerNum)
    {
        if (TDGameController.Instance.gold - towers[towerNum].cost >= 0)
        {
            Build newTower = Instantiate(towers[towerNum]);
            mouseBuilding.currentBuild = newTower;
            mouseBuilding.onEndBuild = OnEndBuld;
        }    
    }
    void OnEndBuld(Build build)
    {        
        BuyTower(build);
//        build.onSellTower += SellTower;
//        build.onUpdateTower += UpdateTower;        

    }
    void BuyTower(Build build)
    {

        TDGameController.Instance.gold -= build.cost;
        //Debug.LogError("BuyTower" + mouseBuilding.currentBuild.cost);
    }
    public void SellTower(Build build)
    {
        TDGameController.Instance.gold += build.cost;
        build.Sell();
    }
    public void UpdateTower(Build build)
    {
        if (TDGameController.Instance.gold - build.costUpdate >= 0)
        {
            TDGameController.Instance.gold -= build.costUpdate;
            build.UpdateTower();
        }
    }
}
