using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDGameController : MonoBehaviour {
    public static TDGameController Instance;
    public int startGold = 100;
    private int _gold;
    public int gold { get { return _gold; } set { _gold = value; view.SetGold(_gold); } }
    private int _health;
    public int startHealth = 100;
    public int health
    {
        get { return _health; }
        set { _health = value; view.SetHelth(_health); CheckLooseGame(); }
    }
    public int currentWave = 0;
    public int maxWaveCount = 10;
    public System.Action<int, int> onNextWave = delegate(int wave, int enemyCount) { };
    public System.Action onEndWave = delegate () { };
    public GameView view;
    // Use this for initialization
    void Awake () {
        Instance = this;
        gold = startGold;
        health = startHealth;
        view.SetWave(currentWave);
        view.OpenStartGameBtn();
        onEndWave += CheckWinGame;
    }
    public void NextWave()
    {
        if (currentWave < maxWaveCount)
        {
            currentWave++;
            
        }
        onNextWave.Invoke(currentWave, 10 * currentWave);
        view.SetWave(currentWave);
    }
    void CheckWinGame()
    {
        if(currentWave == maxWaveCount)
        {
            view.WinGame();
            Time.timeScale = 0.1f;
        }
        else
        {
            view.OpenNextWaveBtn();
        }
    }
    void CheckLooseGame()
    {
        if(health <= 0)
        {
            view.LooseGame();
            Time.timeScale = 0.1f;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
