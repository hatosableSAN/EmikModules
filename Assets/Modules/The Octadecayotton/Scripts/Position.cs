﻿namespace TheOctadecayotton
{
    internal static class Position
    {
        internal static readonly float[,] weights =
        {
            // X = 0, Y = 1, Z = 2
            { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 },
            // W = 3, V = 4, U = 5
            { 0.8f, 0.2f, 0.5f }, { 0.2f, 0.5f, 0.8f }, { 0.5f, 0.8f, 0.2f },
            // R = 6, S = 7, T = 8
            { 2, 0.125f, 0.125f }, { 0.125f, 0.125f, 2 }, { 0.125f, 2, 0.125f },
            // O = 9, P = 10, Q = 11
            { 0.125f, 0.05f, 3.2f }, { 0.05f, 3.2f, 0.125f }, { 3.2f, 0.125f, 0.05f },
            // L = 12, M = 13, N = 14
            { 4, 2, 1 }, { 1, 4, 2 }, { 2, 1, 4 },
            // I = 15, J = 16, K = 17
            { 5, 0, 0 }, { 0, 5, 0 }, { 0, 0, 5 },
            // F = 18, G = 19, H = 20
            { 4, 1, 2.5f }, { 1, 2.5f, 4 }, { 2.5f, 4, 1 },
            // C = 21, D = 22, E = 23
            { 10, 0.6f, 0.6f }, { 0.6f, 0.6f, 10 }, { 0.6f, 10, 0.6f },
            // A = 24, B = 25, XX = 26
            { 0.6f, 0.25f, 16 }, { 0.25f, 16, 0.6f }, { 16, 0.6f, 0.25f },
            // YY = 27, ZZ = 28, WW = 29
            { 20, 10, 5 }, { 5, 20, 10 }, { 10, 5, 20 },
        };
    }
}
