using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    //scroll main texture based on time

    Material BackgroundMaterial;
    public float scrollSpeed = 0.5f;
    float offset;
    float rotate;

    private void Start()
    {
        BackgroundMaterial = GetComponent<Image>().material;
        BackgroundMaterial.SetTextureOffset("_MainTex", new Vector2(0, 0));
    }

    private void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10.0f;
        BackgroundMaterial.SetTextureOffset("_MainTex", new Vector2(offset, offset));

    }
}
