using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private float speed;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeRoutine());   
    }

    IEnumerator FadeRoutine()
    {
        Color color = image.color;

        while (image.color.a < 250f)
        {
            color.a += speed * Time.deltaTime;
            image.color = color;

            yield return null;
        }

        yield return null;
    }
}
