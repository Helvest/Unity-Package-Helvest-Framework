﻿using UnityEngine;

namespace BackboneFramework
{
	public sealed class DontDestroyOnLoad : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
