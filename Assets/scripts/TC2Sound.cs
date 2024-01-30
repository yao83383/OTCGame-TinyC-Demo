using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TC2Sound : MonoBehaviour
{
	public List<AudioSource> speakers = new List<AudioSource>();

	public List<string> soundNames = new List<string>();

	public List<AudioClip> soundClips = new List<AudioClip>();

	public Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();

	public AudioClip selection;

	public AudioClip newTech;

	public AudioClip delect;

	public AudioClip openMenu;

	private void Start()
	{
		for (int soundIndex = 0; soundIndex < soundClips.Count && soundIndex < soundNames.Count; ++soundIndex)
		{
			Sounds.Add(soundNames[soundIndex], soundClips[soundIndex]);
		}
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
