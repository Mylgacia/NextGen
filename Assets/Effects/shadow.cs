using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{

    [SerializeField] private GameObject shadow_obj0;
    [SerializeField] private GameObject shadow_obj1;
    [SerializeField] private GameObject shadow_obj2;
    private bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shadow1()
    {
        //shadow_obj1.SetActive(isActivated = !isActivated);
        shadow_obj1.SetActive(true);
    }

    public void Shadow2()
    {
        // shadow_obj2.SetActive(isActivated = !isActivated);
        shadow_obj2.SetActive(true);
    }
}
