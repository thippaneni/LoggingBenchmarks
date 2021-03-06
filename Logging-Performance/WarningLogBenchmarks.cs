﻿using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logging_Performance
{
    [MemoryDiagnoser]
    public class WarningLogBenchmarks
    {
        private LoggerLogging _loggerLogging;
        private LoggerMessageLogging _loggerMessageLogging;
        private const string Value1 = "Value";
        private const int Value2 = 100;

        [GlobalSetup]
        public void Setup()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(builder => builder
                .AddFilter("LoggingBenchmarks", LogLevel.Warning)
            );

            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger("TEST");

            _loggerLogging = new LoggerLogging(logger);
            _loggerMessageLogging = new LoggerMessageLogging(logger);
        }

        [Benchmark(Baseline = true)]
        public void LoggerWarningNoParams() => _loggerLogging.LogWarning();

        [Benchmark]
        public void LoggerMessageWarningNoParams() => _loggerMessageLogging.LogWarning();

        [Benchmark]
        public void LoggerWarningWithOneParams() => _loggerLogging.LogWarning(Value1);

        [Benchmark]
        public void LoggerMessageWarningWithOneParams() => _loggerMessageLogging.LogWarning(Value1);

        [Benchmark]
        public void LoggerWarningWithTwoParams() => _loggerLogging.LogWarning(Value1, Value2);

        [Benchmark]
        public void LoggerMessageWarningWithTwoParams() => _loggerMessageLogging.LogWarning(Value1, Value2);
    }
}
