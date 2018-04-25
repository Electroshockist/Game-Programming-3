using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {
    public AudioClip basementIntro, basementLoop;

    public static AudioSource music;

    bool swappedToLoop, muted = false;

    MusicGrouper groupedMusic;
    float totalTimesamples = 0;

	// Use this for initialization
	void Start () {
        music = transform.GetChild(1).GetComponent<AudioSource>();
        groupedMusic = new MusicGrouper(basementIntro,basementLoop);
        music.clip = groupedMusic.intro;
        music.Play();

        Debug.Log(groupedMusic.intro.samples);
    }
	
	// Update is called once per frame
	void Update () {        
        music.mute = muted;

        if (groupedMusic.intro.samples >= groupedMusic.intro.samples && !swappedToLoop) {
            music.clip = groupedMusic.loop;
            music.Play();
            swappedToLoop = true;
        }
        else if (!swappedToLoop) totalTimesamples += groupedMusic.intro.samples;


        //toggles mute
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
