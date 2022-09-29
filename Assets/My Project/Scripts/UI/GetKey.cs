using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = UnityEngine.UI.Text;

public class GetKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DisplayKey(this.gameObject.name);
    }

    public void DisplayKey(string objectName){
        switch (objectName)
        {
            case "MoveLeft":
                this.gameObject.GetComponent<Text>().text = CapitalFirstLetter(GameSetting.Instance.MoveLeftKey.ToString());
                break;
            case "MoveRight":
                this.gameObject.GetComponent<Text>().text = CapitalFirstLetter(GameSetting.Instance.MoveRightKey.ToString());
                break;
        }
    }
    public string CapitalFirstLetter(string keyName)
    {
        return keyName.Length > 1 ? char.ToUpper(keyName[0]) + keyName.Substring(1) : char.ToUpper(keyName[0]).ToString();
    }
}
