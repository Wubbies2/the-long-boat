using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterWave : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveLength = 2f;
    public float waveSpeed = 1f;

    private Vector3[] baseVertices;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
    }

    void Update()
    {
        Vector3[] vertices = new Vector3[baseVertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];
            float wave = Mathf.Sin(Time.time * waveSpeed + vertex.x + vertex.z) * waveHeight;
            vertex.y = wave;
            vertices[i] = vertex;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    public float GetWaveHeightAtPosition(Vector3 position)
    {
        return Mathf.Sin(Time.time * waveSpeed + position.x + position.z) * waveHeight;
    }
}
