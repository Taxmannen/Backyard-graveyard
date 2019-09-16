using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveShaderAnimator : MonoBehaviour
{
    Material mat;
    bool dissolve = true;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            Debug.Log("Dissolving...");

            if(dissolve)
                Dissolve(0f, 1f, 1f);
            else
                Dissolve(1f, 0f, 1f);

            dissolve = !dissolve;
        }
    }

    public void Dissolve(float from, float to, float duration) {
        mat = GetComponent<Renderer>().material;
        StartCoroutine(DissolveCoroutine(from, to, duration));
    }

    private IEnumerator DissolveCoroutine(float from, float to, float duration) {
        float v;

        try {
            v = mat.GetFloat("_cutoff");
        }
        catch(System.Exception e) {
            Debug.LogError("DissolveShaderAnimator exception: " + e);
            yield break;
        }

        for (float i = 0; i <= 1; i += Time.deltaTime / duration) {
            mat.SetFloat("_cutoff", Mathf.Lerp(from, to, i));
            yield return null;
        }
        mat.SetFloat("_cutoff", to);
    }
}
