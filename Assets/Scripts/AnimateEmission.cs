using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEmission : MonoBehaviour
{
    [SerializeField] Color from;
    [SerializeField] Color to;
    [SerializeField] float duration;

    [SerializeField] Renderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }

        StartCoroutine(DoAnimation(from, to));
    }

    IEnumerator DoAnimation(Color from, Color to)
    {
        for(float i = 0; i < 1; i += Time.deltaTime / duration)
        {
            foreach (Renderer renderer in renderers)
            {
                float f = 1;
                Color color = Color.Lerp(from, to, i) * f;
                //Debug.Log($"Setting color to {color}");
                renderer.material.SetColor("_color", color);
                renderer.material.SetColor("_EmissionColor", color);

                yield return null;
            }
        }

        StartCoroutine(DoAnimation(to, from));
    }
}
