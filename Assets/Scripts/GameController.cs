using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject startButton;
    public GameObject exitButton;
    public GameObject dj;
    public GameObject lights;
    public GameObject player;

    // Variable
    private float _GameLevel = 0f;
    // Getter and setter
    public float GameLevel {
        get { return _GameLevel; }
        set { _GameLevel = value; }
    }

    // Variable
    private bool _GameStarted = false;
    // Getter and setter
    public bool GameStarted {
        get { return _GameStarted; }
        set { _GameStarted = value; }
    }

    public delegate void OnVariableChangeDelegate ();
    public event OnVariableChangeDelegate OnVariableChange;

    public void StartGame () {
        _GameLevel = 0f;
        _GameStarted = true;
        startButton.SetActive (false);
        exitButton.SetActive (false);
        StartCoroutine (upgradeLevel ());
    }

    public void EndGame () {
        _GameStarted = false;
        exitButton.SetActive (true);
        StartCoroutine (restartGame ());
    }

    IEnumerator restartGame () {
        yield return new WaitForSeconds (7.5f);
        SceneManager.LoadScene ("RanciusChallenge");
    }

    IEnumerator upgradeLevel () {
        yield return new WaitForSeconds (5f);
        _GameLevel += .055f;
        dj.GetComponent<Dj> ().UpdateLevel (_GameLevel);
        player.GetComponent<Player> ().UpdateLevel (_GameLevel);
        lights.GetComponent<LightRotation> ().UpdateLevel (_GameLevel);
        StartCoroutine (upgradeLevel ());
    }

    public void ExitGame () {
        SceneManager.LoadScene ("Intro");
    }

}