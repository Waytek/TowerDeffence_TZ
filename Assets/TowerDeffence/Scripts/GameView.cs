using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour {
    [SerializeField]
    Text health;
    [SerializeField]
    Text gold;
    [SerializeField]
    Text wave;
    [SerializeField]
    GameObject startGameBtn;
    [SerializeField]
    GameObject nextWaveBtn;
    [SerializeField]
    GameObject WinView;
    [SerializeField]
    GameObject LooseView;
    public void SetHelth(int healthNum)
    {
        health.text = healthNum.ToString();
    }
    public void SetGold(int goldNum)
    {
        gold.text = goldNum.ToString();
    }
    public void SetWave(int waveNum)
    {
        wave.text = waveNum.ToString();
    }
    public void OpenStartGameBtn()
    {
        startGameBtn.SetActive(true);
    }
    public void OpenNextWaveBtn()
    {
        nextWaveBtn.SetActive(true);
    }
    public void WinGame()
    {
        WinView.SetActive(true);
    }
    public void LooseGame()
    {
        LooseView.SetActive(true);
    }

}
