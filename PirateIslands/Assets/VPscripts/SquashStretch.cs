using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashStretch : MonoBehaviour
{

    private SpriteRenderer spriteR;
    private Sprite[] sprites;
    private Sprite[] idleSprites;
    private Sprite[] walkSprites;
    public int spriteIndex;
    //string spriteName = ;
    float timer = 0f;
    [Range(0.5f,4f)]
    public float animSpeed = 1f;
    int lastFrame = 0;
    [Range(0.01f,1f)]
    public float squetchAmount = 0.1f;
    [Range(0.0f,1.0f)]
    public float squetchTime = 0.5f;
    float squetchCurrentTime = 0f;
    float squetchX = 0f;
    float squetchY = 0f;
    [Header("Sound Stuff")]
    public bool stepSound = true;
    AudioSource audioData;
    [Header("Sprite Info")]
    public bool idle = true;
    public Texture idleAnim;
    public Texture walkAnim;


    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        idleSprites = Resources.LoadAll<Sprite>(idleAnim.name);//spriteR.sprite.texture.name);
        walkSprites = Resources.LoadAll<Sprite>(walkAnim.name);//spriteR.sprite.texture.name);
        sprites = idleSprites;
    }

    void Squetch(){
        // start squetch time
        squetchCurrentTime = 0f;
        //Debug.Log("squetch was called");
        if (stepSound) {
            audioData = GetComponent<AudioSource>();
            audioData.Play(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (idle){ // change animation
            sprites = idleSprites;
        } else {
            sprites = walkSprites;
        }
        timer += Time.deltaTime * animSpeed;
        if (squetchCurrentTime<1f){
            squetchCurrentTime += Time.deltaTime * animSpeed / squetchTime;
            squetchCurrentTime = Mathf.Clamp(squetchCurrentTime, 0, 1);
            // squetchX = Easing.Elastic.In(squetchCurrentTime) * squetchAmount;
            // squetchY = Easing.Elastic.In(squetchCurrentTime) * squetchAmount;
            squetchX = Easing.Quadratic.In(squetchCurrentTime) * squetchAmount;
            squetchY = Easing.Quadratic.In(1-squetchCurrentTime) * squetchAmount;
            gameObject.transform.localScale = new Vector3(1+squetchX,1+squetchY,1f);
        }
        
        spriteIndex = Mathf.RoundToInt(timer%(sprites.Length-1));
        
        spriteR.sprite = sprites[spriteIndex];

        // squash / stretch
        if (lastFrame != spriteIndex){
            Squetch();
        } 
        lastFrame = spriteIndex;
    }
}
