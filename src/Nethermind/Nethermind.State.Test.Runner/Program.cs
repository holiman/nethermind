// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using System;
using System.IO;
using CommandLine;
using Ethereum.Test.Base;
using Ethereum.Test.Base.Interfaces;

namespace Nethermind.State.Test.Runner
{
    internal class Program
    {
        public class Options
        {
            [Option('i', "input", Required = true, HelpText = "Set the state test input file or directory.")]
            public string Input { get; set; }

            [Option('t', "trace", Required = false, HelpText = "Set to always trace (by default traces are only generated for failing tests).")]
            public bool TraceAlways { get; set; }

            [Option('n', "neverTrace", Required = false, HelpText = "Set to never trace (by default traces are only generated for failing tests).")]
            public bool TraceNever { get; set; }

            [Option('w', "wait", Required = false, HelpText = "Wait for input after the test run.")]
            public bool Wait { get; set; }

            [Option('m', "memory", Required = false, HelpText = "Exclude memory trace")]
            public bool ExcludeMemory { get; set; }

            [Option('s', "stack", Required = false, HelpText = "Exclude stack trace")]
            public bool ExcludeStack { get; set; }
        }

        public static void Main(params string[] args)
        {
            ParserResult<Options> result = Parser.Default.ParseArguments<Options>(args);
            if (result is Parsed<Options> options)
            {
                Run(options.Value);
            }
        }

        private static void Run(Options options)
        {
            WhenTrace whenTrace = WhenTrace.WhenFailing;
            if (options.TraceNever)
            {
                whenTrace = WhenTrace.Never;
            }

            if (options.TraceAlways)
            {
                whenTrace = WhenTrace.Always;
            }

            if (!string.IsNullOrWhiteSpace(options.Input))
            {
                RunSingleTest(options.Input, source => new StateTestsRunner(source, whenTrace, !options.ExcludeMemory, !options.ExcludeStack));
            }

            if (options.Wait)
            {
                Console.ReadLine();
            }
        }

        private static void RunSingleTest(string path, Func<ITestSourceLoader, IStateTestRunner> testRunnerBuilder)
        {
            ITestSourceLoader source;

            if (Path.HasExtension(path))
            {
                source = new TestsSourceLoader(new LoadGeneralStateTestFileStrategy(), path);
            }
            else
            {
                source = new TestsSourceLoader(new LoadGeneralStateTestsStrategy(), path);
            }

            testRunnerBuilder(source).RunTests();
        }
    }
}
