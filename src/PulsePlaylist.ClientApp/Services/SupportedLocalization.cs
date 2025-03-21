// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PulsePlaylist.ClientApp.Services;

public static class SupportedLocalization
{
    public const string ResourcesPath = "Resources";

    public static readonly LanguageCode[] SupportedLanguages =
    {
        new()
        {
            Code = "en-US",
            DisplayName = "English (United States)"
        },
        new()
        {
            Code = "zh-CN",
            DisplayName = "??(??,??)"
        },
        new()
        {
            Code = "de-DE",
            DisplayName = "Deutsch (Deutschland)"
        },
        new()
        {
            Code = "fr-FR",
            DisplayName = "fran�ais (France)"
        },
        new()
        {
            Code = "ja-JP",
            DisplayName = "??? (??)"
        },
        new()
        {
            Code = "es-ES",
            DisplayName = "espa�ol (Espa�a)"
        },
        new()
        {
            Code = "ko-KR",
            DisplayName = "???(????)"
        },
        new()
        {
            Code = "pt-BR",
            DisplayName = "portugu�s (Brasil)"
        }
    };
}

public class LanguageCode
{
    public string DisplayName { get; set; } = "en-US";
    public string Code { get; set; } = "English";
}
