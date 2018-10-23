﻿using System.Numerics;
using System.Runtime.CompilerServices;

using BenchmarkDotNet.Attributes;

using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp.Benchmarks.General.PixelConversion
{
    public class PixelConversion_ConvertToVector4
    {
        struct ConversionRunner<T>
            where T : struct, ITestPixel<T>
        {
            private T[] source;

            private Vector4[] dest;

            public ConversionRunner(int count)
            {
                this.source = new T[count];
                this.dest = new Vector4[count];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void RunRetvalConversion()
            {
                int count = this.source.Length;

                ref T sourceBaseRef = ref this.source[0];
                ref Vector4 destBaseRef = ref this.dest[0];

                for (int i = 0; i < count; i++)
                {
                    Unsafe.Add(ref destBaseRef, i) = Unsafe.Add(ref sourceBaseRef, i).ToVector4();
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void RunCopyToConversion()
            {
                int count = this.source.Length;

                ref T sourceBaseRef = ref this.source[0];
                ref Vector4 destBaseRef = ref this.dest[0];

                for (int i = 0; i < count; i++)
                {
                    Unsafe.Add(ref sourceBaseRef, i).CopyToVector4(ref Unsafe.Add(ref destBaseRef, i));
                }
            }
        }

        private ConversionRunner<TestRgba> runner;

        [Params(32)]
        public int Count { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            this.runner = new ConversionRunner<TestRgba>(this.Count);
        }

        [Benchmark(Baseline = true)]
        public void UseRetval()
        {
            this.runner.RunRetvalConversion();
        }

        [Benchmark]
        public void UseCopyTo()
        {
            this.runner.RunCopyToConversion();
        }

        // RESULTS:
        //     Method | Count |     Mean |     Error |    StdDev | Scaled |
        // ---------- |------ |---------:|----------:|----------:|-------:|
        //  UseRetval |    32 | 94.99 ns | 1.1199 ns | 0.9352 ns |   1.00 |
        //  UseCopyTo |    32 | 59.47 ns | 0.6104 ns | 0.5710 ns |   0.63 |
    }
}