﻿namespace WinReform.Settings
{
    /// <summary>
    /// Represents a class that provides general application settings data
    /// </summary>
    public interface IApplicationSettingsViewModel
    {
        /// <summary>
        /// Gets or Sets the state that indicates if a dark theme should currently be used
        /// </summary>
        bool UseDarkTheme { get; set; }

        /// <summary>
        /// Gets or Sets the state that defines if the Active Windows should display window location instead of PID
        /// </summary>
        bool DisplayActiveWindowLocation { get; set; }

        /// <summary>
        /// Gets or Sets the state that defines if the window should be minized when closed
        /// </summary>
        bool MinimizeOnClose { get; set; }

        /// <summary>
        /// Gets or Sets the state that defines if the Active Windows should automaticly be refreshed
        /// </summary>
        bool AutoRefreshActiveWindows { get; set; }
    }
}
