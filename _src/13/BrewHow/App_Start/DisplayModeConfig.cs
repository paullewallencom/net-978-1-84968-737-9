using System;
using System.Web.WebPages;

namespace BrewHow
{
    public class DisplayModeConfig
    {
        public static void RegisterDisplayModes()
        {
            var nexus7DisplayMode = new DefaultDisplayMode("nexus7")
            {
                ContextCondition = (context =>
                    context
                        .GetOverriddenUserAgent()
                        .IndexOf(
                            "Nexus 7",
                            StringComparison
                                .OrdinalIgnoreCase
                        ) >= 0)
            };

            DisplayModeProvider
                .Instance
                .Modes
                .Insert(0, nexus7DisplayMode);
        }
    }
}