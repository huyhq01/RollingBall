using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;
public class SetKey : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>{
            Setting.Instance.SetBindingKey(this.gameObject.name);
        });
    }
}
