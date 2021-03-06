﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeLayout : MonoBehaviour, LegacyDataViewer {
    public GameObject data_row_prefab;

    public GameObject new_data_object;

    private UIGrid grid;
    private List<GameObject> data_rows = new List<GameObject>();

    void Start()
    {
        grid = GetComponent<UIGrid>();
        if (LegacyEditor.editor.current_fighter != null) FighterChanged(LegacyEditor.editor.current_fighter);
        grid.Reposition();
    }

    void RemoveData()
    {
        foreach (GameObject action_row in data_rows)
        {
            NGUITools.Destroy(action_row);
        }
    }

    public void FighterChanged(FighterInfo info)
    {
        RemoveData();
        foreach (VarData data in info.variables)
        {
            AttributeDataRow dataRow = InstantiateRow(data);
            if (AbstractFighter.DefaultStats.ContainsKey(data.name))
            {
                dataRow.SetRemovable(false);
            }
        }
        new_data_object.transform.SetAsLastSibling();
        grid.Reposition();
    }

    private AttributeDataRow InstantiateRow(VarData data)
    {
        GameObject go = NGUITools.AddChild(gameObject, data_row_prefab);
        AttributeDataRow attr = go.GetComponent<AttributeDataRow>();
        attr.variable_name = data.name;
        attr.vardata = data;
        data_rows.Add(go);
        return attr;
    }

    public void WindowChanged(string window_name) { }
    public void SubWindowChanged(string sub_window_name) { }
    public void ActionFileChanged(ActionFile actions) { }
    public void SelectedActionChanged(DynamicAction action) { }
    public void CategoryChanged(string category_name) { }

    public void OnEnable()
    {
        LegacyEditor.OnFighterChanged += FighterChanged;
    }

    public void OnDisable()
    {
        LegacyEditor.OnFighterChanged -= FighterChanged;
    }
}