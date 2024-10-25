using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{
    PlayerController _player;
    Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _slider = GetComponent<Slider>();
    }

    // Update is called once per physics step
    void FixedUpdate()
    {
        _player.transform.position = new Vector3(_slider.value, _player.transform.position.y, _player.transform.position.z);
    }
}
