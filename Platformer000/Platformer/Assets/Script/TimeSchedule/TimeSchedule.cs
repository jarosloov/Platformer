using UnityEngine;
using UnityEngine.UI;

public class TimeSchedule : MonoBehaviour
{
    [SerializeField] private Text time;
    [SerializeField] private float _timeMinutes;
    [SerializeField] private Transform arrow;

    [Space]
    [Header("Задний фон")]
    [SerializeField] private SpriteRenderer backgroundImage;
    
    [Header("Изображения")]
    [SerializeField] private Sprite background1;
    [SerializeField] private Sprite background2;
    [SerializeField] private Sprite background3;
    [SerializeField] private Sprite background4;
    
    private float _arrowNubmer; 
    
    private float _currentTime;
    private int _minutes;
    private int _watch;
    private void Start()
    {
        time = GetComponent<Text>();    
        _arrowNubmer = -90 / 18;
        _watch = 6;
        time.text = _watch + ":00";
    }
    
    private void Update()
    {
        TimeCounter();
        SpriteTime();
    }

    private void SpriteTime()
    {
        if (_watch >= 6 && _watch <= 10)
            backgroundImage.sprite = background1;
        if(_watch >= 11 && _watch <= 15)
            backgroundImage.sprite = background2;
        if (_watch >= 16 && _watch <= 20)
            backgroundImage.sprite = background3;
        if(_watch >= 20 && _watch <= 24)
            backgroundImage.sprite = background4;
    }
    private void TimeCounter()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= 5 * _timeMinutes) { _currentTime = 0; _minutes += 5; }
        else if (_minutes >= 60)
        {
            _watch++;
            _minutes = 0;
            arrow.Rotate(0, 0, _arrowNubmer);
        }
        else if (_watch >= 24)
            _watch = 0;
        else if (_watch == 1)
        {
            _watch = 6;
            arrow.rotation = new Quaternion(0, 0, 0, 1);
        }
        else if (_minutes == 0)
            time.text = _watch + ":00";
        else
            time.text = _watch + ":" + _minutes;
    }
}
