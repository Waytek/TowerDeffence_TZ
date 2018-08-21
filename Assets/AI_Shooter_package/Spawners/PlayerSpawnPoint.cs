using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour {
    public PlayerController playerPrefab;
    void Start()
    {
        GameController.Instance.playerSpawners.Add(this);
    }
	// Use this for initialization
	public void Spawn()
    {
        PlayerController player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
    void OnDisable()
    {
        GameController.Instance.playerSpawners.Remove(this);
    }
}
