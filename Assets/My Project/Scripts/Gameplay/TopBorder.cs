using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBorder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetUpTransform();
        
    }
    void SetUpTransform(){
        transform.localScale = new Vector2(Mathf.Abs(SpawnManager.Instance.GetLeftBorder() * 2), transform.localScale.y);
        transform.position = new Vector2(0, SpawnManager.Instance.GetTopBorder());
    }
}
