using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
public void RestartGame(){

    SceneManager.LoadScene("Level1");

}
public void Menu(){

SceneManager.LoadScene("Menu");

}
    
}
