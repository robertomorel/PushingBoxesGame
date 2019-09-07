using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPun
{

    [SerializeField]
    private GameObject _caixa;

    [SerializeField]
    private Button _forca, _explosao;

    public bool forcaUsada, explosaoUsada;

    public int scores;

    public Text score;

    [SerializeField]
    private GameObject _smoke;

    [SerializeField]
    private GameObject _joystick, _joystickBG;

    public static bool joystick;

    Rigidbody2D rigidbody2D;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();

        InvokeRepeating("CriaCaixa", 2.0f, 1.5f);
        scores = 0;

        if (!joystick)
        {
            _joystick.SetActive(false);
            _joystickBG.SetActive(false);

            _joystick.transform.localScale = Vector2.zero;
            _joystickBG.transform.localScale = Vector2.zero;
        }
    }

    void Update()
    {
        if (forcaUsada)
        {
            var colors = _forca.colors;
            colors.normalColor = Color.gray;
            _forca.colors = colors;
            //_forca.enabled = false;
        }
        if (explosaoUsada)
        {
            var colors = _explosao.colors;
            colors.normalColor = Color.gray;
            _explosao.colors = colors;
            //_explosao.enabled = false;
        }

        score.text = scores.ToString();
    }
	
	void CriaCaixa()
    {
		photonView.RPC("CriaCaixaNetwork", RpcTarget.All);
        //Instantiate(_caixa, new Vector2(Random.Range(-8.0f, 8.0f), 8.2f), Quaternion.identity);
    }

    [PunRPC]
    void CriaCaixaNetwork()
    {
        Instantiate(_caixa, new Vector2(Random.Range(-8.0f, 8.0f), 8.2f), Quaternion.identity);
    }

    public void DestroyAllGameObjects()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].tag == "Caixa")
            {
                Destroy(gameObjects[i]);
                scores++;
                GameObject smoke = Instantiate(_smoke, gameObjects[i].transform.position, Quaternion.identity);

            }
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
