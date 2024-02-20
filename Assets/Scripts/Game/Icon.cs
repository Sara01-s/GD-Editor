using static Unity.Mathematics.math;
using System.Collections;
using UnityEngine;

namespace Game {

	internal sealed class Icon : MonoBehaviour {

		[Header("Graphics")]
		[SerializeField] private SpriteRenderer _primarySpriteRenderer;
		[SerializeField] private SpriteRenderer _secondarySpriteRenderer;
		[SerializeField] private SpriteRenderer _glowSpriteRenderer;
		[SerializeField] private ParticleSystem _iconParticles;

		[Header("Life Cycle")]
		[SerializeField] private GameObject _deathEffect;
		[SerializeField, Min(0)] private int _blinksOnSpawn = 4; // Originally 4
		[SerializeField, Min(0.0f)] private float _blinkDuration = 0.05f;

		private bool _useGlow;

		private void Awake() {
			StartCoroutine(_Blink(_blinksOnSpawn));
		}

		private void SetIconColors(Color primary, Color secondary) {
			_primarySpriteRenderer.color = primary;
			_secondarySpriteRenderer.color = secondary;

			_primarySpriteRenderer.transform.ForAllChilds(child => {
				if (child.TryGetComponent<SpriteRenderer>(out var spriteRenderer)) {
					spriteRenderer.color = primary;
				}
			});

			_secondarySpriteRenderer.transform.ForAllChilds(child => {
				if (child.TryGetComponent<SpriteRenderer>(out var spriteRenderer)) {
					spriteRenderer.color = secondary;
				}
			});
		}

		private void SetGlowColor(Color glowColor, bool useGlow) {
			_glowSpriteRenderer.enabled = useGlow;
			_glowSpriteRenderer.color = glowColor;
			_useGlow = useGlow;
		}

		// ref: https://docs.unity3d.com/ScriptReference/ParticleSystem.MainModule-startColor.html
		private void SetDeathEffectColor(Color deathEffectColor) {
			var particles = _deathEffect.GetComponent<ParticleSystem>().main;
			particles.startColor = deathEffectColor;

			_deathEffect.transform.ForAllChilds(child => {
				if (child.TryGetComponent<ParticleSystem>(out var particleSystem)) {
					var childParticles = particleSystem.main;
					childParticles.startColor = deathEffectColor;
				}
			});
		}

		private void ConfigureParticles(Color effectsColor) {
			_iconParticles.transform.SetParent(transform.parent);
			SetParticlesColor(effectsColor);
		}

		internal void SetColors(Color primary, Color secondary, bool useGlow, Color glowColor = default) {
			SetIconColors(primary, secondary);
			SetGlowColor(glowColor, useGlow);
			ConfigureParticles(secondary);
			SetDeathEffectColor(secondary);
		}

		internal void PlayDeathEffect() {
			Instantiate(_deathEffect, transform);
		}

		internal void ShowIcon() {
			_primarySpriteRenderer.gameObject.SetActive(true);
			_secondarySpriteRenderer.gameObject.SetActive(true);
			_glowSpriteRenderer.gameObject.SetActive(_useGlow);
		}

		internal void HideIcon() {
			_primarySpriteRenderer.gameObject.SetActive(false);
			_secondarySpriteRenderer.gameObject.SetActive(false);
			_glowSpriteRenderer.gameObject.SetActive(false);
		}

		internal void ShowParticles() {
			if (!_iconParticles.isPlaying) {
				_iconParticles.Play();
			}
		}

		internal void HideParticles() {
			if (_iconParticles.isPlaying) {
				_iconParticles.Clear();
				_iconParticles.Stop();
			}
		}

		// ref: https://docs.unity3d.com/ScriptReference/ParticleSystem-colorOverLifetime.html
		private void SetParticlesColor(Color color) {
			var colorOverLifetime = _iconParticles.colorOverLifetime;
			colorOverLifetime.enabled = true;

			var gradient = new Gradient();
			var gradientStartColor = new GradientColorKey(color, 0.0f);
			var gradientEndColor   = new GradientColorKey(color, 1.0f);
			var gradientStartAlpha = new GradientAlphaKey(1.0f, 0.0f);
			var gradientEndAlpha   = new GradientAlphaKey(0.0f, 1.0f);
			var gradientColorKeys  = new GradientColorKey[] { gradientStartColor, gradientEndColor};
			var gradientAlphaKeys  = new GradientAlphaKey[] { gradientStartAlpha, gradientEndAlpha};
			
			gradient.SetKeys(gradientColorKeys, gradientAlphaKeys);

			colorOverLifetime.color = gradient;
		}

		private IEnumerator _Blink(int repeatCount) {
			for (int i = 0; i < repeatCount; i++) {
				HideIcon();
				yield return new WaitForSeconds(_blinkDuration);
				ShowIcon();
				yield return new WaitForSeconds(_blinkDuration);
			}
		}
		
	}
}
