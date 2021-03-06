using Microsoft.ApplicationInsights.Channel;
using Serilog.Context;
using Xunit;

namespace Serilog.Sinks.ApplicationInsights.Tests
{
    public class FormattingTests : ApplicationInsightsTest
    {
        [Fact]
        public void Log_level_is_not_in_trace_custom_property()
        {
            Logger.Information("test");

            Assert.False(LastSubmittedTraceTelemetry.Properties.ContainsKey("LogLevel"));
        }

        [Fact]
        public void Message_template_is_not_in_trace_custom_property()
        {
            Logger.Information("test");

            Assert.False(LastSubmittedTraceTelemetry.Properties.ContainsKey("MessageTemplate"));
        }

        [Fact]
        public void Message_properies_include_log_context()
        {
            using (LogContext.PushProperty("custom1", "value1"))
            {
                Logger.Information("test context");

                Assert.True(LastSubmittedTraceTelemetry.Properties.TryGetValue("custom1", out string value1) && value1 == "value1");
            }
        }
    }
}
