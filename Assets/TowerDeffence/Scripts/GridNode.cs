using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour {

    public Vector3 leftTop;
    public Vector3 rightTop;
    public Vector3 leftBottom;
    public Vector3 rightBottom;

    public bool empty = true;
    public bool visibly = false;

    //void OnDrawGizmosSelected()
    //{
       // Debug.DrawLine(leftTop, rightTop, gridColor);
        //Debug.DrawLine(rightTop, rightBottom, gridColor);
        //Debug.DrawLine(rightBottom, leftBottom, gridColor);
        //Debug.DrawLine(leftBottom, leftTop, gridColor);
       
//        Debug.LogError("OnDrawSelected");
    //}
    public void SetPosition(Vector3 leftTop, float lenght, float width, List<Transform> transformForGrid)
    {
        this.leftTop = leftTop;
       // Vector3 center = new Vector3(leftTop.x - lenght/2, leftTop.y, leftTop.z + width/2);
       // transform.position = center;
        rightTop = new Vector3(leftTop.x + lenght, leftTop.y, leftTop.z);
        leftBottom = new Vector3(leftTop.x, leftTop.y, leftTop.z - width);
        rightBottom = new Vector3(leftTop.x + lenght, leftTop.y, leftTop.z - width);
        ProectToEnviroment(transformForGrid);
        transform.localScale = new Vector3(lenght / 10, 1, width / 10);
        Vector3 center = new Vector3(leftTop.x + lenght / 2, this.leftTop.y, leftTop.z - width / 2);
        
        transform.position = center;
    }
    void PnPprotect()
    {
        //Debug.Log(transform.position.y);
        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z) ;
        //Debug.Log(transform.position.y);
        rightTop.y += 0.1f;
        leftBottom.y += 0.1f;
        rightBottom.y += 0.1f;
        leftTop.y += 0.1f;
    }
    public void ProectToEnviroment(List<Transform> transformForGrid)  //TODO свернуть с рекурсией
    {
        RaycastHit lefttopHit;
        Ray ray = new Ray(leftTop, -Vector3.up);
        if(Physics.Raycast(ray, out lefttopHit))
        {
            if(transformForGrid.Find(tr => tr == lefttopHit.transform) == null)
            {
                Destroy(this.gameObject); ;
            }
            leftTop = lefttopHit.point;
            RaycastHit leftBotHit;
            ray = new Ray(leftBottom, -Vector3.up);
            if (Physics.Raycast(ray, out leftBotHit))
            {
                if (transformForGrid.Find(tr => tr == leftBotHit.transform) == null)
                {
                    Destroy(this.gameObject); ;
                }
                leftBottom = leftBotHit.point;
                RaycastHit rightBotHit;
                ray = new Ray(rightBottom, -Vector3.up);
                if (Physics.Raycast(ray, out rightBotHit))
                {
                    if (transformForGrid.Find(tr => tr == rightBotHit.transform) == null)
                    {
                        Destroy(this.gameObject); ;
                    }
                    rightBottom = rightBotHit.point;
                    RaycastHit rightTopHit;
                    ray = new Ray(rightTop, -Vector3.up);
                    if (Physics.Raycast(ray, out rightTopHit))
                    {
                        if (transformForGrid.Find(tr => tr == rightTopHit.transform) == null)
                        {
                            Destroy(this.gameObject); ;
                        }
                        rightTop = rightTopHit.point;
                       
                        if (!CheckY())
                        {
                            Destroy(this.gameObject);
                        }
                        PnPprotect();
                        if (!CheckXZ())
                        {
                            Destroy(this.gameObject);
                        }
                       
                        //Debug.LogError(rightTopHit.transform.name);
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        ChangeView(true);
    }
    bool CheckY()
    {
        if(leftBottom.y - rightBottom.y < 0.1f)
        {
            if(rightBottom.y - leftTop.y < 0.1f)
            {
                if(leftTop.y - rightTop.y < 0.1f)
                {
                    if(rightTop.y - leftBottom.y < 0.1f)
                    return true;
                }
            }
        }
        return false;
    }
    bool CheckXZ()
    {
        RaycastHit hit;
        if(Physics.Linecast(leftTop, rightBottom, out hit))
        {
            return false;
        }
        if (Physics.Linecast(rightTop, leftBottom, out hit))
        {
            return false;
        }
        if (Physics.Linecast(rightTop, leftTop, out hit))
        {
            return false;
        }
        if (Physics.Linecast(leftTop, leftBottom, out hit))
        {
            return false;
        }
        if (Physics.Linecast(leftBottom, rightBottom, out hit))
        {
            return false;
        }
        if (Physics.Linecast(rightBottom, rightTop, out hit))
        {
            return false;
        }
        return true;
    }
    public void ChangeView(bool visibly)
    {
        if (visibly)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }      
        
    }
   
}
