using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public float movementSpeed = 30;
    public float boundary = 22;

    public GameObject hayBalePrefab; 
    public Transform haySpawnpoint; 
    public float shootInterval; 
    private float shootTimer;

    public Transform modelParent;
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    void Start()
    {
        LoadModel();
    }

    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject);
        switch (GameSettings.hayMachineColor)
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;
            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;
            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }


    void Update()
    {
        UpdateMovement();
        UpdateShooting();
    }

    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 

        if (horizontalInput < 0 && transform.position.x > - boundary) 
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < boundary) 
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }

    }

    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();
    }
    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime; // 1

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space)) // 2
        {
            shootTimer = shootInterval; // 3
            ShootHay(); // 4
        }
    }

}
