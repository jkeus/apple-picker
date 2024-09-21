using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject applePrefab;
    public GameObject pearPrefab;

    public float speed = 1f; // Initial speed of tree movement
    public float leftAndRightEdge = 10f;
    public float changeDirChance = 0.1f;
    public float fruitDropDelay = 1f; // Initial fruit drop delay
    public float pearChance = 0.2f; // Initial pear drop chance

    public static float sharedFallSpeed = 5f; // Shared initial speed for all falling objects
    public float fallAcceleration = 0.5f; // Slower downward acceleration
    public float maxFallSpeed = 50f; // Maximum fall speed cap
    public float maxSpeed = 25f; // Maximum tree speed
    public float minDropDelay = 0.3f; // Minimum fruit drop delay
    public float maxPearChance = 0.4f; // Maximum pear chance

    // Start is called before the first frame update
    void Start()
    {
        // Start the processes for acceleration and changes over time
        StartCoroutine(AccelerateSharedFallSpeed());
        StartCoroutine(IncreaseTreeSpeed());
        StartCoroutine(DecreaseFruitDropDelay());
        StartCoroutine(IncreasePearRate());

        Invoke("DropFruit", fruitDropDelay);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    void FixedUpdate()
    {
        if (Random.value < changeDirChance)
        {
            speed *= -1;
        }
    }

    void DropFruit()
    {
        if (Random.value < pearChance)
        {
            DropPear();
        }
        else
        {
            DropApple();
        }

        // Drop fruit with updated delay
        Invoke("DropFruit", fruitDropDelay);
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;

        Rigidbody rb = apple.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply shared fall speed to all objects
            rb.velocity = new Vector3(0, -sharedFallSpeed, 0);
        }
    }

    void DropPear()
    {
        GameObject pear = Instantiate<GameObject>(pearPrefab);
        pear.transform.position = transform.position;

        Rigidbody rb = pear.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply shared fall speed to all objects
            rb.velocity = new Vector3(0, -sharedFallSpeed, 0);
        }
    }

    // Coroutine to gradually increase the shared fall speed over time
    IEnumerator AccelerateSharedFallSpeed()
    {
        while (sharedFallSpeed < maxFallSpeed)
        {
            // Gradually increase the shared fall speed at a slower rate
            sharedFallSpeed += fallAcceleration * Time.deltaTime;

            // Debug: Track the shared fall speed
            Debug.Log($"Shared fall speed: {sharedFallSpeed}");

            yield return new WaitForFixedUpdate(); // Wait for the next physics update
        }
    }

    // Coroutine to gradually increase the tree's speed over time, with a cap of maxSpeed
    IEnumerator IncreaseTreeSpeed()
    {
        while (speed < maxSpeed)
        {
            // Increase the tree speed gradually
            speed += 0.1f * Time.deltaTime;

            // Cap the speed
            speed = Mathf.Min(speed, maxSpeed);

            yield return new WaitForFixedUpdate(); // Wait for the next physics update
        }
    }

    // Coroutine to gradually decrease the fruit drop delay over time, with a minimum of minDropDelay
    IEnumerator DecreaseFruitDropDelay()
    {
        while (fruitDropDelay > minDropDelay)
        {
            // Decrease the fruit drop delay gradually
            fruitDropDelay -= 0.01f * Time.deltaTime;

            // Cap the drop delay to avoid going below minDropDelay
            fruitDropDelay = Mathf.Max(fruitDropDelay, minDropDelay);

            yield return new WaitForFixedUpdate(); // Wait for the next physics update
        }
    }

    // Coroutine to gradually increase the pear drop rate, with a maximum of maxPearChance
    IEnumerator IncreasePearRate()
    {
        while (pearChance < maxPearChance)
        {
            // Increase the pear chance gradually
            pearChance += 0.01f * Time.deltaTime;

            // Cap the pear chance to avoid going above maxPearChance
            pearChance = Mathf.Min(pearChance, maxPearChance);

            yield return new WaitForFixedUpdate(); // Wait for the next physics update
        }
    }
}
