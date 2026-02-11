using System.Runtime.CompilerServices;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float slowMotionTimeScale;

    private float startTimeScale;
    private float startFixedDeltaTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartSlowMotion();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopSlowMotion();
        }
    }
    private void StartSlowMotion()
    {
        Time.timeScale = slowMotionTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimeScale;
    }
    private void StopSlowMotion()
    {
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

}
