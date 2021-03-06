﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCurrentSubaction : LegacyEditorAction
{
    private SubactionData previousSubaction = null;
    public SubactionData nextSubaction;

    public void init(SubactionData sub)
    {
        nextSubaction = sub;
    }

    public override void execute()
    {
        SubactionData subactionToSet = nextSubaction;
        //If we're re-selecting the current action, unselect it instead (by creating an empty action that can be edited and saved later)
        previousSubaction = LegacyEditorData.instance.currentSubaction;
        if (previousSubaction == nextSubaction)
        {
            Debug.Log("Subactions match, deselecting");
            subactionToSet = null;
        }
        Debug.Log("Current value before processing: " + previousSubaction);
        Debug.Log("Will change to: " + subactionToSet);
        LegacyEditorData.instance.currentSubaction = subactionToSet;
        LegacyEditorData.ChangedSubaction();
    }

    public override void undo()
    {
        Debug.Log("Undoing: " + previousSubaction);
        LegacyEditorData.instance.currentSubaction = previousSubaction;
        LegacyEditorData.ChangedSubaction();
    }
}
