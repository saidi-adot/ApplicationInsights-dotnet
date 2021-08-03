﻿namespace IntegrationTests.Tests.TestFramework
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.ApplicationInsights.DataContracts;

    using Xunit.Abstractions;

    public static class TestOutputHelperExtensions
    {
        public static void PrintTelemetryItems(this ITestOutputHelper testOutputHelper, TelemetryBag telemetryItems)
        {
            int i = 1;
            foreach (var item in telemetryItems.SentItems)
            {
                testOutputHelper.WriteLine("Item " + (i++) + ".");

                if (item is RequestTelemetry req)
                {
                    testOutputHelper.WriteLine("RequestTelemetry");
                    testOutputHelper.WriteLine(req.Name);
                    testOutputHelper.WriteLine(req.Duration.ToString());
                }
                else if (item is DependencyTelemetry dep)
                {
                    testOutputHelper.WriteLine("DependencyTelemetry");
                    testOutputHelper.WriteLine(dep.Name);
                }
                else if (item is TraceTelemetry trace)
                {
                    testOutputHelper.WriteLine("TraceTelemetry");
                    testOutputHelper.WriteLine(trace.Message);
                }
                else if (item is ExceptionTelemetry exc)
                {
                    testOutputHelper.WriteLine("ExceptionTelemetry");
                    testOutputHelper.WriteLine(exc.Message);
                }
                else if (item is MetricTelemetry met)
                {
                    testOutputHelper.WriteLine("MetricTelemetry");
                    testOutputHelper.WriteLine(met.Name + "" + met.Sum);
                }

                testOutputHelper.PrintProperties(item as ISupportProperties);
                testOutputHelper.WriteLine("----------------------------");
            }
        }

        public static void PrintProperties(this ITestOutputHelper testOutputHelper, ISupportProperties itemProps)
        {
            foreach (var prop in itemProps.Properties)
            {
                testOutputHelper.WriteLine(prop.Key + ":" + prop.Value);
            }
        }

        public static async Task PrintResponseContentAsync(this ITestOutputHelper testOutputHelper, HttpResponseMessage httpResponseMessage)
        {
            testOutputHelper.WriteLine(await httpResponseMessage.Content.ReadAsStringAsync());
        }
    }
}