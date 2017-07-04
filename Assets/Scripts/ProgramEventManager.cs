using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class ProgramEventManager : MonoBehaviour
{

    public delegate void EventListener(object data);

    class Event : UnityEvent<object>
    {

    }

    Dictionary<string, EventListener> globalEventListenerDict;

    Dictionary<GameObject, Dictionary<string, EventListener>> localEventListenerDict;

    public static ProgramEventManager instance = null;

    //private ProgramEventManager()
    //{
    //    globalEventListenerDict = new Dictionary<string, EventListener>();
    //    localEventListenerDict = new Dictionary<GameObject, Dictionary<string, EventListener>>();
    //}

    public static ProgramEventManager GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("ProgramEventManager.instance is null!");
            //instance = new ProgramEventManager();
        }

        return instance;
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }            

        globalEventListenerDict = new Dictionary<string, EventListener>();
        localEventListenerDict = new Dictionary<GameObject, Dictionary<string, EventListener>>();

    }

    //void Start()
    //{
    //    //TestUnityEvent();
    //    TestDelegate();
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown("p"))
    //    {
    //        PrintEventListenersInfo();
    //    }           
    //}

    void AddEventListener(Dictionary<string, EventListener> dict, string type, EventListener listener)
    {
        if (!dict.ContainsKey(type))
        {
            dict.Add(type, listener);
        }
        else
        {
            dict[type] = (EventListener)Delegate.Combine(dict[type], listener);
            //dict[type] += listener;
        }
    }

    void RemoveEventListener(Dictionary<string, EventListener> dict, string type, EventListener listener)
    {
        if (!dict.ContainsKey(type))
        {
            return;
        }
        else
        {
            dict[type] = (EventListener)Delegate.Remove(dict[type], listener);
            //dict[type] -= listener;
        }
    }

    public void AddGlobalEventListener(string type, EventListener listener)
    {
        AddEventListener(globalEventListenerDict, type, listener);
    }

    public void RemoveGlobalEventListener(string type, EventListener listener)
    {
        RemoveEventListener(globalEventListenerDict, type, listener);
    }

    public void DispatchGlobalEvent(string type, object data)
    {
        if (globalEventListenerDict.ContainsKey(type))
        {
            if(globalEventListenerDict[type]!=null)
                globalEventListenerDict[type](data);
        }
    }

    public void AddLocalEventListener(GameObject obj, string type, EventListener listener)
    {
        if (!localEventListenerDict.ContainsKey(obj))
        {
            Dictionary<string, EventListener> dict = new Dictionary<string, EventListener>();
            localEventListenerDict.Add(obj, dict);
            AddEventListener(dict, type, listener);
        }
        else
        {
            AddEventListener(localEventListenerDict[obj], type, listener);
        }
    }

    public void RemoveLocalEventListener(GameObject obj, string type, EventListener listener)
    {
        if (!localEventListenerDict.ContainsKey(obj))
        {
            return;
        }
        else
        {
            RemoveEventListener(localEventListenerDict[obj], type, listener);
        }
    }
    public void DispatchLocalEvent(GameObject obj, string type, object data)
    {
        if (localEventListenerDict.ContainsKey(obj))
        {
            if (localEventListenerDict[obj].ContainsKey(type))
            {
                localEventListenerDict[obj][type](data);
            }
        }
    }

    void TestUnityEvent()
    {
        UnityEvent<object> testEvent = new Event();
        Debug.Log("====Test1====");
        testEvent.AddListener(TestListener1);
        testEvent.Invoke(null);

        Debug.Log("====Test2====");
        testEvent.AddListener(TestListener2);
        testEvent.Invoke(null);

        Debug.Log("====Test3====");
        testEvent.RemoveListener(TestListener1);
        testEvent.Invoke(null);

        Debug.Log("====Test4====");
        testEvent.RemoveListener(TestListener2);
        testEvent.Invoke(null);

        Debug.Log("====Test5====");
        testEvent.AddListener(TestListener1);
        testEvent.Invoke(null);

        Debug.Log("====Test56====");
        testEvent.AddListener(TestListener1);
        testEvent.Invoke(null);
    }

    void TestDelegate()
    {
        Debug.Log("start test delegate!");

        Debug.Log("====Test0====");
        EventListener listener = null;
        EventListener listener0 = new EventListener(TestListener1);
        listener += listener0;
        if (listener != null)
            Debug.Log("listener:" + listener);
        if (listener != null)
            listener(null);

        Debug.Log("====Test1====");
        if (listener != null)
            Debug.Log("listener:"+listener);
        if (listener != null)
            listener(null);

        Debug.Log("====Test2====");
        EventListener listener2 = new EventListener(TestListener2);
        listener += listener2;
        if (listener != null)
            Debug.Log("listener:" + listener);
        if (listener != null)
            listener(null);

        Debug.Log("====Test3====");
        EventListener listener1 = new EventListener(TestListener1);
        listener -= listener1;
        if (listener != null)
            Debug.Log("listener:" + listener);
        if (listener != null)
            listener(null);

        Debug.Log("====Test4====");
        EventListener listener3 = new EventListener(TestListener2);
        listener -= listener3;
        if(listener !=null)
            Debug.Log("listener:" + listener);
        if (listener != null)
            listener(null);

        Debug.Log("====Test5====");
        EventListener listener4 = new EventListener(TestListener1);
        listener += listener4;
        if (listener != null)
            Debug.Log("listener:" + listener);
        if (listener != null)
            listener(null);

        Debug.Log("====Test6====");
        EventListener listener5 = new EventListener(TestListener1);
        listener += listener5;
        if (listener != null)
            Debug.Log("listener:" + listener);
        if (listener != null)
            listener(null);
    }

    void TestListener1(object eventData)
    {
        Debug.Log("TestListener1");
    }

    void TestListener2(object eventData)
    {
        Debug.Log("TestListener2");
    }

    void PrintEventListenersInfo()
    {
        Debug.Log("=====Print all global event listenrs======");
        foreach (KeyValuePair<string, EventListener> pair in globalEventListenerDict)
        {
            Debug.Log("key:" + pair.Key + "  " + "value:" + pair.Value);
        }
        Debug.Log("=====Print all local event listenrs======");
        foreach (KeyValuePair<GameObject, Dictionary<string, EventListener>> pair1 in localEventListenerDict)
        {
            Debug.Log("------------------------------");
            Debug.Log(pair1.Key);
            Debug.Log("----------");

            Dictionary<string, EventListener> listenerDict = pair1.Value;
            foreach (KeyValuePair<string, EventListener> pair2 in listenerDict)
            {
                Debug.Log("key:" + pair2.Key + "  " + "value:" + pair2.Value);
            }
            Debug.Log("------------------------------");
        }
    }
}

