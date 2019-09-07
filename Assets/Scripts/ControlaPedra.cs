using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class ControlaPedra : MonoBehaviour
{

    private Rigidbody2D _rb;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _speedJump;

    private bool _isGrounded;

    private Animator _anim;

    private GameObject _gameManager;

    private GameManager _scriptGameManager;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = false;
        _anim = GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager");
        _scriptGameManager = _gameManager.GetComponent<GameManager>();

        _speed = 5.0f;
        _speedJump = 1.0f;
    }

    private void FixedUpdate()
    {

        Vector2 move = Vector2.zero;

        if (GameManager.joystick)
        {
            move = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), transform.position.y);
        }
        else
        {
            move = new Vector2(Input.acceleration.x * 2.5f, transform.position.y);
        }        
        
        _rb.AddForce(move * _speed);

        //Jump();

    }

    public void Jump()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        if (_rb.velocity.y == 0 /*_isGrounded */ )
        {
            _rb.velocity = new Vector2(0, 0);
            _rb.AddForce(new Vector2(0, 500 * _speedJump), ForceMode2D.Force);
        }
        //}
        //rb.velocity = Vector3.zero;
    }

    public void ActStartAnim()
    {
        if (!_scriptGameManager.explosaoUsada)
        {
            _scriptGameManager.explosaoUsada = true;
            StartCoroutine(StartAnim());

        }
    }

    public void ActStartStrength()
    {
        if (!_scriptGameManager.forcaUsada)
        {
            _scriptGameManager.forcaUsada = true;
            StartCoroutine(StartStrength());
        }
    }

    IEnumerator StartAnim()
    {
        _anim.SetBool("start", true);
        yield return new WaitForSeconds(1.0f);
        _anim.SetBool("start", false);
        _gameManager.SendMessage("DestroyAllGameObjects");
    }

    IEnumerator StartStrength()
    {
        _anim.SetBool("star2", true);
        _rb.mass = 10.0f;
        _speed = 100.0f;
        _speedJump = 13.0f;
        yield return new WaitForSeconds(5.0f);
        _rb.mass = 1.0f;
        _speed = 5.0f;
        _speedJump = 1.0f;
        _anim.SetBool("star2", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Hole")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
