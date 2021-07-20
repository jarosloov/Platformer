using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInfo : MonoBehaviour
{
    [SerializeField] private GameObject board;
    private bool _onTriggerBoard;
    private bool _input;
    
    private void Start()
    {
        board.SetActive(false);
    }
    
     private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && _onTriggerBoard)
        {
            _input = !_input;
            board.SetActive(_input);
        }
              
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _onTriggerBoard = other.GetComponent<BoardTrigger>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _onTriggerBoard = !other.GetComponent<BoardTrigger>();
    }
    
}
