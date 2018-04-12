using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionmarkBlock : MonoBehaviour {

    public SpriteRenderer entityRenderer;

    public Sprite[] QuestionBlock;

    int blockFrame;

    void BlockAnim() {
        entityRenderer.sprite = QuestionBlock[blockFrame];
        blockFrame++;
        if (blockFrame >= QuestionBlock.Length) {
            blockFrame = 0;
        }
        Invoke("BlockAnim", 0.12f);
    }
    // Use this for initialization
    void Start () {
        BlockAnim();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
