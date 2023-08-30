using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PixelCamera))]
public class PixelCameraEditor: Editor
{
    public override void OnInspectorGUI(){
        PixelCamera pc = (PixelCamera)target;

            // When the inspector is drawn (or any values are changed) re-initialize the render texture
            if (DrawDefaultInspector() || pc.CheckScreenResize()) pc.Init();
    }
}

