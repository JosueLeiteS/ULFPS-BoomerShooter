using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpriteManager : MonoBehaviour
{
   public Sprite[] sprites;
   public TextMeshProUGUI text;
   public Image image;
   public PlayerController playerController;
    public float vidamax;
    public float vidaac;
    public float porcentage;
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = sprites[0];
        vidamax = playerController.health;
        SetearTexto();
    }

    // Update is called once per frame
    void Update()
    {
        SetearTexto();
        if (porcentage >= 80)
        {
            image.sprite = sprites[0];
        }
        else if (porcentage < 80 && porcentage >= 60)
        {
            image.sprite = sprites[1];
        }
        else if (porcentage < 60 && porcentage >= 40)
        {
            image.sprite = sprites[2];
        }
        else if (porcentage < 40 && porcentage >= 20)
        {
            image.sprite = sprites[3];
        }
        else {
            image.sprite = sprites[4];
        }
    }
    void SetearTexto() {
        vidaac = playerController.health;
        porcentage = (vidaac / vidamax) * 100;
        text.text = porcentage.ToString() + '%';
    }
}   
