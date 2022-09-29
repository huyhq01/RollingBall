using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindingKey : MonoBehaviour
{
    void OnGUI()
    {
        if (Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            KeyCode kcode = Event.current.keyCode;
            switch (Setting.Instance.currentControlKey)
            {
                case "MoveLeft":
                    GameSetting.Instance.MoveLeftKey = kcode;
                    break;
                case "MoveRight":
                    GameSetting.Instance.MoveRightKey = kcode;
                    break;
            }
            GetKey obj = GameObject.Find(Setting.Instance.currentControlKey).GetComponent<GetKey>();
            obj.DisplayKey(Setting.Instance.currentControlKey);
            this.gameObject.SetActive(false);
        }
    }
}
