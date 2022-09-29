using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject dangerPrefab;
    [SerializeField] Camera cam;
    [SerializeField] int defaultSize;
    [SerializeField] int maxSize;

    private ObjectPool<GameObject> Ppool;
    private ObjectPool<GameObject> Dpool;
    private Vector3 spawnPosition;
    private int countToSpawnDanger;
    private float leftBound, bottomBound, widthPrefab, leftBorder, bottomBorder;
    private bool isStart { get; set; }

    public float GetLeftBound() { return leftBound; }
    public float GetBottomBound() { return bottomBound; }

    public float GetTopBorder() { return -bottomBorder; }
    public float GetLeftBorder() { return leftBorder; }
    public float spawnRate { get; set; }


    private void Awake()
    {
        GameManager.UpdateState += SpawnManagerOnStateChange;
    }
    void SpawnManagerOnStateChange(GameState state)
    {
        if (state == GameState.Continue)
        {
            StartCoroutine(nameof(SpawnPlatform));
        } else
        {
            StopCoroutine(nameof(SpawnPlatform));
        }

        if (state == GameState.Restart)
        {
            GameManager.UpdateState -= SpawnManagerOnStateChange;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        widthPrefab = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        Vector3 edgeCamCoordinate = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        leftBorder = edgeCamCoordinate.x;
        bottomBorder = edgeCamCoordinate.y;
        leftBound = leftBorder + (widthPrefab / 2);
        bottomBound = bottomBorder - 2;

        #region createPool
        Ppool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(platformPrefab);
        }, prefab =>
        {
            prefab.gameObject.SetActive(true);
            prefab.transform.position = spawnPosition;
        }, prefab =>
        {
            prefab.gameObject.SetActive(false);
        }, prefab =>
        {
            Destroy(prefab.gameObject);
        }, false, defaultSize, maxSize);

        Dpool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(dangerPrefab);
        }, prefab =>
        {
            prefab.gameObject.SetActive(true);
            prefab.transform.position = spawnPosition;
        }, prefab =>
        {
            prefab.gameObject.SetActive(false);
        }, prefab =>
        {
            Destroy(prefab.gameObject);
        }, false, defaultSize, maxSize);

        #endregion

        countToSpawnDanger = Random.Range(1, 4);
    }

    IEnumerator SpawnPlatform()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            spawnPosition = new Vector3(Random.Range(leftBound, -leftBound), bottomBound);
            if (countToSpawnDanger == 1)
            {
                GameObject d = Dpool.Get();
                countToSpawnDanger = Random.Range(2, 5);
            }
            else
            {
                GameObject p = Ppool.Get();
                countToSpawnDanger--;
            }
        }
    }

    public void Deactive(GameObject platform)
    {
        if (platform.CompareTag(Tag.Danger.ToString()))
        {
            Dpool.Release(platform);
        }
        else
        {
            Ppool.Release(platform);
        }
    }
}
