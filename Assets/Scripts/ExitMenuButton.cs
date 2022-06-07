using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseClick()
    {
        Debug.Log("Ending Game");
        Application.Quit();
    }
}
