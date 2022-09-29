using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Platform : MonoBehaviour
{
    private bool isStopped { get; set; }
    public int speed { get; set; }

    private void Awake()
    {
        GameManager.UpdateState += OnStateWait;
    }
    private void OnEnable()
    {
        switch (GameManager.Instance.difficulty)
        {
            case 0:
                speed = 2;
                break;
            case 1:
                speed = 3;
                break;
            case 2:
                speed = 5;
                break;
        }
    }
    void OnStateWait(GameState state)
    {
        isStopped = (state == GameState.Wait || state == GameState.Pause || state == GameState.Lose);
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tag.Danger.ToString()))
        {
            SpawnManager.Instance.Deactive(this.gameObject);
        }
    }
}
