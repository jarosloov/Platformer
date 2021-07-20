using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolman : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _pointionOfPatrol;
    [SerializeField] private Transform _point;
    [SerializeField] private SpriteRenderer _enemy;

    [SerializeField] private Animator _enemyAnimator;
    

    [SerializeField] private Transform _player;
    [SerializeField] private float _stoppingDistance; // дистания остановке
    [SerializeField] private float _attackDistance;
    
    private bool _moveingRight;
    private bool _chill = false;
    private bool _angry = false;
    private bool _goBack = false;
    private bool _attack = false;

    private float _time;
    [SerializeField] private float _timeAttack;
    
    private void Start()
    {
        _player = gameObject.GetComponent<MovementCharacter>().transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _attackDistance && _angry)
        {
            _chill = _goBack = false;
            //print("Attack");
            _attack = true;
            Attack();
        }
        
        if (Vector2.Distance(transform.position, _point.position) < _pointionOfPatrol && _angry == false)
        {
            _chill = true;
            _goBack = false;
        }

        if (Vector2.Distance(transform.position, _player.transform.position) < _stoppingDistance)
        {
            _angry = true;
            _goBack = false;
            _chill = false;
        }
        
        if (Vector2.Distance(transform.position, _player.transform.position) > _stoppingDistance)
        {
            _goBack = true;
            _angry = false;

        }
        
        if(_chill == true)
            Chill();
        else if(_angry == true)
            Angry();
        else if (_goBack == true)
            GoBack();
        else if (_attack == true)
        {
            Attack();
            print("Attack");
        }
            
    }

    private void Attack()
    {
        _time += Time.deltaTime;
        _enemyAnimator.Play("attack");
        if (_time >= _timeAttack)
        {
            print("Удар");
            _time = 0;
        }
    }

    private void Chill()
    {
        if (transform.position.x > _point.position.x + _pointionOfPatrol)
        {
            _moveingRight = false;
        }
        else if (transform.position.x < _point.position.x - _pointionOfPatrol)
        {
            _moveingRight = true;
        }
        if (_moveingRight)
        {
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime,
                transform.position.y);
            _enemy.flipX = false;
        }
        
        else
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime,
                transform.position.y);
            _enemy.flipX = true;
        }
    }

    private void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            _player.position, _speed * Time.deltaTime);
        
        if (transform.position.x > _player.position.x )
        {
            _moveingRight = false;
        }
        else if (transform.position.x < _player.position.x )
        {
            _moveingRight = true;
        }

        if (_moveingRight)
        {
            _enemy.flipX = false;
        }
        else
        {
            _enemy.flipX = true;
        }
    }

    private void GoBack()
    {
        _enemyAnimator.Play("walk");
        transform.position = Vector2.MoveTowards(transform.position, 
            _point.position, _speed * Time.deltaTime);
        
        if (transform.position.x > _point.position.x + _pointionOfPatrol)
        {
            _moveingRight = false;
        }
        else if (transform.position.x < _point.position.x - _pointionOfPatrol)
        {
            _moveingRight = true;
        }

        if (_moveingRight)
        {
            _enemy.flipX = false;
        }
        else
        {
            _enemy.flipX = true;
        }
    }
}

