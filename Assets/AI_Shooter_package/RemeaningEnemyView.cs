using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemeaningEnemyView : MonoBehaviour {
    public Text remeaningText;
	// Use this for initialization
    void Start()
    {
        GameController.Instance.onChangeRemeaningEnemy += ChangeRemeaning;
    }
    void ChangeRemeaning(int remeaningEnemy)
    {
        remeaningText.text = remeaningEnemy.ToString();
    }

}
