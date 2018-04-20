using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {
    public AudioSource[] sound;
    public AudioClip basementIntro, basementLoop;

    AudioSource music;

    bool swappedToLoop, muted = false;

    MusicGrouper groupedMusic;
    float timer = 0;

	// Use this for initialization
	void Start () {
        sound = GetComponents<AudioSource>();
        music = sound[0];
        groupedMusic = new MusicGrouper(basementIntro,basementLoop);
    }
	
	// Update is called once per frame
	void Update () {
        music.mute = muted;
        Debug.Log(swappedToLoop);
        if(timer >= groupedMusic.intro.length && !swappedToLoop) {
            music.clip = groupedMusic.loop;
            music.Play();
            swappedToLoop = true;
        }
        else timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.M))  muted = !muted;
    }
}

class MusicGrouper {
    public AudioClip intro;
    public AudioClip loop;
    public MusicGrouper(AudioClip _intro, AudioClip _loop) {
        intro = _intro;
        loop = _loop;
    }
}
