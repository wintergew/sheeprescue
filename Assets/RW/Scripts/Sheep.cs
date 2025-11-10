using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed; // 1
    public float gotHayDestroyDelay; // 2
    private bool hitByHay; // 3
    private bool hasDropped;

    public float dropDestroyDelay; // 1
    private Collider myCollider; // 2
    private Rigidbody myRigidbody; // 3
    private SheepSpawner sheepSpawner;

    public float heartOffset; // 1
    public GameObject heartPrefab; // 2

    public bool isSpecial;


    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();

        if (isSpecial)
        {
            Renderer renderer = GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                GlowFlicker gf = renderer.gameObject.GetComponent<GlowFlicker>();
                if (gf == null) gf = renderer.gameObject.AddComponent<GlowFlicker>();
                gf.enabled = true;
            }
        }

    }

    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other) // 1
    {
        if (other.CompareTag("Hay") && !hitByHay) // 2
        {
            Destroy(other.gameObject); // 3
            HitByHay(); // 4
        }

        else if (other.CompareTag("DropSheep") && !hasDropped)
        {
            sheepSpawner.RemoveSheepFromList(gameObject);
            Drop();
        }


    }

    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true;
        runSpeed = 0;

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;

        Destroy(gameObject, gotHayDestroyDelay);
        SoundManager.Instance.PlaySheepHitClip();
        GameStateManager.Instance.SavedSheep();

        if (isSpecial)
        {
            PowerUpManager.Instance.ActivatePowerUp();
        }
    }
    private void Drop()
    {
        hasDropped = true;
        sheepSpawner.RemoveSheepFromList(gameObject);
        myRigidbody.isKinematic = false; // 1
        myCollider.isTrigger = false; // 2
        Destroy(gameObject, dropDestroyDelay); // 3
        SoundManager.Instance.PlaySheepDroppedClip();
        GameStateManager.Instance.DroppedSheep();
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}
