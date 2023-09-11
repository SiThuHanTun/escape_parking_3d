using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    LevelGenerator generator;
    SerializedObject getTarget;

    void OnEnable()
    {
        generator = (LevelGenerator)target;
        getTarget = new SerializedObject(generator);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        getTarget.Update();

        if(GUILayout.Button("Create Playfield"))
        {
            generator.CreateThePlayfield();
        }

        if(GUILayout.Button("Delete Playfield"))
        {
            generator.DeletePlayfield();
        }

        getTarget.ApplyModifiedProperties();
    }
}
