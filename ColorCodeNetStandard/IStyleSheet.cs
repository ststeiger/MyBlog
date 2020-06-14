// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace ColorCode
{
    /// <summary>
    /// Defines the contract for a style sheet.
    /// </summary>
    public interface IStyleSheet
    {
        /// <summary>
        /// Gets the dictionary of styles for the style sheet.
        /// </summary>
        StyleDictionary Styles { get; }
    }
}