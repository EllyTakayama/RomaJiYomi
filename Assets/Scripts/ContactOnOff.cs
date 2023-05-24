using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactOnOff : MonoBehaviour
{
    [SerializeField] private GameObject contactText;

    public void ContactTex()
    {
        if (contactText.activeSelf)
        {
            contactText.SetActive(false);
        }
        else
        {
            contactText.SetActive(true);
        }
    }
}
