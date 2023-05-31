using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreLinkOnOff : MonoBehaviour
{
    [SerializeField] private GameObject contactText;
    [SerializeField] private GameObject storeLinkText;

    public void StoreTex()
    {
        if (storeLinkText.activeSelf)
        {
            storeLinkText.SetActive(false);
        }
        else
        {
            storeLinkText.SetActive(true);
            contactText.SetActive(false);
        }
    }

}
