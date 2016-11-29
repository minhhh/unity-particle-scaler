using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ShurikenScaler : EditorWindow
{
    [MenuItem ("Window/ShurikenScaler")]
    public static void OpenWindow ()
    {
        GetWindow<ShurikenScaler> (false, "Shuriken Particle Scaler", true);
    }

    ParticleSystem[] particleSystems = null;

    float oldScale = 1.0f;
    float currentScale = 1.0f;
    int currentInstanceId = 0;
    static Dictionary<int, float> currentScales = new Dictionary<int, float> ();

    void OnEnable ()
    {
//        Debug.Log (GetType ().Name + "::OnEnabled");
        Init ();
    }

    void OnDisable ()
    {
//        Debug.Log (GetType ().Name + "::OnDisable");
    }

    void OnSelectionChange ()
    {
//        Debug.Log (GetType ().Name + "::OnSelectionChange");
        Init ();
        Repaint ();
    }

    void Init ()
    {
        particleSystems = null;

        if (Selection.activeGameObject == null)
            return;

        particleSystems = Selection.activeGameObject.GetComponentsInChildren <ParticleSystem> ();

        currentInstanceId = Selection.activeGameObject.GetInstanceID ();
        if (!currentScales.ContainsKey (currentInstanceId)) {
            currentScales [currentInstanceId] = 1.0f;
        }
        currentScale = currentScales [currentInstanceId];
    }

    void OnGUI ()
    {
        if (particleSystems == null || particleSystems.Length == 0) {
            EditorGUILayout.HelpBox ("Please select an object with ParticleSystem components in itself or its children", MessageType.Error);
            return;
        }
            

        EditorGUILayout.Space ();

        EditorGUILayout.BeginHorizontal ();

        EditorGUI.BeginChangeCheck ();
        oldScale = currentScale;
        currentScale = EditorGUILayout.Slider (currentScale, 0.1f, 100);

        if (EditorGUI.EndChangeCheck ()) {
            DoScale ();
        }

        if (GUILayout.Button ("Play")) {
            foreach (var ps in particleSystems) {
                ps.Play (true);
            }
        }
        if (GUILayout.Button ("Pause")) {
            foreach (var ps in particleSystems) {
                ps.Pause (true);
            }
        }
        if (GUILayout.Button ("Stop")) {
            foreach (var ps in particleSystems) {
                ps.Stop (true);
            }
        }
        EditorGUILayout.EndHorizontal ();

        if (GUILayout.Button ("Reset")) {
            currentScale = 1.0f;
            DoScale ();
        }

        if (GUI.changed) {
            SceneView.RepaintAll ();
        }
    }

    void DoScale ()
    {
        currentScales [currentInstanceId] = currentScale;

        var go = Selection.activeGameObject;
        foreach (var ps in particleSystems) {
            ps.Stop ();
            ScaleParticleSystem (go, ps);
            ps.Play ();
        }
    }

    void ScaleParticleSystem (GameObject go, ParticleSystem ps)
    {
        var multiplier = currentScale / oldScale;

        if (go != ps.gameObject) {
            ps.gameObject.transform.localPosition *= multiplier;
        }

        ps.startSize *= multiplier;
        ps.gravityModifier *= multiplier;
        ps.startSpeed *= multiplier;

        SerializedObject so = new SerializedObject (ps);

        so.FindProperty ("ShapeModule.radius").floatValue *= multiplier;
        so.FindProperty ("ShapeModule.length").floatValue *= multiplier;
        so.FindProperty ("ShapeModule.boxX").floatValue *= multiplier;
        so.FindProperty ("ShapeModule.boxY").floatValue *= multiplier;
        so.FindProperty ("ShapeModule.boxZ").floatValue *= multiplier;
        so.FindProperty ("VelocityModule.x.scalar").floatValue *= multiplier;
        so.FindProperty ("VelocityModule.y.scalar").floatValue *= multiplier;
        so.FindProperty ("VelocityModule.z.scalar").floatValue *= multiplier;
        ScaleCurve (so.FindProperty ("VelocityModule.x.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("VelocityModule.x.maxCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("VelocityModule.y.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("VelocityModule.y.maxCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("VelocityModule.z.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("VelocityModule.z.maxCurve").animationCurveValue, multiplier);
        so.FindProperty ("ClampVelocityModule.magnitude.scalar").floatValue *= multiplier;
        so.FindProperty ("ClampVelocityModule.x.scalar").floatValue *= multiplier;
        so.FindProperty ("ClampVelocityModule.y.scalar").floatValue *= multiplier;
        so.FindProperty ("ClampVelocityModule.z.scalar").floatValue *= multiplier;
        ScaleCurve (so.FindProperty ("ClampVelocityModule.x.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ClampVelocityModule.x.maxCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ClampVelocityModule.y.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ClampVelocityModule.y.maxCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ClampVelocityModule.z.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ClampVelocityModule.z.maxCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ClampVelocityModule.magnitude.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ClampVelocityModule.magnitude.maxCurve").animationCurveValue, multiplier);
        so.FindProperty ("ForceModule.x.scalar").floatValue *= multiplier;
        so.FindProperty ("ForceModule.y.scalar").floatValue *= multiplier;
        so.FindProperty ("ForceModule.z.scalar").floatValue *= multiplier;
        ScaleCurve (so.FindProperty ("ForceModule.x.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ForceModule.x.maxCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ForceModule.y.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ForceModule.y.maxCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ForceModule.z.minCurve").animationCurveValue, multiplier);
        ScaleCurve (so.FindProperty ("ForceModule.z.maxCurve").animationCurveValue, multiplier);
        so.FindProperty ("ColorBySpeedModule.range").vector2Value *= multiplier;
        so.FindProperty ("SizeBySpeedModule.range").vector2Value *= multiplier;
        so.FindProperty ("RotationBySpeedModule.range").vector2Value *= multiplier;

        so.ApplyModifiedProperties ();
    }

    void ScaleCurve (AnimationCurve curve, float multiplier)
    {
        for (int i = 0; i < curve.keys.Length; i++) {
            curve.keys [i].value *= multiplier;
        }
    }

}
