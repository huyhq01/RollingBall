using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isBot, isTop;
    void Start()
    {
        if (isBot)
        {
            transform.position = new Vector2(
            Random.Range(
                SpawnManager.Instance.GetLeftBound(),
                -SpawnManager.Instance.GetLeftBound()), -7);
        }
        else if (isTop)
        {
            transform.position = new Vector2(
            Random.Range(
                SpawnManager.Instance.GetLeftBound(),
                -SpawnManager.Instance.GetLeftBound()), 0);
        }
        else
        {
            transform.position = new Vector2(
            Random.Range(
                SpawnManager.Instance.GetLeftBound(),
                -SpawnManager.Instance.GetLeftBound()), -3);
        }
    }
}
