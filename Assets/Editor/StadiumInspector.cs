﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StadiumSpawner))]
public class StadiumInspector : Editor {
    public override void OnInspectorGUI() {

        DrawDefaultInspector();
        StadiumSpawner myTarget = (StadiumSpawner)target;

        if (GUILayout.Button("Build Stadium")) {
            myTarget.SpawnStadium();
        }

		if (GUILayout.Button("Clear Stadium")) {
			myTarget.ClearStadium();
		}
    }
}