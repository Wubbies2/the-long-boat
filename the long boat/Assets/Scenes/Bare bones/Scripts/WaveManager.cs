using UnityEngine;

public static class WaveManager
{
    public static float waveAmplitude = 1.5f;
    public static float waveFrequency = 0.8f;
    public static float waveSpeed = 1.2f;
    public static float baseWaterLevel = 0.4f;
    public static float waveSteepness = 1.0f;

    // Define multiple waves (amplitude, direction, speed, etc.)
    private static readonly Vector2[] directions = new Vector2[]
    {
        new Vector2(1f, 0.2f).normalized,
        new Vector2(0.5f, 1f).normalized,
        new Vector2(-0.7f, 0.3f).normalized,
        new Vector2(-1f, -0.5f).normalized,
    };

    public static float GetWaveHeightAtPosition(Vector3 worldPos)
    {
        float totalHeight = 0f;
        float t = Time.time;

        foreach (Vector2 dir in directions)
        {
            float dot = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), dir);
            float wave = Mathf.Sin(dot * waveFrequency + t * waveSpeed);
            totalHeight += wave;
        }

        totalHeight = (totalHeight / directions.Length) * waveAmplitude;

        return baseWaterLevel + totalHeight;
    }

    // Optional: Get wave displacement (used for visuals)
    public static Vector3 GetWaveDisplacement(Vector3 worldPos)
    {
        Vector3 displacement = Vector3.zero;
        float t = Time.time;

        foreach (Vector2 dir in directions)
        {
            float dot = Vector2.Dot(new Vector2(worldPos.x, worldPos.z), dir);
            float phase = dot * waveFrequency + t * waveSpeed;
            float sin = Mathf.Sin(phase);
            float cos = Mathf.Cos(phase);

            displacement.x += waveSteepness * dir.x * cos;
            displacement.z += waveSteepness * dir.y * cos;
            displacement.y += sin;
        }

        displacement /= directions.Length;
        displacement *= waveAmplitude;

        return new Vector3(displacement.x, displacement.y + baseWaterLevel, displacement.z);
    }
}
