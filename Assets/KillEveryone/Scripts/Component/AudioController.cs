using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace KillEveryone
{
	public class AudioController : MonoBehaviour
	{
		private AudioSource audioSource;

		[SerializeField] private AudioClip emptyClip;
		[SerializeField] private AudioClip reloadClip;
		[SerializeField] private AudioClip fireClip;
		private void Start()
		{
			audioSource = GetComponent<AudioSource>();

			//EventManager.Reload += OnReload;
		}
		public void PlayReload()
		{
			audioSource.PlayOneShot(reloadClip);
		}
		public void PlayShoot()
		{
			audioSource.PlayOneShot(fireClip);
		}
		public void PlayEmpty()
		{
			audioSource.PlayOneShot(emptyClip);
		}
		
	}
}
