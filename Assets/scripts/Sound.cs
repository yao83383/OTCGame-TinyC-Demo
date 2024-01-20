using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
	public List<AudioSource> speakers = new List<AudioSource>();

	public List<AudioClip> moveSounds = new List<AudioClip>();

	public List<AudioClip> skillSounds = new List<AudioClip>();

	public List<AudioClip> otherSounds = new List<AudioClip>();

	public AudioClip selection;

	public AudioClip newTech;

	public AudioClip delect;

	public AudioClip openMenu;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void PlaySound(AudioClip ac)
	{
		AudioSource audioSource = speakers[0];
		audioSource.clip = ac;
		audioSource.Play();
		speakers.Remove(audioSource);
		speakers.Add(audioSource);
	}
}
