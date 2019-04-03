using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlButtonCnt : MonoBehaviour
{
    private bool _isPress = false;
    private Text _txt;

    private void Start()
    {
        _txt = gameObject.GetComponent<Text>();
    }
    public void Press()
   {

        _isPress = !_isPress;
        if(_isPress)
        {
            _txt.text = "Stop loop";
        }else
        {
            _txt.text = "Start loop";
        }
        
   }
}
