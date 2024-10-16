using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FlashText : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private float timeSeconds = 0.5f;
    private float timePassed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed >= timeSeconds)
        {
            text.enabled = !text.enabled;
            timePassed = 0;
        }
        timePassed += Time.deltaTime;
    }
}
