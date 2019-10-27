﻿using UIKit;
using Uno.System.Profile;
using UnoDeviceForm = Uno.System.Profile.DeviceForm;

namespace Windows.System.Profile
{
	public partial class AnalyticsInfo
	{
		private static UnoDeviceForm GetDeviceForm()
		{
			switch (UIDevice.CurrentDevice.UserInterfaceIdiom)
			{
				case UIUserInterfaceIdiom.Phone:
					return UnoDeviceForm.Mobile;
				case UIUserInterfaceIdiom.Pad:
					return UnoDeviceForm.Tablet;
				case UIUserInterfaceIdiom.TV:
					return UnoDeviceForm.Television;
				case UIUserInterfaceIdiom.CarPlay:
					return UnoDeviceForm.Car;
				default:
					return UnoDeviceForm.Unknown;
			}
		}
	}
}
