using System.Collections;
using System;

namespace Game {

	/// <summary>
    /// Represents a timer that triggers an event when it ends.
    /// </summary>
    public sealed class Timer {

		/// <summary>
        /// Occurs when the timer ends.
        /// </summary>
        public event Action OnTimerEnd;

		/// <summary>
        /// Gets the remaining seconds on the timer.
        /// </summary>
        public float RemainingSeconds { get; private set; }

        public float DurationSeconds { get; private set; }
		

		private readonly bool _resetOnCompletion;

		/// <summary>
        /// Creates a new timer with the specified duration.
        /// </summary>
        /// <param name="durationSeconds">The duration of the timer in seconds.</param>
        public Timer(float durationSeconds, bool resetOnCompletion = false) {
            RemainingSeconds = durationSeconds;
			DurationSeconds = durationSeconds;

			_resetOnCompletion = resetOnCompletion;
        }

		/// <summary>
        /// Decreases the remaining seconds on the timer by the specified amount.
        /// </summary>
        /// <param name="deltaTime">The amount of time to decrease from the timer.</param>
        public void Tick(float deltaTime) {
            if (RemainingSeconds <= 0.0f) return;

            RemainingSeconds -= deltaTime;

            CheckForTimerEnd();
        }

        public IEnumerator _StartTicking() {
			while (RemainingSeconds <= DurationSeconds) {
            	RemainingSeconds -= UnityEngine.Time.deltaTime;
            	CheckForTimerEnd();
				yield return null;
			}
        }

        private void CheckForTimerEnd() {
            if (RemainingSeconds > 0.0f) return;

            RemainingSeconds = 0.0f;

			if (_resetOnCompletion) {
				RemainingSeconds = DurationSeconds;
			}
			
            OnTimerEnd?.Invoke();
        }

    }
}