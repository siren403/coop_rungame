using UnityEngine;
using System.Collections;

public class DistanceCulling : MonoBehaviour {
public float cullingDistance;
public SingleUnityLayer layer;
    void Start() {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];

		distances[layer.LayerIndex] = cullingDistance;
        camera.layerCullDistances = distances;
    }
}