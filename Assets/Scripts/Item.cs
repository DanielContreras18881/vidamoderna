using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    protected AudioSource hitAudio;
    Rigidbody2D rb;
    ParticleSystem ps;

    private void Start () {
        hitAudio = GetComponent<AudioSource> ();
        rb = GetComponent<Rigidbody2D> ();
        ps = gameObject.transform.GetChild (0).GetComponent<ParticleSystem> ();
        ps.Stop ();
        gameObject.GetComponent<Renderer> ().enabled = true;
    }

    void OnTriggerEnter2D (Collider2D other) {

        if (other.tag == "Player") {
            ActionToPlayer (other.gameObject, 0);
            StartCoroutine (WaitForParticles (true));
        }

        if (other.tag == "Wall" || other.tag == "Floor") {
            StartCoroutine (WaitForParticles (false));
        }

    }

    IEnumerator WaitForParticles (bool particle) {
        gameObject.GetComponent<Renderer> ().enabled = false;
        if (particle) {
            ps.Play ();
            yield return new WaitForSeconds (1.5f);
            ps.Stop ();
        }
        Destroy (gameObject);
    }

    protected virtual void ActionToPlayer (GameObject go, int action) {
        go.SendMessage ("ItemCollision", action);
    }
}