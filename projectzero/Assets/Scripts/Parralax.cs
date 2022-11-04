using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    Transform camTransform;
    Vector3 lastCamPos;
    float textureUnitSizeX, textureUnitSizeY;
    [SerializeField] private Vector2 parralaxEffect;

    void Start()
    {
        camTransform = Camera.main.transform;
        lastCamPos = camTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x; 
        //textureUnitSizeY = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;
    }
    void FixedUpdate()
    {
        Vector3 deltaMov=camTransform.position-lastCamPos;
        //transform.position+=  new Vector3(deltaMov.x * parralaxEffect.x, deltaMov.y * parralaxEffect.y);
        lastCamPos = camTransform.position;
        transform.position -= new Vector3(deltaMov.x * parralaxEffect.x, deltaMov.y * parralaxEffect.y);
        if (Mathf.Abs(camTransform.transform.position.x-transform.position.x)>=textureUnitSizeX)
        {
            float offsetpositionX = (camTransform.transform.position.x - transform.position.x)%textureUnitSizeX;
            transform.position = new Vector3(camTransform.transform.position.x+offsetpositionX,transform.position.y);
        }
    }
}
