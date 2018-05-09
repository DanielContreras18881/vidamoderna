using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Variable
    private float _GameLevel = 1f;
    // Getter and setter
    public float GameLevel {
        get { return _GameLevel; }
        set { _GameLevel = value; }
    }
}