using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {
    public static bool open;
    public GameObject OpenedDoor;
    private int currentScene;
    // Use this for initialization
    void Start () {
        open = false;
        if (open == true)
            open = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (open == true)
        {// transform.position = new Vector3(Random.value, Random.value, 0)
            if (currentScene == 2)
            {
                OpenedDoor.transform.position = new Vector3(16f, 7f, 0);// need change based on player location
            }
            else if (currentScene == 3)
            {
                OpenedDoor.transform.position = new Vector3(16.01776f, 7.99023f, 0);// need change based on player location
            }
            else if (currentScene == 4)
            {
                OpenedDoor.transform.position = new Vector3(16.01776f, 7.99023f, 0);// need change based on player location   
            }
        }
	}
}
