using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovementCharacter : MonoBehaviour
{
    
    [SerializeField] private float _speed;
    [SerializeField] private float _fastSpeed;
    
    [SerializeField] private Rigidbody2D _character;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _jampForse;
    
    
    // Бар с энергией 
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _energyBar;
    [SerializeField] private float _fill;
    
    private float _timeDelayHP;  // текщее време 
    [SerializeField] private float _recoveryTime; // через какое  время востанавливать
    
    private float _timeDelayHPDoun;  
    [SerializeField] private float _recoveryTimeDoun; 

    private bool _onGround;
    
    private Transform _enemy;

    private void Start()
    {
        _character = GetComponent<Rigidbody2D>();
        _onGround = true;

        _fill = 1f;
        _image.fillAmount = _fill;
        _energyBar.SetActive(false);

        _enemy = gameObject.GetComponent<Patrolman>().transform;

        Physics2D.queriesStartInColliders = false;
    }
        
    private void Update()
    {
        Flip();
        Movement();
        Animation();
        Jamp();
        _image.fillAmount = _fill;
        if (_fill < 1)
            _energyBar.SetActive(true);
        else
            _energyBar.SetActive(false);

        RaycastHit2D hit = Physics2D.Raycast(transform.position,
            transform.localScale.x * Vector2.right, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + transform.localScale  * Vector2.right);
    }
    
    private void Jamp()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
            _character.velocity = new Vector2(_character.velocity.x, _jampForse);
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.Mouse0) ) return;
        _character.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed, _character.velocity.y);
        if (Input.GetKey(KeyCode.LeftShift) && _onGround && Input.GetAxis("Horizontal") != 0 && _fill > 0)
        {
            _character.velocity = _fill > 0 ? new Vector2(Input.GetAxis("Horizontal") * _fastSpeed, _character.velocity.y) : 
                new Vector2(Input.GetAxis("Horizontal") * _speed, _character.velocity.y);
            //StartCoroutine(EnergyDown());
            _timeDelayHPDoun += Time.deltaTime;
            if (_timeDelayHPDoun >= _recoveryTimeDoun)
            {
                _timeDelayHPDoun = 0;
                _fill -=  0.05f;
                if (_fill < 0)
                    _fill = 0;
            }
        }
        // Востоновление энергии
        else if (_fill < 1f && !Input.GetKey(KeyCode.LeftShift))
        {
            _timeDelayHP += Time.deltaTime;
            if (_timeDelayHP >= _recoveryTime)
            {
                _fill += 0.05f;
                _timeDelayHP = 0;
                if (_fill >= 1)
                    _fill = 1;
            }
        }
        _energyBar.SetActive(Input.GetKey(KeyCode.LeftShift));
    }

    private IEnumerator EnergyDown()
    {
        
        if (Input.GetKey(KeyCode.LeftShift) && _fill > 0 && Input.GetAxis("Horizontal") != 0)
        {
            for (float i = 0; i < 1; i += 0.01f)
            {
                _fill -= 0.01f;
                yield return new WaitForSeconds(0.1f);
                if(!Input.GetKey(KeyCode.LeftShift)) 
                    yield break;
            }
        }
    }




    private void Animation()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetAxis("Horizontal") != 0)
        {
            if (_enemy.position.x < transform.position.x)
            {
                _spriteRenderer.flipX = true;
            }
            if (_enemy.position.x > transform.position.x)
            {
                _spriteRenderer.flipX = false;
            }
            _animator.Play("DashAttack");
        }
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetAxis("Horizontal") == 0)
        {
            _animator.Play("Attack");
        }    
        
        else if(Input.GetAxis("Horizontal") == 0 && _onGround && !Input.GetKey(KeyCode.Mouse0))
            _animator.Play("IDLE");
        else if(!Input.GetKey(KeyCode.LeftShift) && _onGround && !Input.GetKey(KeyCode.Mouse0))
            _animator.Play("Running");
        else if(_onGround && !Input.GetKey(KeyCode.Mouse0))
            _animator.Play("FastRunning");
        else if(Input.GetKeyDown(KeyCode.Space))
            _animator.Play("Jump");
        else if(Input.GetKey(KeyCode.Space) )
            _animator.Play("Fall");
    }

    private void Flip()
    {
        /*if ((Input.GetAxis("Horizontal") > 0 && !_spriteRight) 
            || (Input.GetAxis("Horizontal") < 0 && _spriteRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            _spriteRight = !_spriteRight;
        }*/
        if(Input.GetAxis("Horizontal") < 0)
            _spriteRenderer.flipX = true;
        else if(Input.GetAxis("Horizontal") > 0)
            _spriteRenderer.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CheckingGround>())
            _onGround = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CheckingGround>())
            _onGround = false;
    }
}


