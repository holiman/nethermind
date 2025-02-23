// SPDX-FileCopyrightText: 2022 Demerzel Solutions Limited
// SPDX-License-Identifier: LGPL-3.0-only

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Nethermind.Core.Crypto;
using Nethermind.Core.Test.Builders;
using Nethermind.Db.Blooms;
using Nethermind.Trie;

namespace Nethermind.Benchmarks.Store
{
    public class HexPrefixFromBytesBenchmarks
    {
        private byte[] _a;

        private byte[][] _scenarios = new byte[][]
        {
            Keccak.EmptyTreeHash.Bytes,
            Keccak.Zero.Bytes,
            TestItem.AddressA.Bytes,
        };

        [Params(0, 1, 2)]
        public int ScenarioIndex { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _a = _scenarios[ScenarioIndex];
        }

        [Benchmark]
        public byte Improved()
        {
            return HexPrefix.FromBytes(_a).Path[0];
        }

        [Benchmark(Baseline = true)]
        public byte Current()
        {
            return HexPrefix.FromBytes(_a).Path[0];
        }
    }
}
