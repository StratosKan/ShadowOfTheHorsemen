using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
    private bool shouldFade = false;

    private CanvasGroup CGroup;

	void Start ()
    {
        CGroup = GetComponent<CanvasGroup>();
        StartCoroutine(TimeToPass());
    }

    void Update()
    {
        if (shouldFade)
        {
            CGroup.alpha += Mathf.Lerp(0, 1, Time.deltaTime *1f);

        }
    }

    public IEnumerator TimeToPass() //Starts a timer in seconds
    {
        yield return new WaitForSeconds(1f); 

        shouldFade = true;
    }
}
