using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;

    private int currentBrickCount;
    private int totalBrickCount;

    public float screenShakeDuration = 0.5f;
    public float screenShakeStrength = 1f;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        if(currentBrickCount == 1){
            AudioManager.Instance.lastHit(); //Increase volume if its the last brick
            screenShakeDuration = 1.0f;
            screenShakeStrength = 5.0f;
        }
        
        AudioManager.Instance.PlaySFX("Vine");//play sound on brick break
        AudioManager.Instance.decreasePitch();
        
        // implement particle effect here
        // add camera shake here
        
        CameraShake.Shake(screenShakeDuration, screenShakeStrength);
        currentBrickCount--;
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");

        if(currentBrickCount == 0){
            AudioManager.Instance.resetVolume();
            AudioManager.Instance.resetPitch();
            StartCoroutine(yippeeAfterDelay(1)); //ensures delay so sounds aren't playing over eachother
            StartCoroutine(LoadNextSceneAfterDelay(2)); //ensure all sound gets to play before level change
        } 
    }

    private IEnumerator yippeeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        float temp = AudioManager.Instance.getPitch();
        AudioManager.Instance.setPitch = 1.0f;
        AudioManager.Instance.PlaySFX("Yippee");
        AudioManager.Instance.setPitch = temp;
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneHandler.Instance.LoadNextScene();
    }

    public void KillBall()
    {
        maxLives--;
        float temp = AudioManager.Instance.getPitch();
        AudioManager.Instance.setPitch = 1.0f;
        AudioManager.Instance.PlaySFX("Kaboom"); //Death sound
        AudioManager.Instance.setPitch = temp;
        
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();
    }
}
