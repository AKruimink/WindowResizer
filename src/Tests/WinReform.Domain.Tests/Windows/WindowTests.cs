﻿using System;
using WinReform.Domain.WinApi;
using WinReform.Domain.Windows;
using Xunit;

namespace WinReform.Domain.Tests.Windows
{
    /// <summary>
    /// Tests for the <see cref="Window"/>
    /// </summary>
    public class WindowTests
    {
        #region Equals Tests

        [Fact]
        public void Equals_EqualWindow_ShouldReturnTrue()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Rect() };
            var window2 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Rect() };

            // Act

            // Assert
            Assert.True(window1.Equals(window2));
        }

        [Fact]
        public void Equals_UnEqualWindow_ShouldReturnFalse()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Rect() };
            var window2 = new Window { Id = 2, WindowHandle = (IntPtr)2, Description = "Window2", Dimensions = new Rect() };

            // Act

            // Assert
            Assert.False(window1.Equals(window2));
        }

        [Fact]
        public void Equals_InvalidType_ShouldReturnFalse()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window", Dimensions = new Rect() };

            // Act

            // Assert
            Assert.False(window1.Equals("Invalid Type"));
        }

        #endregion Equals Tests

        #region CompareTo Tests

        [Fact]
        public void CompareTo_EqualId_ShouldReturnZero()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Rect() };
            var window2 = new Window { Id = 1, WindowHandle = (IntPtr)2, Description = "Window2", Dimensions = new Rect() };

            // Act

            // Assert
            Assert.True(window1.CompareTo(window2) == 0);
        }

        [Fact]
        public void CompareTo_UnEqualId_ShouldReturnNegativeOne()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Rect() };
            var window2 = new Window { Id = 2, WindowHandle = (IntPtr)2, Description = "Window2", Dimensions = new Rect() };

            // Act

            // Assert
            Assert.True(window1.CompareTo(window2) == -1);
        }

        [Fact]
        public void CompareTo_NullInstances_ShouldReturnNegativeOne()
        {
            // Prepare
            var window1 = new Window { Id = 1, WindowHandle = (IntPtr)1, Description = "Window1", Dimensions = new Rect() };

            // Act

            // Assert
            Assert.True(window1.CompareTo(null) == -1);
        }

        #endregion CompareTo Tests
    }
}
