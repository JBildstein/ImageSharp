﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

// <auto-generated />

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp.PixelFormats.PixelBlenders
{
    internal static partial class PorterDuffFunctions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Src(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);
            source.W *= opacity;

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 1) + (bw * 0) + (xw * 1);

            // calculate final value
            Vector4 xform = ((source * xw) + (Vector4.Zero * bw) + (source * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Atop(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 0) + (bw * 1) + (xw * 1);

            // calculate final value
            Vector4 xform = ((source * xw) + (backdrop * bw) + (Vector4.Zero * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Over(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);
            source.W *= opacity;

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 1) + (bw * 1) + (xw * 1);

            // calculate final value
            Vector4 xform = ((source * xw) + (backdrop * bw) + (source * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 In(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 0) + (bw * 0) + (xw * 1);

            // calculate final value
            Vector4 xform = ((source * xw) + (Vector4.Zero * bw) + (Vector4.Zero * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Out(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);
            source.W *= opacity;

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 1) + (bw * 0) + (xw * 0);

            // calculate final value
            Vector4 xform = ((Vector4.Zero * xw) + (Vector4.Zero * bw) + (source * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Dest(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 0) + (bw * 1) + (xw * 1);

            // calculate final value
            Vector4 xform = ((backdrop * xw) + (backdrop * bw) + (Vector4.Zero * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 DestAtop(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);
            source.W *= opacity;

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 1) + (bw * 0) + (xw * 1);

            // calculate final value
            Vector4 xform = ((backdrop * xw) + (Vector4.Zero * bw) + (source * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 DestOver(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);
            source.W *= opacity;

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 1) + (bw * 1) + (xw * 1);

            // calculate final value
            Vector4 xform = ((backdrop * xw) + (backdrop * bw) + (source * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 DestIn(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 0) + (bw * 0) + (xw * 1);

            // calculate final value
            Vector4 xform = ((backdrop * xw) + (Vector4.Zero * bw) + (Vector4.Zero * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 DestOut(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 0) + (bw * 1) + (xw * 0);

            // calculate final value
            Vector4 xform = ((Vector4.Zero * xw) + (backdrop * bw) + (Vector4.Zero * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Clear(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 0) + (bw * 0) + (xw * 0);

            // calculate final value
            Vector4 xform = ((Vector4.Zero * xw) + (Vector4.Zero * bw) + (Vector4.Zero * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Xor(Vector4 backdrop, Vector4 source, float opacity)
        {
            opacity = opacity.Clamp(0, 1);
            source.W *= opacity;

            // calculate weights
            float xw = backdrop.W * source.W;
            float bw = backdrop.W - xw;
            float sw = source.W - xw;

            // calculate final alpha
            float fw = (sw * 1) + (bw * 1) + (xw * 0);

            // calculate final value
            Vector4 xform = ((Vector4.Zero * xw) + (backdrop * bw) + (source * sw)) / MathF.Max(fw, Constants.Epsilon);
            xform.W = fw;

            return Vector4.Lerp(backdrop, xform, opacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Normal<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Normal(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Multiply<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Multiply(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Add<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Add(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Subtract<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Subtract(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Screen<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Screen(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Darken<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Darken(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Lighten<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Lighten(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Overlay<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Overlay(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel HardLight<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(HardLight(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Src<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Src(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Atop<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Atop(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Over<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Over(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel In<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(In(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Out<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Out(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Dest<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Dest(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel DestAtop<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(DestAtop(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel DestOver<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(DestOver(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel DestIn<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(DestIn(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel DestOut<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(DestOut(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Clear<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Clear(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPixel Xor<TPixel>(TPixel backdrop, TPixel source, float amount)
            where TPixel : struct, IPixel<TPixel>
        {
            TPixel dest = default;
            dest.PackFromVector4(Xor(backdrop.ToVector4(), source.ToVector4(), amount));
            return dest;
        }

    }
}