using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateMove
{
    DOWN = 1, 
    LEFT = 4,
    RIGHT = 7, 
    UP = 10
}
public class SpiderAnimation : MonoBehaviour
{
    public SpiderSkin[] spriteResouces; 
    private int currentSkin;
    public SpriteRenderer visualSpriteRenderer;
    [SerializeField]
    private StateMove stateMove;
    private float currentFrame = 0;
    private int NumberPerState = 3;

    private void OnEnable()
    {
        currentSkin = Random.Range(0, spriteResouces.Length);
    }
    public void ChangeState(StateMove stateMove)
    {
        if (this.stateMove != stateMove)
            this.stateMove = stateMove;
    }

    private void Update()
    {
        UpdateStateAnimation();
    }

    private void UpdateStateAnimation()
    {
        currentFrame += Time.deltaTime * 10;
        if (currentFrame > 3)
        {
            currentFrame = 0;
        }
        int currentSprite = (int)currentFrame + (int)stateMove;
        
        visualSpriteRenderer.sprite = spriteResouces[currentSkin].spriteResources[currentSprite - 1];
    }
}
