using UnityEngine;

public class JuiceManager : Singleton<JuiceManager>
{
    public static float WaitTime;
    private float gameTime = 0f;

    public void Update()
    {
        gameTime += Time.unscaledDeltaTime;

        if (WaitTime > 0)
        {
            WaitTime -= Time.unscaledDeltaTime;
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public static void PauseFor(float time)
    {
        WaitTime = time;
    }
}