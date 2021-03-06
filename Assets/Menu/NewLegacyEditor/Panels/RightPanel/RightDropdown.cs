﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDropdown : MonoBehaviour {
    private UIPopupList list;
    private Collider coll;

    private string[] rightDropdownOptions;

    // Use this for initialization
    void Awake()
    {
        list = GetComponent<UIPopupList>();
        coll = GetComponent<BoxCollider>();
    }

    void Start()
    {
        rightDropdownOptions = LegacyEditorConstants.RightDropdownOptionsDict[LegacyEditorData.instance.leftDropdown];
        UpdateListItems();
    }

    void UpdateListItems()
    {
        list.items.Clear();
        foreach (string opt in rightDropdownOptions)
        {
            list.items.Add(opt);
        }
        //If the list is only one (or zero I guess) items long, we don't need to actually be able to change dropdowns.
        if (list.items.Count < 2)
        {
            coll.enabled = false;
        } else
        {
            coll.enabled = true;
        }
        UpdateOptionWithoutEvent();
        list.eventReceiver = gameObject;
    }

    void OnModelChanged()
    {
        if (LegacyEditorData.instance.leftDropdownDirty)
        {
            rightDropdownOptions = LegacyEditorConstants.RightDropdownOptionsDict[LegacyEditorData.instance.leftDropdown];
            UpdateListItems();
        }
        if (LegacyEditorData.instance.rightDropdownDirty)
        {
            UpdateOptionWithoutEvent();
        }
    }

    //This is hacky as fuck, isn't it? I'm unsetting the event receiver so I can change this data without firing another change, preventing a double-fire and blowing up the redoList
    public void UpdateOptionWithoutEvent()
    {
        list.eventReceiver = null;
        list.selection = LegacyEditorData.instance.rightDropdown;
        list.eventReceiver = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnChangeDropdown(string selected)
    {
        //Create a message object to have the model execute
        ChangeRightDropdownAction act = ScriptableObject.CreateInstance<ChangeRightDropdownAction>();
        act.init(selected);
        LegacyEditorData.instance.DoAction(act);
    }
}
