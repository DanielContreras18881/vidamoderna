using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject startButton;
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
        _GameStarted = true;
        startButton.SetActive (false);
        StartCoroutine (upgradeLevel ());
    }

    IEnumerator upgradeLevel () {
        yield return new WaitForSeconds (5f);
        _GameLevel += .05f;
        Debug.Log (_GameLevel);
        dj.GetComponent<Dj> ().UpdateLevel (_GameLevel);
        player.GetComponent<Player> ().UpdateLevel (_GameLevel);
        lights.GetComponent<LightRotation> ().UpdateLevel (_GameLevel);
        StartCoroutine (upgradeLevel ());
    }

}