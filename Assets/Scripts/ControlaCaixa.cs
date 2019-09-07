using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCaixa : MonoBehaviourPun
{

    private Rigidbody2D _rb;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddTorque(Random.Range(0.2f, 7.0f));
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Hole")
        {
            Debug.Log("Colidiuu!!");
            _gameManager.scores++;
            StartCoroutine(DestroiCaixa());
        }
    }

    IEnumerator DestroiCaixa()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

}
