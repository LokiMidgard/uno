﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SamplesApp.UITests.TestFramework;
using Uno.UITest.Helpers;
using Uno.UITest.Helpers.Queries;

namespace SamplesApp.UITests.Windows_UI_Xaml_Input
{
	public class Tapped_Tests : SampleControlUITestBase
	{
		private const string _xamlTestPage = "UITests.Shared.Windows_UI_Input.GestureRecognizerTests.TappedTest";

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.Android, Platform.iOS)] // We cannot test right button click on WASM yet
		public void When_Basic()
		{
			Run(_xamlTestPage);

			const string targetName = "Basic_Target";
			const int tapX = 10, tapY = 10;

			// Tap the target
			var target = _app.WaitForElement(targetName).Single().Rect;
			_app.TapCoordinates(target.X + tapX, target.Y + tapY);

			var result = _app.Marked("LastTapped").GetDependencyPropertyValue<string>("Text");
			result.Should().Be(FormattableString.Invariant($"{targetName}@{tapX:F2},{tapY:F2}"));
		}


		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.Android, Platform.iOS)] // We cannot test right button click on WASM yet
		public void When_Transformed()
		{
			Run(_xamlTestPage);

			const string parentName = "Transformed_Parent";
			const string targetName = "Transformed_Target";

			var parent = _app.WaitForElement(parentName).Single().Rect;
			var target = _app.WaitForElement(targetName).Single().Rect;

			// Tap the target
			_app.TapCoordinates(parent.Right - target.Width, parent.Bottom - 3);

			var result = _app.Marked("LastTapped").GetDependencyPropertyValue<string>("Text");
			result.Should().StartWith(targetName);
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.Android, Platform.iOS)] // We cannot test right button click on WASM yet
		public void When_InScroll()
		{
			Run(_xamlTestPage);

			const string targetName = "InScroll_Target";
			const int tapX = 10, tapY = 10;

			// Scroll to make the target visible
			var scroll = _app.WaitForElement("InScroll_ScrollViewer").Single().Rect;
			_app.DragCoordinates(scroll.Right - 3, scroll.Bottom - 3, 0, 0);

			// Tap the target
			var target = _app.WaitForElement(targetName).Single();
			_app.TapCoordinates(target.Rect.X + tapX, target.Rect.Y + tapY);

			var result = _app.Marked("LastTapped").GetDependencyPropertyValue<string>("Text");
			result.Should().Be(FormattableString.Invariant($"{targetName}@{tapX:F2},{tapY:F2}"));
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.Android, Platform.iOS)] // We cannot test right button click on WASM yet
		public void When_InListViewWithItemClick()
		{
			Run(_xamlTestPage);

			const string targetName = "ListViewWithItemClick";

			// Scroll a bit in the ListView
			var target = _app.WaitForElement(targetName).Single().Rect;
			_app.DragCoordinates(target.CenterX, target.Bottom - 3, target.CenterX, target.Y + 3);

			// Tap and hold an item
			_app.TapCoordinates(target.CenterX, target.CenterY);

			var result = _app.Marked("LastTapped").GetDependencyPropertyValue<string>("Text");
			result.Should().StartWith("Item_3");
		}

		[Test]
		[AutoRetry]
		[ActivePlatforms(Platform.Android, Platform.iOS)] // We cannot test right button click on WASM yet
		public void When_InListViewWithoutItemClick()
		{
			Run(_xamlTestPage);

			const string targetName = "ListViewWithoutItemClick";

			// Scroll a bit in the ListView
			var target = _app.WaitForElement(targetName).Single().Rect;
			_app.DragCoordinates(target.CenterX, target.Bottom - 3, target.CenterX, target.Y + 3);

			// Tap and hold an item
			_app.TapCoordinates(target.CenterX, target.CenterY);

			var result = _app.Marked("LastTapped").GetDependencyPropertyValue<string>("Text");
			result.Should().StartWith("Item_3");
		}
	}
}
