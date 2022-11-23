using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalHandler : MonoBehaviour
{
    [SerializeField] private float ShakeTime;
    [SerializeField] private float RightTime;
    [SerializeField] private float RightSpeed;
    [SerializeField] private float ShakeDuration;
    [SerializeField] private float ShakeAmount;
    private bool LoadNext = false;
    private bool Shaking = false;
    private bool GoingRight = false;
    private int nextScene;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.sceneCountInBuildSettings - 1 == SceneManager.GetActiveScene().buildIndex)
        {
            nextScene = 0;
        }
        else
        {
            nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }

    void Update()
    {
        if (LoadNext)
        {
            SceneManager.LoadScene(nextScene);
        }
        if (Shaking)
        {
            StartCoroutine(Shake());
        }
        if (GoingRight)
        {
            transform.position = new Vector3(transform.position.x + (RightSpeed / 100.0f), transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") return;
        collision.gameObject.SetActive(false);
        StartCoroutine(GoalAnimation());
        
    }
    private IEnumerator GoalAnimation()
    {
        Shaking = true;
        yield return new WaitForSeconds(ShakeTime / 100.0f);
        Shaking = false;
        GoingRight = true;
        yield return new WaitForSeconds(RightTime / 100.0f);
        LoadNext = true;
    }
    private IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;

        float elapsed = 0.0f;

        while (elapsed < ShakeDuration)
        {
            float y = Random.Range(-1f, 1f) * ShakeAmount;

            transform.position = new Vector3(originalPosition.x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
        
    }
}
