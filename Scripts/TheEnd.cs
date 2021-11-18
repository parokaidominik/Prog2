using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour

{
    public int Level;
    public bool useIntegerToLoadLevel = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.name == "Player"){
            SceneManager.LoadScene("End");}
        
    }
}
