﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DI_System : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageIndicator indicatorPrefab = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private Transform player = null;

    private Dictionary<Transform, DamageIndicator> Indicators = new Dictionary<Transform, DamageIndicator>();

    public Action<Transform> CreateIndicator = delegate {};
    public Func<Transform, bool> CheckIfObjectInSight = null;

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }
    private void OnDisable()
    {
        CreateIndicator -= Create;
        CheckIfObjectInSight -= InSight;
    }
    void Create(Transform target)
    {   
        if(Indicators.ContainsKey(target))
        {
            Indicators[target].Restart();
            return;
        }
        DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
        newIndicator.Register(target,player,new Action( () => { Indicators.Remove(target); }));

        Indicators.Add(target, newIndicator);
    }
    bool InSight(Transform t)
    {
        return true;
    }
}