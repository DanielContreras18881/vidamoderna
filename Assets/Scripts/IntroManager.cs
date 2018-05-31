using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

  public void GoToBasketTraining () {
    SceneManager.LoadScene ("BallGameMenu");
  }

  public void GoToRanciusChallenge () {
    SceneManager.LoadScene ("RanciusChallenge");
  }
}