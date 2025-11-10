using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    public float increasedShootRateMultiplier = 2f;
    public float increasedSpeedMultiplier = 1.5f; 
    public float powerUpDuration = 10f; 

    private HayMachine hayMachine;
    private float originalShootInterval;
    private float originalMovementSpeed;

    private bool isPowerUpActive = false;

    void Awake()
    {
        Instance = this;
        hayMachine = FindFirstObjectByType<HayMachine>();
        originalShootInterval = hayMachine.shootInterval;
        originalMovementSpeed = hayMachine.movementSpeed;
    }

    private IEnumerator ActivatePowerUps()
    {
        isPowerUpActive = true;

        hayMachine.shootInterval /= increasedShootRateMultiplier;
        hayMachine.movementSpeed *= increasedSpeedMultiplier;

        EnableGlow(true);

        yield return new WaitForSeconds(powerUpDuration);

        hayMachine.shootInterval = originalShootInterval;
        hayMachine.movementSpeed = originalMovementSpeed;
        EnableGlow(false);
    }

    public void ActivatePowerUp()
    {
        if (!isPowerUpActive)
        {
            StartCoroutine(ActivatePowerUps());
        }
    }

    private void EnableGlow(bool enable)
    {
        MeshRenderer machineRenderer = hayMachine.modelParent.GetComponentInChildren<MeshRenderer>();
        if (machineRenderer != null)
        {
            GlowFlicker gf = machineRenderer.GetComponent<GlowFlicker>();
            if (enable)
            {
                if (gf == null) gf = machineRenderer.gameObject.AddComponent<GlowFlicker>();
                gf.enabled = true;
            }
            else
            {
                if (gf != null) gf.enabled = false;
            }
        }
    }

}