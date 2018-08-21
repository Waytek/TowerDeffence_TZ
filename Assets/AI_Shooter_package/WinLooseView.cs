using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLooseView : MonoBehaviour {
    public GameObject winImage;
    public GameObject looseView;
    public GameObject startGameView;
    // Use this for initialization
    void Start() {
        GameController.Instance.onWin += Win;
        GameController.Instance.onLoose += Loose;
    }
    void Loose()
    {
        looseView.SetActive(true);
        Invoke("Restart", 3);
    }
    void Win()
    {
        winImage.SetActive(true);
        Invoke("Restart", 3);
    }
    void Restart()
    {
        startGameView.SetActive(true);
        winImage.SetActive(false);
        looseView.SetActive(false);
    }
	
	// Update is called once per frame

}
