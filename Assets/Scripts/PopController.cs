using System.Collections;
using System.Collections.Generic;
using UnityEngine;//4月29日更新

public class PopController : MonoBehaviour
{
    
    public GameObject popup1;
    
    public void POPUP1() {
        //if menu is opened
        if (popup1.transform.GetChild(0).transform.gameObject.activeSelf)
        {
            //close the menu by setting the values to set active false
            for (int i=0; i< popup1.transform.childCount-1; i++)
            {
                popup1.transform.GetChild(i).transform.gameObject.SetActive(false);
            }
        }
        //other vise set the gameobjects to true
        else {
            for (int i = 0; i < popup1.transform.childCount; i++)
            {
                popup1.transform.GetChild(i).transform.gameObject.SetActive(true);
            }
            //TikaraQues.instance.TiMondaiLoad();
        }

    }
}
