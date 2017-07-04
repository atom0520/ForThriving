using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Atom {
    public class SceneManager : MonoBehaviour {

        //[SerializeField]
        //private GameObject fadingScreenCover;
        //[SerializeField]
        //private GameObject loadingScreenCover;
    
        public const float defaultMinLoadingDuration = 0;

		public IEnumerator currSwapSceneCoroutine{ get; private set;}
//        IEnumerator currScreenFadeInCoroutine;
//        IEnumerator currScreenFadeOutCoroutine;

        public static SceneManager instance { get; private set; }
    
        static public SceneManager GetInstance()
        {
                return instance;
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }



        public void SwapScene(string sceneName, bool applyLoadingScreenCover = false, float minLoadingDuration = defaultMinLoadingDuration, bool applyFadeTransition = true, float fadeDuration = Fader.defaultDuration)
        {
            //Debug.Log("SwapScene!");

            if (currSwapSceneCoroutine != null)
                StopCoroutine(currSwapSceneCoroutine);
            //else
            //    Debug.Log("currSwapSceneCoroutine is null!");

//            if(currScreenFadeInCoroutine != null)
//                StopCoroutine(currScreenFadeInCoroutine);
//            if (currScreenFadeOutCoroutine != null)
//                StopCoroutine(currScreenFadeOutCoroutine);

            currSwapSceneCoroutine = SwapSceneCoroutine(sceneName, applyLoadingScreenCover, minLoadingDuration, applyFadeTransition, fadeDuration);
            StartCoroutine(currSwapSceneCoroutine);
                        
        }

        // Use this for initialization 
        public IEnumerator SwapSceneCoroutine(string sceneName, bool applyLoadingScreenCover=false, float minLoadingDuration = defaultMinLoadingDuration, bool applyFadeTransition=true, float fadeDuration=Fader.defaultDuration) {
      
            //ProgramEventManager.GetInstance().DispatchGlobalEvent("OnSwapSceneStart", null);
            //Debug.Log("start fadingScreenCover fade in!");
			Fader screenFader = ScreenManager.instance.fadingScreenCover.GetComponent<Fader>();

            if (applyFadeTransition)
            {
                //currScreenFadeInCoroutine = ScreenManager.instance.fadingScreenCover.GetComponent<Fader>().FadeIn(fadeDuration);
                
				yield return screenFader.StartCoroutine (screenFader.FadeIn (fadeDuration));                              
            }
                

            if (applyLoadingScreenCover)
                ScreenManager.instance.loadingScreenCover.SetActive(true);

            //yield return StartCoroutine(fadingScreenCover.GetComponent<Fader>().FadeOut());

            float minEndLoadingTime = Time.time + minLoadingDuration;
                        

            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (Time.time < minEndLoadingTime)
            {
                yield return null;
            }

            if (applyLoadingScreenCover)
                ScreenManager.instance.loadingScreenCover.SetActive(false);

            if (applyFadeTransition)
            {
                //currScreenFadeOutCoroutine = ScreenManager.instance.fadingScreenCover.GetComponent<Fader>().FadeOut(fadeDuration);
                //yield return StartCoroutine(currScreenFadeOutCoroutine);    
				yield return screenFader.StartCoroutine(screenFader.FadeOut(fadeDuration));
            }
             
			currSwapSceneCoroutine = null;
            //ProgramEventManager.GetInstance().DispatchGlobalEvent("OnSwapSceneEnd", null);
        }
	
        
    }
}


