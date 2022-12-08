using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public void SetPlayer(GameObject player)
    {
        player.transform.position = _spawnPoint.position;
        player.SetActive(true);

    }
}