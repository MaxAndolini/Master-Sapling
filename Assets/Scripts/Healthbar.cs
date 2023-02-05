using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image sprite;

    public void UpdateBar(float current)
    {
        gameObject.GetComponent<Slider>().value = current;
        
        if(current > 60)
            sprite.GetComponent<Image>().color = Color.green;
        else if(current < 60 && current > 30)
            sprite.GetComponent<Image>().color = Color.yellow;
        else if(current < 30)
            sprite.GetComponent<Image>().color = Color.red;
    }
}