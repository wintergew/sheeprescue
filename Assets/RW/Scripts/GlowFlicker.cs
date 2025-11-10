using UnityEngine;

public class GlowFlicker : MonoBehaviour
{
    public Color glowColor = Color.yellow;
    public float glowSpeed = 2f;
    public float baseEmission = 0.5f;
    public float glowIntensity = 2f;

    Renderer rend;
    Material mat;
    Color originalColor;

    void Awake()
    {
        rend = GetComponent<Renderer>() ?? GetComponentInChildren<Renderer>();
        if (rend)
        {
            mat = rend.material;
            if (mat.HasProperty("_EmissionColor"))
                originalColor = mat.GetColor("_EmissionColor");
        }
    }

    void OnEnable()
    {
        if (mat) mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (!mat) return;
        float emission = baseEmission + Mathf.PingPong(Time.time * glowSpeed, glowIntensity);
        mat.SetColor("_EmissionColor", glowColor * emission);
    }

    void OnDisable()
    {
        if (!mat) return;
        mat.SetColor("_EmissionColor", originalColor);
        mat.DisableKeyword("_EMISSION");
    }
}
