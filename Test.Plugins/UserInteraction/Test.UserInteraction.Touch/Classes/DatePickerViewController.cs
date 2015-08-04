using System;
using CoreGraphics;
using UIKit;
using Foundation;

namespace Plugins.UserInteraction.Touch
{
	public class DatePickerViewController : UIViewController
	{
	
		readonly DateTime preSelectedDateTime;
		readonly DateTime? minimumDate;
		readonly DateTime? maximumDate;

		bool selectTime = false;
		UIDatePicker datePicker;

		public DateTime SelectedDate
		{
			get
			{
				if (datePicker == null)
				{
					return preSelectedDateTime;
				}

				var date = NSDateToDateTime(datePicker.Date); 
				if (minimumDate.HasValue && date < minimumDate.Value)
				{
					date = minimumDate.Value;
				}

				if (maximumDate.HasValue && date > maximumDate.Value)
				{
					date = maximumDate.Value;
				}

				return date;
			}
		}

		public DatePickerViewController(DateTime selectedDate, bool selectTime, DateTime? minDate = null, DateTime? maxDate = null)
			: base()
		{
			this.preSelectedDateTime = selectedDate;
			this.selectTime = selectTime;
			this.minimumDate = minDate;
			this.maximumDate = maxDate;

			if (selectTime == false)
			{
				preSelectedDateTime = new DateTime(preSelectedDateTime.Year, preSelectedDateTime.Month, preSelectedDateTime.Day, 12, 0, 0);
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			const int spacer = 10;

			datePicker = new UIDatePicker();
			datePicker.Mode = selectTime ? UIDatePickerMode.DateAndTime : UIDatePickerMode.Date;
			datePicker.Date = DateTimeToNSDate(preSelectedDateTime);
			datePicker.Frame = new CGRect(0, spacer, datePicker.Frame.Width, datePicker.Frame.Height);

			if (minimumDate.HasValue)
			{
				var min = DateTimeToNSDate(minimumDate.Value);
				datePicker.MinimumDate = min;
			}

			if (maximumDate.HasValue)
			{
				var max = DateTimeToNSDate(maximumDate.Value);
				datePicker.MaximumDate = max;
			}

			View.Frame = datePicker.Frame;
			View.AddSubview(datePicker);
		}

		DateTime NSDateToDateTime(NSDate date)
		{
			DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0);
			var netDate = reference.AddSeconds(date.SecondsSinceReferenceDate);
			return TimeZone.CurrentTimeZone.ToLocalTime(netDate);
		}

		NSDate DateTimeToNSDate(DateTime date)
		{
			var gmtdate = TimeZone.CurrentTimeZone.ToUniversalTime(date);
			DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0);
			var span = (gmtdate - reference);
			return NSDate.FromTimeIntervalSinceReferenceDate(span.TotalSeconds);
		}
	}
}

