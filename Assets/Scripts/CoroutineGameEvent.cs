using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Atom;

public abstract class CoroutineGameEvent : MonoBehaviour
{  
    protected IEnumerator currEvent;
	public bool erased {get; set;}
    bool isSyncGameEventCoroutineRunning;

    public enum Trigger
    {
        Custom = 0,
        PressKey=1,
        Auto=2,
        Sync=3
    }

    protected virtual void Start()
    {
        ProgramEventManager.GetInstance().AddGlobalEventListener("OnEventsNeedRefresh", new ProgramEventManager.EventListener(OnEventsNeedRefresh));
        erased = false;
        isSyncGameEventCoroutineRunning = false;

        Refresh();
    }

    public Trigger trigger { get; protected set; }

    protected virtual void Refresh() {
        if (erased)
            return;

        RefreshCurrEvent();

        if (trigger == Trigger.Auto)
            AttemptStartMainGameEvent();
    }

    public void FirmlyStartMainGameEvent()
    {
        StartCoroutine(ContinueStartGameEvent());
    }

    IEnumerator ContinueStartGameEvent()
    {
        while (!AttemptStartMainGameEvent())
            yield return null;
    }

    public virtual bool AttemptStartMainGameEvent()
    {
		if (erased)
			return false;
		
        if (GameManager.instance.isMainGameEventCoroutineRunning)
            return false;
       
        GameManager.instance.isMainGameEventCoroutineRunning = true;
         
        StartCoroutine(MainGameEvent());
        return true;     
    }

    public virtual bool AttemptStartSyncGameEvent()
    {
		if (erased)
			return false;
		
        if (isSyncGameEventCoroutineRunning)
            return false;

        isSyncGameEventCoroutineRunning = true;

        StartCoroutine(SyncGameEvent());
        return true;
    }

    IEnumerator MainGameEvent()
    {
        yield return currEvent;
        GameManager.instance.isMainGameEventCoroutineRunning = false;
        ProgramEventManager.GetInstance().DispatchGlobalEvent("OnEventsNeedRefresh", null);
    }

    IEnumerator SyncGameEvent()
    {
        yield return currEvent;
        isSyncGameEventCoroutineRunning = false;
        ProgramEventManager.GetInstance().DispatchGlobalEvent("OnEventsNeedRefresh", null);
    }

    protected abstract void RefreshCurrEvent();

    void OnEventsNeedRefresh(object data)
    {
        Refresh();
    }

    protected virtual void OnDestroy()
    {
        ProgramEventManager.GetInstance().RemoveGlobalEventListener("OnEventsNeedRefresh", new ProgramEventManager.EventListener(OnEventsNeedRefresh));
    }
  
}
