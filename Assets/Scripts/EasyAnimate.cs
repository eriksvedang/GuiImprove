using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EasyAnimate<T>
{
    public delegate void OnComplete();
    public delegate T InterpolationSampler(T pStart, T pEnd, float pValue);
    public delegate void SetValue(T pValue);
    public class EasyAnimState
    {
        public EasyAnimState(float pStartTime, float pEndTime, T pStartValue, T pEndValue, InterpolationSampler pSampler, SetValue pSetterFunction, OnComplete pCompleteCallback)
        {
            sampler = pSampler;
            setter = pSetterFunction;
            startTime = pStartTime;
            endTime = pEndTime;
            startValue = pStartValue;
            endValue = pEndValue;
            onComplete = pCompleteCallback;
        }
        public EasyAnimState(float pStartTime, float pEndTime, T pStartValue, T pEndValue, InterpolationSampler pSampler, SetValue pSetterFunction)
        {
            sampler = pSampler;
            setter = pSetterFunction;
            startTime = pStartTime;
            endTime = pEndTime;
            startValue = pStartValue;
            endValue = pEndValue;
        }
        public float startTime;
        public float endTime;
        public T startValue;
        public T endValue;
        public InterpolationSampler sampler;
        public SetValue setter;
        public EasyAnimState nextState;
        public OnComplete onComplete;
        public EasyAnimState Then(T pValue, float pAnimationTime)
        {
            nextState = new EasyAnimate<T>.EasyAnimState(endTime, endTime + pAnimationTime, endValue, pValue, sampler, setter);
            return nextState;
        }
        internal void Complete()
        {
            setter(endValue);
            if (onComplete != null)
            {
                onComplete();
            }
        }

    }
    static EasyAnimate<T> _instance = null;
    public static EasyAnimate<T> instance 
    {
        get
        {
            if (_instance == null)
                _instance = new EasyAnimate<T>();
            return _instance; 
        }
    }

    private Dictionary<int, EasyAnimState> _objects = new Dictionary<int, EasyAnimState>();
    
    public void Update()
    {
        foreach (KeyValuePair<int, EasyAnimState> kv in _objects.ToArray())
        {
            int key = kv.Key;
            EasyAnimState e = kv.Value;
            if (e.endTime < Time.time)
            {
                e.Complete();
                _objects.Remove(key);
            }
            else
            {
                float t = Mathf.Max((Time.time - e.startTime) / (e.endTime - e.startTime), 0f);
                e.setter(e.sampler(e.startValue, e.endValue, t));
            }
        }
    }

    public void Register(object o, string pChannel, EasyAnimState a)
    {
        int hash = o.GetHashCode() ^ pChannel.GetHashCode();
        if (!_objects.ContainsKey(hash))
        {
            _objects.Add(hash, a);
        }
        else
        {
            _objects[hash] = a;
        }
        
    }
}

