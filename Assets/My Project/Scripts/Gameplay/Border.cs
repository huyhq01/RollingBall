using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] bool isLeftBound, isBottomBound, isRightBound;
    // Start is called before the first frame update
    void Start()
    {
        // Invoke(nameof(ChangePosition),.1f);
        ChangePosition();
        
    }
    void ChangePosition(){
        if (isLeftBound)
        {
            transform.position = new Vector2(SpawnManager.Instance.GetLeftBorder() - .5f, 0);
        } else if (isRightBound)
        {
            transform.position = new Vector2(-SpawnManager.Instance.GetLeftBorder() + .5f, 0);
        }
        else if (isBottomBound)
        {
            transform.position = new Vector2(0, SpawnManager.Instance.GetBottomBound());
        }
    }
}
