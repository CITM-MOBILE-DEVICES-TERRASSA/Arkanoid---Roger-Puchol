using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{
    private Ball _ball;
    PlayerController _player;
    Slider _slider;

    public bool automatic;
    public float autoSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _player = FindObjectOfType<PlayerController>();
        _slider = GetComponent<Slider>();
    }

    // Update is called once per physics step
    void FixedUpdate()
    {
        if (automatic && _ball)
        {
            _slider.value = Mathf.Lerp(_slider.value, _ball.transform.position.x, Time.deltaTime*autoSpeed);
        }
        
        _player.transform.position = new Vector3(_slider.value, _player.transform.position.y, _player.transform.position.z);
    }
}
