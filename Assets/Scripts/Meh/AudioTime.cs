using static Unity.Mathematics.math;
using UnityEngine;

namespace Game {

	internal sealed class AudioTime : MonoBehaviour {


		internal double SmoothedDspTime {
			get {
				if (_fetchCounter <= _windowSize) {
					return AudioSettings.dspTime;
				}
				
				double newTime = Time.unscaledTimeAsDouble * _coefficient1 + _coefficient2;

				newTime = max(newTime, _lastSmoothedDspTime);
				_lastSmoothedDspTime = newTime;

				return newTime;
			} 
		}

		[SerializeField] private AudioSource _testSource;
		[SerializeField, Min(0)] private int _windowSize;

		private const double START_SCHEDULE_WINDOW = 1.0;
		private double _audioDspStartTime;

		private double[] _gameTimeSample;
		private double[] _dspTimeSample;

		private double _lastSmoothedDspTime;
		private double _coefficient1;
		private double _coefficient2;
		private double _slope;

		private int _fetchCounter;

		private void Awake() {
			_audioDspStartTime = AudioSettings.dspTime + START_SCHEDULE_WINDOW;
			_testSource.PlayScheduled(_audioDspStartTime);

			_gameTimeSample = new double[_windowSize];
			_dspTimeSample = new double[_windowSize];		
		}

		private void Update() {
			float currentGameTime = Time.realtimeSinceStartup;
			double currentDspTime = AudioSettings.dspTime;

			int index = _fetchCounter % _gameTimeSample.Length;

			_gameTimeSample[index] = currentGameTime;
			_dspTimeSample[index] = currentDspTime;
			_fetchCounter++;

			if (_fetchCounter <= _windowSize) return;
			
			// Update linear regression
			LinearRegression(_gameTimeSample, _dspTimeSample, out _coefficient1, out _coefficient2, out _slope);
		}

		/// <summary>
        /// Fits a line to a collection of (x,y) points.
        /// </summary>
        /// <param name="xVals">The x-axis values.</param>
        /// <param name="yVals">The y-axis values.</param>
        /// <param name="rSquared">The r^2 value of the line.</param>
        /// <param name="yIntercept">The y-intercept value of the line (i.e. y = ax + b, yIntercept is b).</param>
        /// <param name="slope">The slop of the line (i.e. y = ax + b, slope is a).</param>
        public static void LinearRegression (
            double[] xVals,
            double[] yVals,
            out double rSquared,
            out double yIntercept,
            out double slope)
        {
            if (xVals.Length != yVals.Length) {
                Debug.LogError("Input values should be with the same length.");
            }

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;

            for (int i = 0; i < xVals.Length; i++) {
                double x = xVals[i];
                double y = yVals[i];

                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }

            int count = xVals.Length;
            double ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            double ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

            double rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            double sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = rNumerator / sqrt(rDenom);

            rSquared = dblR * dblR;
            yIntercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }

	}
}