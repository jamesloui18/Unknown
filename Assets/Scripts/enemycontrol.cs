using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontrol : MonoBehaviour
{ 
  //  private AudioSource audioh;
    //public bool isTigger = false;
    //private int currentScene;

    private void Start()
    {
    }
    private void Update()
    {
        /*if (isTigger == true)
            isTigger = false;*/
    }

 /*   public void OnTriggerEnter2D(Collider2D collision)// in colloder ckeck IStriggrts then it touch the object will have effect
    {
        if (!isTigger)
        {
            if (collision.gameObject.name == "playerP")// 3levels?
            {
                isTigger = true;
                Debug.Log("Player hit bomb");
                AudioManager.MusicSound.PlaySoundEffect(0);
                //Destroy(collision.gameObject);  how to Destroy the bomb after player hit it???

                //TransitionManager.++;   Time bar 5- ?? how 

                if (currentScene == 1)
                    playerP.transform.position = new Vector3(-6.2f, 0.5f, 0f);
                else if (currentScene == 2)
                    playerP.transform.position = new Vector3(-7.6f, -0.13f, 0f);

            }   
        }
    }*/

}