using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

    enum State
    {
        Idle,
        FadingIn,
        FadingOut,
    }

    [SerializeField]
    private Image fadeImage;

    public const float defaultDuration = 1.0f;

    State state;
    //private Color fadeInColor;
    //private Color fadeOutColor;

    //private float fadeProgress;

    // Use this for initialization
    void Start()
    {
        state = State.Idle;
    }

    public IEnumerator FadeIn(float duration = Fader.defaultDuration)
    {
        state = State.FadingIn;

        if (duration == 0)
        {
            Color temp = fadeImage.color;
            temp.a = 1;
            fadeImage.color = temp;
            yield break;
        }

        //fadeProgress = 0;

        while (fadeImage.color.a < 1 && state == State.FadingIn)
        {
            //Debug.Log("FadeIn Loop!");
            Color temp = fadeImage.color;
            temp.a += Time.deltaTime * (1 / duration);
            temp.a = Mathf.Min(1, temp.a);           
            fadeImage.color = temp;
//			Debug.Log("fadeImage.color.a:"+fadeImage.color.a);
//			Debug.Log("state:"+state);
            yield return null;
        }
		//Debug.Log("FadeIn End!");
    }

    public IEnumerator FadeOut(float duration = Fader.defaultDuration)
    {
        state = State.FadingOut;

        if (duration == 0)
        {
            Color temp = fadeImage.color;
            temp.a = 0;
            fadeImage.color = temp;
            yield break;
        }

        while (fadeImage.color.a > 0 && state == State.FadingOut)
        {
            
            Color temp = fadeImage.color;
            temp.a -= Time.deltaTime * (1 / duration);
            temp.a = Mathf.Max(0, temp.a);
            fadeImage.color = temp;

            yield return null;
        }
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.I))
    //    {
    //        StopAllCoroutines();
    //        Debug.Log("Start ScreenFadeIn!");
    //        StartCoroutine(FadeIn());
    //    }

    //    if (Input.GetKey(KeyCode.O))
    //    {
    //        StopAllCoroutines();
    //        Debug.Log("Start ScreenFadeOut!");
    //        StartCoroutine(FadeOut());

    //    }
    //}

}
