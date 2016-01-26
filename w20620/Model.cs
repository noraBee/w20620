using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows.Media;
using w20620.Annotations;

namespace w20620
{
	public class Model : INotifyPropertyChanged
	{
		private double _percent;
		private bool _isScreenWatching = true;
		private DateTime _startDate = DateTime.Now;

		public event PropertyChangedEventHandler PropertyChanged;

		private readonly Timer _timer;
		private static readonly Color ScreenColor = Color.FromArgb(120, 0, 170, 0);
		private static readonly Color FarColor = Color.FromArgb(120, 170, 0, 0);

		private TimeSpan _farTime = TimeSpan.FromSeconds(20);
		private TimeSpan _screenTime = TimeSpan.FromMinutes(20);
		private TimeSpan _animationTime = TimeSpan.FromSeconds(1);

		public TimeSpan ScreenTime
		{
			get { return _screenTime; }
			set
			{
				if (value.Equals(_screenTime)) return;
				_screenTime = value;
				OnPropertyChanged();
			}
		}

		public TimeSpan AnimationTime
		{
			get { return _animationTime; }
			set
			{
				if (value.Equals(_animationTime)) return;
				_animationTime = value;
				OnPropertyChanged();
			}
		}

		public TimeSpan FarTime
		{
			get { return _farTime; }
			set
			{
				if (value.Equals(_farTime)) return;
				_farTime = value;
				OnPropertyChanged();
			}
		}

		public Model()
		{
			_timer = new Timer();
			_timer.Elapsed += (s, e) =>
			{
				lock (_timer)
				{
					var now = DateTime.Now;
					if (IsScreenWatching)
					{
						if (StartDate.Add(ScreenTime) <= now)
						{
							IsScreenWatching = false;
							StartDate = now;
							_timer.Interval = (int)(FarTime.TotalMilliseconds / 360.0);
						}
						Percent = ((StartDate.Add(ScreenTime) - now).TotalSeconds / ScreenTime.TotalSeconds) * 100.0;
					}
					else
					{
						var farTime = FarTime.Add(AnimationTime).Add(AnimationTime);
						if (StartDate.Add(farTime) <= now)
						{
							IsScreenWatching = true;
							StartDate = now;
							_timer.Interval = (int)(ScreenTime.TotalMilliseconds / 360.0);
						}
						Percent = ((StartDate.Add(farTime) - now).TotalSeconds / farTime.TotalSeconds) * 100.0;
					}
				}
			};
			_timer.Interval = 55;
			IsScreenWatching = true;
			_timer.Start();
		}

		public Brush Brush
		{
			get
			{
				return IsScreenWatching ? new SolidColorBrush(ScreenColor) : new SolidColorBrush(FarColor);
			}
		}

		public DateTime StartDate
		{
			get { return _startDate; }
			set
			{
				if (value.Equals(_startDate)) return;
				_startDate = value;
				OnPropertyChanged();
			}
		}

		public double Percent
		{
			get { return _percent; }
			set
			{
				if (value.Equals(_percent)) return;
				_percent = value;
				OnPropertyChanged();
			}
		}

		public bool IsScreenWatching
		{
			get { return _isScreenWatching; }
			set
			{
				if (value.Equals(_isScreenWatching)) return;
				_isScreenWatching = value;
				OnPropertyChanged();
				OnPropertyChanged("Brush");
			}
		}

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}