using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnabled : MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
