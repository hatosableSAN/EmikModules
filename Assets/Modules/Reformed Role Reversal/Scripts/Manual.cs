using KModkit;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Contains methods that assemble new conditions, tests them, and then store the result in the return.
/// </summary>
namespace ReformedRoleReversalModule
{
    static class Manual
    {
        private static readonly Random rnd = new Random();

        #region First/Second Conditions
        public static Condition FirstA(int[] wires, ref string seed, int lookup, bool discard, KMBombInfo Info, bool firstCondition, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 3, min: 0, max: Arrays.Edgework.Length);
            bool inversion = rnd.NextDouble() > 0.5, leftmost = rnd.NextDouble() > 0.5, appendFromArray = rnd.NextDouble() > 0.5;

            parameters[0] = (parameters[0] / 5) + 2;
            parameters[2] = (wires.Length == 4 || wires.Length == 8) && !firstCondition ? 3 : (parameters[2] % 2) + 3;
            int randomColor = rnd.Next(0, 10);

            Condition condition = new Condition
            {
                Text = firstCondition ? string.Format("この爆弾に{0}{1}つの{2}がある場合、条件{3}へ飛ぶ。", inversion ? "多くとも" : "少なくとも", parameters[0], Arrays.Edgework[parameters[1]], parameters[2])
                                      : appendFromArray || discard ? string.Format("{0}{1}個の{2}がある場合、{5}{4}の{3}。", inversion ? "最大" : "少なくとも", parameters[0], Arrays.Edgework[parameters[1]], discard ? (rnd.NextDouble() > 0.5 ? "ワイヤを取り除く" : "まだ含まれていないワイヤを追加する") : "まだ含まれていないワイヤを追加する", Math.Abs(parameters[2]) - 2 != 1 ? Math.Abs(parameters[2] - 2).ToString() + '本' : string.Empty, leftmost ? "左端" : "右端")
                                                                   : string.Format("{0}{1}個の{2}がある場合、{3}本の{4}のワイヤを追加する。", inversion ? "最大" : "少なくとも", parameters[0], Arrays.Edgework[parameters[1]], Math.Abs(parameters[2] - 2).ToString(), Arrays.Colors[randomColor])
            };

            if (!isCorrectIndex)
                return condition;

            int edgework = new Arrays(Info).GetEdgework(parameters[1]);

            if ((!inversion && parameters[0] <= edgework) || (inversion && parameters[0] >= edgework))
                if (firstCondition)
                    condition.Skip = parameters[2];
                else if (discard)
                    condition.Discard = leftmost ? -(parameters[2] - 2) : parameters[2] - 2;
                else if (appendFromArray)
                    condition.Append = Algorithms.AppendFromArray(wires, ref seed, leftmost, parameters[2] - 2, lookup);
                else
                    condition.Append = Algorithms.ArrayFromInt(randomColor, parameters[2] - 2);

            return condition;
        }

        public static Condition FirstB(int[] wires, ref string seed, int lookup, bool discard, KMBombInfo Info, bool firstCondition, bool isCorrectIndex)
        {
            Arrays arrays = new Arrays(Info);

            int[] parameters = Algorithms.Random(length: 3, min: 0, max: Arrays.Edgework.Length);
            bool more = rnd.NextDouble() > 0.5, leftmost = rnd.NextDouble() > 0.5, appendFromArray = rnd.NextDouble() > 0.5;

            parameters[2] = (wires.Length == 4 || wires.Length == 8) && !firstCondition ? 3 : (parameters[2] % 2) + 3;
            int randomColor = rnd.Next(0, 10);

            Condition condition = new Condition
            {
                Text = firstCondition ? string.Format("{1}が{2}より{0}場合、条件{3}に飛ぶ。", more ? "多い" : "少ない", Arrays.Edgework[parameters[0]], Arrays.Edgework[parameters[1]], parameters[2])
                                      : appendFromArray || discard ? string.Format("{1}が{2}より{0}場合、 {5}{4}の{3}。", more ? "多い" : "少ない", Arrays.Edgework[parameters[0]], Arrays.Edgework[parameters[1]], discard ? (rnd.NextDouble() > 0.5 ? "ワイヤを取り除く" : "まだ含まれていないワイヤを追加する") : "まだ含まれていないワイヤを追加する", Math.Abs(parameters[2]) - 2 != 1 ? Math.Abs(parameters[2] - 2).ToString() + '本' : string.Empty, leftmost ? "左端" : "右端")
                                                                   : string.Format("{2}より{1}が{0}場合、{3}本の{4}のワイヤを追加する。", more ? "多い" : "少ない", Arrays.Edgework[parameters[0]], Arrays.Edgework[parameters[1]], Math.Abs(parameters[2] - 2).ToString(), Arrays.Colors[randomColor])
            };

            if (!isCorrectIndex)
                return condition;

            int edgework1 = arrays.GetEdgework(parameters[0]), edgework2 = arrays.GetEdgework(parameters[1]);

            if ((!more && edgework1 < edgework2) || (more && edgework1 > edgework2))
                if (firstCondition)
                    condition.Skip = parameters[2];
                else if (discard)
                    condition.Discard = leftmost ? -(parameters[2] - 2) : parameters[2] - 2;
                else if (appendFromArray)
                    condition.Append = Algorithms.AppendFromArray(wires, ref seed, leftmost, parameters[2] - 2, lookup);
                else
                    condition.Append = Algorithms.ArrayFromInt(randomColor, parameters[2] - 2);

            return condition;
        }

        public static Condition FirstC(int[] wires, ref string seed, int lookup, bool discard, KMBombInfo Info, bool firstCondition, bool isCorrectIndex)
        {
            Arrays arrays = new Arrays(Info);

            int[] parameters = Algorithms.Random(length: 3, min: 0, max: Arrays.Edgework.Length);
            bool orAnd = rnd.NextDouble() > 0.5, inversion = rnd.NextDouble() > 0.5, leftmost = rnd.NextDouble() > 0.5, appendFromArray = rnd.NextDouble() > 0.5;

            parameters[2] = (wires.Length == 4 || wires.Length == 8) && !firstCondition ? 3 : (parameters[2] % 2) + 3;
            int randomColor = rnd.Next(0, 10);

            Condition condition = new Condition
            {
                Text = firstCondition ? string.Format("{0}{1}{2}が存在{3}場合、条件{4}に飛ぶ。", Arrays.Edgework[parameters[0]], orAnd ? "もしくは" : "と", Arrays.Edgework[parameters[1]], inversion ? "しない" : "する", parameters[2])
                                      : appendFromArray || discard ? string.Format("{0}{1}{2}が存在{3}場合、{6}{5}の{4}。", Arrays.Edgework[parameters[0]], orAnd ? "もしくは" : "と", Arrays.Edgework[parameters[1]], inversion ? "しない" : "する", discard ? (rnd.NextDouble() > 0.5 ? "ワイヤを取り除く" : "まだ含まれていないワイヤを追加する") : "まだ含まれていないワイヤを追加する", Math.Abs(parameters[2]) - 2 != 1 ? Math.Abs(parameters[2] - 2).ToString() + '本' : string.Empty, leftmost ? "左端" : "右端")
                                                                   : string.Format("{0}{1}{2}が存在{3}場合、{4}本の{5}のワイヤを追加する。", Arrays.Edgework[parameters[0]], orAnd ? "もしくは" : "と", Arrays.Edgework[parameters[1]], inversion ? "しない" : "する", Math.Abs(parameters[2] - 2).ToString(), Arrays.Colors[randomColor])
            };

            if (!isCorrectIndex)
                return condition;

            int edgework1 = arrays.GetEdgework(parameters[0]), edgework2 = arrays.GetEdgework(parameters[1]);

            if (inversion && ((orAnd && (edgework1 == 0 || edgework2 == 0)) || (!orAnd && edgework1 == 0 && edgework2 == 0))
            || !inversion && ((orAnd && (edgework1 != 0 || edgework2 != 0)) || (!orAnd && edgework1 != 0 && edgework2 != 0)))
                if (firstCondition)
                    condition.Skip = parameters[2];
                else if (discard)
                    condition.Discard = leftmost ? -(parameters[2] - 2) : parameters[2] - 2;
                else if (appendFromArray)
                    condition.Append = Algorithms.AppendFromArray(wires, ref seed, leftmost, parameters[2] - 2, lookup);
                else
                    condition.Append = Algorithms.ArrayFromInt(randomColor, parameters[2] - 2);

            return condition;
        }

        public static Condition FirstD(int[] wires, ref string seed, int lookup, bool discard, KMBombInfo Info, bool firstCondition, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 3, min: 0, max: Arrays.Edgework.Length);
            bool inversion = rnd.NextDouble() > 0.5, leftmost = rnd.NextDouble() > 0.5, appendFromArray = rnd.NextDouble() > 0.5;

            parameters[0] = (parameters[0] / 7) + 2;
            parameters[2] = (wires.Length == 4 || wires.Length == 8) && !firstCondition ? 3 : (parameters[2] % 2) + 3;
            int randomColor = rnd.Next(0, 10);

            Condition condition = new Condition
            {
                Text = firstCondition ? string.Format("{2}がちょうど{1}つ{0}場合、条件{3}に飛ぶ。", inversion ? "ない" : "ある", parameters[0], Arrays.Edgework[parameters[1]], parameters[2])
                                      : appendFromArray || discard ? string.Format("{2}がちょうど{1}つ{0}場合、{5}{4}の{3}。", inversion ? "ない" : "ある", parameters[0], Arrays.Edgework[parameters[1]], discard ? (rnd.NextDouble() > 0.5 ? "ワイヤを取り除く" : "まだ含まれていないワイヤを追加する") : "まだ含まれていないワイヤを追加する", Math.Abs(parameters[2]) - 2 != 1 ? Math.Abs(parameters[2] - 2).ToString() + '本' : string.Empty, leftmost ? "左端" : "右端")
                                                                   : string.Format("{2}がちょうど{1}つ{0}場合、{4}本の{5}のワイヤを追加する。", inversion ? "ない" : "ある", parameters[0], Arrays.Edgework[parameters[1]], Math.Abs(parameters[2] - 2).ToString(), Arrays.Colors[randomColor])
            };

            if (!isCorrectIndex)
                return condition;

            int edgework = new Arrays(Info).GetEdgework(parameters[1]);

            if ((!inversion && parameters[0] == edgework) || (inversion && parameters[0] != edgework))
                if (firstCondition)
                    condition.Skip = parameters[2];
                else if (discard)
                    condition.Discard = leftmost ? -(parameters[2] - 2) : parameters[2] - 2;
                else if (appendFromArray)
                    condition.Append = Algorithms.AppendFromArray(wires, ref seed, leftmost, parameters[2] - 2, lookup);
                else
                    condition.Append = Algorithms.ArrayFromInt(randomColor, parameters[2] - 2);

            return condition;
        }
        #endregion

        #region Noninitial Conditions
        public static Condition A(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: Arrays.Colors.Length);
            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤが{1}のワイヤの右隣にある場合、最初の{0}のワイヤを切る。", Arrays.Colors[parameters[0]], Arrays.Colors[parameters[1]])
            };

            if (!isCorrectIndex)
                return condition;

            for (int i = 1; i < wires.Length; i++)
                if (wires[i] == parameters[0] && wires[i - 1] == parameters[1])
                {
                    condition.Wire = Algorithms.Find(method: "firstInstanceOfKey", key: ref parameters[0], wires: wires);
                    break;
                }

            return condition;
        }

        public static Condition B(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 4, min: 0, max: Arrays.Colors.Length);
            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤが{1}か{2}か{3}のワイヤの左隣にある場合、最初の{1}か{2}か{3}のワイヤを切る。", Arrays.Colors[parameters[0]], Arrays.Colors[parameters[1]], Arrays.Colors[parameters[2]], Arrays.Colors[parameters[3]])
            };

            if (!isCorrectIndex)
                return condition;

            for (int i = 1; i < wires.Length; i++)
                if (wires[i - 1] == parameters[0] && (wires[i] == parameters[1] || wires[i] == parameters[2] || wires[i] == parameters[3]))
                {
                    condition.Wire = Algorithms.First(new int?[] { Algorithms.Find(method: "firstInstanceOfKey", key: ref parameters[1], wires: wires),
                                                               Algorithms.Find(method: "firstInstanceOfKey", key: ref parameters[2], wires: wires),
                                                               Algorithms.Find(method: "firstInstanceOfKey", key: ref parameters[3], wires: wires) });
                    break;
                }

            return condition;
        }

        public static Condition C(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 3, min: 0, max: wires.Length + 1);
            Condition condition = new Condition
            {
                Text = string.Format("{0}か{1}か{2}のワイヤの色が共通する場合、色が異なる最初のワイヤを切る。", Arrays.Ordinals[parameters[0]], Arrays.Ordinals[parameters[1]], Arrays.Ordinals[parameters[2]])
            };

            if (!isCorrectIndex)
                return condition;

            if (wires[parameters[0]] == wires[parameters[1]] ||
                wires[parameters[1]] == wires[parameters[2]] ||
                wires[parameters[2]] == wires[parameters[0]])
            {
                int matchingWire = wires[parameters[0]] == wires[parameters[1]] ? wires[parameters[0]] : wires[parameters[2]];
                condition.Wire = Algorithms.Find(method: "firstInstanceOfNotKey", key: ref matchingWire, wires: wires);
            }

            return condition;
        }

        public static Condition D(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next((int)Math.Ceiling((float)wires.Length / 2), wires.Length);
            Condition condition = new Condition
            {
                Text = string.Format("色が一致しているワイヤが{0}本ある場合、色が異なる最後のワイヤを切る。", parameter)
            };

            if (!isCorrectIndex)
                return condition;

            int[] colors = Algorithms.GetColors(grouped: false, wires: wires);

            for (int i = 0; i < colors.Length; i++)
                if (colors[i] >= parameter)
                {
                    condition.Wire = Algorithms.Find(method: "lastInstanceOfNotKey", key: ref i, wires: wires);
                    break;
                }

            return condition;
        }

        public static Condition E(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            bool highest = rnd.NextDouble() > 0.5;
            Condition condition = new Condition
            {
                Text = string.Format("すべてのワイヤの色が異なる場合、値が最も{0}ワイヤを切る。", highest ? "高い" : "低い")
            };

            if (!isCorrectIndex)
                return condition;

            if (wires.Distinct().Count() == wires.Count())
            {
                int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);
                int key = highest ? revertedWires.Max() : revertedWires.Min();

                condition.Wire = Algorithms.Find(method: "firstInstanceOfKey", key: ref key, wires: revertedWires);
            }

            return condition;
        }

        public static Condition F(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(1, wires.Length - 1);
            bool seenException = false, lowest = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("色が一致する{0}本1組のワイヤの組を0~1組除き、すべてのワイヤの色が異なる場合、値が最も{1}ワイヤを切る。", Arrays.Tuplets[parameter], lowest ? "低い" : "高い")
            };

            if (!isCorrectIndex)
                return condition;

            foreach (IGrouping<int, int> number in wires.GroupBy(x => x))
            {
                if (number.Count() == 1)
                    continue;

                if (seenException || number.Count() != parameter + 1)
                    return condition;

                seenException = true;
            }

            int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);
            int key = lowest ? revertedWires.Min() : revertedWires.Max();

            condition.Wire = Algorithms.Find(method: "firstInstanceOfKey", key: ref key, wires: revertedWires);

            return condition;
        }

        public static Condition G(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(1, (int)Math.Ceiling((float)wires.Length / 2) + 1);
            int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);

            int exceptions = 0;

            Condition condition = new Condition
            {
                Text = string.Format("{0}本を除き、すべてのワイヤの色が異なる場合、値が中央値となる最後のワイヤを切る。", parameter)
            };

            if (!isCorrectIndex)
                return condition;

            foreach (IGrouping<int, int> number in revertedWires.GroupBy(x => x))
                if (number.Count() == 1)
                    exceptions++;

            if (exceptions != parameter)
                return condition;

            int[] sortedWires = new int[revertedWires.Length];
            Array.Copy(revertedWires, sortedWires, revertedWires.Length);
            Array.Sort(sortedWires);

            int middleWire1 = sortedWires[sortedWires.Length / 2],
                middleWire2 = sortedWires.Length % 2 == 0
                            ? sortedWires[(sortedWires.Length / 2) - 1]
                            : middleWire1;

            condition.Wire = Math.Max((int)Algorithms.Find(method: "lastInstanceOfKey", key: ref middleWire1, wires: revertedWires),
                                      (int)Algorithms.Find(method: "lastInstanceOfKey", key: ref middleWire2, wires: revertedWires));

            if (condition.Wire == 0)
                condition.Wire = null;

            return condition;
        }

        public static Condition H(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: Arrays.Colors.Length);
            bool inversion = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("{1}のワイヤが{0}場合、最初の{1}系のワイヤを切る。", inversion ? "ない" : "ある", Arrays.Colors[parameters[0]], Arrays.GroupedColors[parameters[1]])
            };

            if (!isCorrectIndex)
                return condition;

            string method = parameters[1] < 5 ? "firstInstanceOfBlue" : "firstInstanceOfPurple";

            if (inversion ^ wires.Contains(parameters[0]))
                condition.Wire = Algorithms.Find(method: method, key: ref parameters[0], wires: wires);

            return condition;
        }

        public static Condition I(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 3, min: 0, max: Arrays.GroupedColors.Length);
            parameters[0] = rnd.Next((int)Math.Ceiling((float)wires.Length / 2), wires.Length);

            Condition condition = new Condition
            {
                Text = string.Format("{1}系のワイヤが{0}本以上ある場合、最初の{2}系のワイヤより右にあるワイヤを切る。", parameters[0], Arrays.GroupedColors[parameters[1]], Arrays.GroupedColors[parameters[2]])
            };

            if (!isCorrectIndex)
                return condition;

            string method = parameters[2] < 5 ? "firstInstanceOfBlue" : "firstInstanceOfPurple";

            if (Algorithms.GetColors(grouped: true, wires: wires)[parameters[1] / 5] >= parameters[0])
            {
                condition.Wire = Algorithms.Find(method: method, key: ref parameters[0], wires: wires) + 1;

                if (condition.Wire > wires.Length)
                    condition.Wire = null;
            }

            return condition;
        }

        public static Condition J(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, Arrays.Colors.Length);
            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤが1本だけある場合、そのワイヤを切る。", Arrays.Colors[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            if (Algorithms.GetColors(grouped: false, wires: wires)[parameter] == 1)
                condition.Wire = Algorithms.Find(method: "firstInstanceOfKey", key: ref parameter, wires: wires);

            return condition;
        }

        public static Condition K(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, Arrays.Colors.Length);
            bool ascending = rnd.NextDouble() > 0.5, first = rnd.NextDouble() > 0.5, odd = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("すべての値が{0}順に並んでいる場合、{2}の値を持つ{1}のワイヤを切る。", ascending ? "昇" : "降", first ? "最初" : "最後", odd ? "奇数" : "偶数")
            };

            if (!isCorrectIndex)
                return condition;

            int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);

            for (int i = 1; i < revertedWires.Length; i++)
                if ((ascending && revertedWires[i - 1] > revertedWires[i]) || (!ascending && revertedWires[i - 1] < revertedWires[i]))
                    return condition;

            string method = first ? "first" : "last";
            method += odd ? "Odd" : "Even";

            condition.Wire = Algorithms.Find(method: method, key: ref parameter, wires: revertedWires);

            return condition;
        }

        public static Condition L(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: Arrays.Colors.Length);
            bool first = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("両隣に2本の{1}系のワイヤがある{0}系のワイヤがある場合、そのような集合のうち{2}のワイヤを切る。", Arrays.GroupedColors[parameters[0]], Arrays.GroupedColors[parameters[1]], first ? "最初" : "最後")
            };

            if (!isCorrectIndex)
                return condition;

            for (int i = first ? 1 : wires.Length - 2; first ? i < wires.Length - 1 : i > 0; i += first ? 1 : -1)
                if (wires[i - 1] / 5 == parameters[1] / 5 && wires[i] / 5 == parameters[0] / 5 && wires[i + 1] / 5 == parameters[1] / 5)
                {
                    condition.Wire = ++i;
                    break;
                }

            return condition;
        }

        public static Condition M(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, wires.Length);
            bool highestIf = rnd.NextDouble() > 0.5, highestThen = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤの値が{1}の場合、最初の{2}の値を持つワイヤを切る。", Arrays.Ordinals[parameter], highestIf ? "最高" : "最低", highestThen ? "最高" : "最低")
            };

            if (!isCorrectIndex)
                return condition;

            int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);

            if ((highestIf && revertedWires.Max() == revertedWires[parameter]) ||
               (!highestIf && revertedWires.Min() == revertedWires[parameter]))
                condition.Wire = highestThen ? revertedWires.ToList().IndexOf(revertedWires.Max()) + 1
                                             : revertedWires.ToList().IndexOf(revertedWires.Min()) + 1;

            return condition;
        }

        public static Condition N(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, wires.Length);
            bool first = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤとの差が5である値を持つワイヤがある場合、その説明に合致する{1}のワイヤを切る。", Arrays.Ordinals[parameter], first ? "最初" : "最後")
            };

            if (!isCorrectIndex)
                return condition;

            string method = first ? "firstInstanceOfOppositeKey" : "lastInstanceOfOppositeKey";

            condition.Wire = Algorithms.Find(method: method, key: ref wires[parameter], wires: wires);

            return condition;
        }

        public static Condition O(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: Arrays.Colors.Length);
            parameters[0] = rnd.Next(0, wires.Length);
            bool first = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤが{1}の場合、{2}の{1}のワイヤを切る。", Arrays.Ordinals[parameters[0]], Arrays.Colors[parameters[1]], first ? "最初" : "最後")
            };

            if (!isCorrectIndex)
                return condition;

            string method = first ? "firstInstanceOfKey" : "lastInstanceOfKey";

            if (wires[parameters[0]] == parameters[1])
                condition.Wire = Algorithms.Find(method: method, key: ref parameters[1], wires: wires);

            return condition;
        }

        public static Condition P(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, Arrays.Colors.Length), divisible = rnd.Next(2, 7);

            Condition condition = new Condition
            {
                Text = string.Format("すべてのワイヤの値の和が{0}で割り切れる場合、最後の{1}系のワイヤを切る。", divisible, Arrays.GroupedColors[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);

            string method = parameter < 5 ? "lastInstanceOfBlue" : "lastInstanceOfPurple";

            if (revertedWires.Sum() % divisible == 0)
                condition.Wire = Algorithms.Find(method: method, key: ref parameter, wires: wires);

            return condition;
        }

        public static Condition Q(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: wires.Length);
            bool higher = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤの値が{2}のワイヤよりも{1}場合、あるワイヤと値の差が5になる最初のワイヤを切る。", Arrays.Ordinals[parameters[0]], higher ? "高い" : "低い", Arrays.Ordinals[parameters[1]])
            };

            if (!isCorrectIndex)
                return condition;

            int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);

            if ((higher && revertedWires[parameters[0]] > revertedWires[parameters[1]]) ||
               (!higher && revertedWires[parameters[0]] < revertedWires[parameters[1]]))
            {
                int?[] opposites = new int?[revertedWires.Length];

                for (int i = 0; i < revertedWires.Length; i++)
                    opposites[i] = Algorithms.Find(method: "firstInstanceOfOppositeKey", key: ref revertedWires[i], wires: revertedWires);

                condition.Wire = Algorithms.First(opposites);
            }

            return condition;
        }

        public static Condition R(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            bool more = rnd.NextDouble() > 0.5;
            Condition condition = new Condition
            {
                Text = string.Format("{0} 紫系のワイヤより青系のワイヤの方が{0}場合、あるワイヤと値の差が5になる最後のワイヤを切る。", more ? "多い" : "少ない")
            };

            if (!isCorrectIndex)
                return condition;

            int[] colors = Algorithms.GetColors(grouped: true, wires: wires);

            if ((more && colors[0] > colors[1]) || (!more && colors[0] < colors[1]))
            {
                int?[] results = new int?[wires.Length];

                for (int i = 0; i < wires.Length; i++)
                    results[i] = Algorithms.Find(method: "lastInstanceOfOppositeKey", key: ref wires[i], wires: wires);

                condition.Wire = results.Max();

                if (condition.Wire == 0)
                    condition.Wire = null;
            }

            return condition;
        }

        public static Condition S(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, wires.Length);
            Condition condition = new Condition
            {
                Text = string.Format("「Role」内にあるいずれかの英字がラベルにあるバニラのインジケーターがある場合、{0}のワイヤを切る。", Arrays.Ordinals[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            Indicator[] indicators = new Indicator[] { Indicator.BOB, Indicator.CAR, Indicator.CLR, Indicator.FRK, Indicator.FRQ, Indicator.NLL, Indicator.TRN };

            foreach (Indicator indicator in indicators)
                if (Info.IsIndicatorPresent(indicator))
                {
                    condition.Wire = ++parameter;
                    break;
                }

            return condition;
        }

        public static Condition T(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, wires.Length);
            Condition condition = new Condition
            {
                Text = string.Format("名前に「Role Reversal」が含まれるモジュールが1つのみではない場合、{0}のワイヤを切る。毎度ありがとう！", Arrays.Ordinals[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            if (new Arrays(Info).GetEdgework(19) > 1)
                condition.Wire = ++parameter;

            return condition;
        }

        public static Condition U(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, wires.Length);
            Condition condition = new Condition
            {
                Text = string.Format("シリアルナンバーに「Reformed Role Reversal」内にあるいずれかの英字が含まれる場合、{0}のワイヤを切る。", Arrays.Ordinals[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            const string moduleNameUnique = "REFORMDLVSA";
            string serial = Info.GetSerialNumber();

            foreach (char n in moduleNameUnique)
                if (serial.Contains(n))
                {
                    condition.Wire = ++parameter;
                    break;
                }

            return condition;
        }

        public static Condition V(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: Arrays.IndicatorNames.Length);
            Condition condition = new Condition
            {
                Text = string.Format("{0}インジケーターか{1}インジケーターがある場合、存在するインジケーターの個数と一致するワイヤを切る。", Arrays.IndicatorNames[parameters[0]], Arrays.IndicatorNames[parameters[1]])
            };

            if (!isCorrectIndex)
                return condition;

            if (Info.IsIndicatorPresent(Arrays.Indicators[parameters[0]]) || Info.IsIndicatorPresent(Arrays.Indicators[parameters[1]]))
                condition.Wire = Info.GetIndicators().Count() > wires.Length ? null : (int?)Info.GetIndicators().Count();

            return condition;
        }

        public static Condition W(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 3, min: 0, max: Arrays.Colors.Length);
            parameters[0] = rnd.Next(0, Arrays.Edgework.Length);

            Condition condition = new Condition
            {
                Text = string.Format("{1}のワイヤより{0}の個数が少ない場合、最後の{2}系ではないワイヤを切る。", Arrays.Edgework[parameters[0]], Arrays.Colors[parameters[1]], Arrays.GroupedColors[parameters[1]])
            };

            if (!isCorrectIndex)
                return condition;

            string method = parameters[1] < 5 ? "lastInstanceOfPurple" : "lastInstanceOfBlue";

            if (new Arrays(Info).GetEdgework(parameters[0]) < wires.Where(x => x.Equals(parameters[1])).Count())
                condition.Wire = Algorithms.Find(method: method, key: ref parameters[1], wires: wires);

            return condition;
        }

        public static Condition X(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: Arrays.Edgework.Length);
            parameters[1] = rnd.Next(0, 10);

            Condition condition = new Condition
            {
                Text = string.Format("{1}のワイヤより{0}の個数が少ない場合、最初の{1}ではないワイヤを切る。", Arrays.Edgework[parameters[0]], Arrays.Colors[parameters[1]])
            };

            if (!isCorrectIndex)
                return condition;

            if (new Arrays(Info).GetEdgework(parameters[0]) < wires.Where(x => x.Equals(parameters[1])).Count())
                condition.Wire = Algorithms.Find(method: "firstInstanceOfNotKey", key: ref parameters[1], wires: wires);

            return condition;
        }

        public static Condition Y(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            bool left = rnd.NextDouble() > 0.5, first = rnd.NextDouble() > 0.5;
            int[] parameters = Algorithms.Random(length: 2, min: 0, max: wires.Length);

            Condition condition = new Condition
            {
                Text = string.Format("{0}と{1}のワイヤが連続して並んでいる場合、条件に合致する{3}のペアの{2}側のワイヤを切る。.", Arrays.Colors[wires[parameters[0]]], Arrays.Colors[wires[parameters[1]]], left ? "左" : "右", first ? "最初" : "最後")
            };

            if (!isCorrectIndex)
                return condition;

            for (int i = first ? 0 : wires.Length - 1; first ? i < wires.Length : i >= 0; i += first ? 1 : -1)
                if (wires[parameters[0]] == wires[i])
                {
                    if (first)
                    {
                        if (i - 1 >= 0 && wires[parameters[1]] == wires[i - 1])
                        {
                            condition.Wire = left ? i - 1 : i + 2;
                            break;
                        }

                        if (i + 1 < wires.Length && wires[parameters[1]] == wires[i + 1])
                        {
                            condition.Wire = left ? i : i + 3;
                            break;
                        }
                    }

                    else
                    {
                        if (i + 1 < wires.Length && wires[parameters[1]] == wires[i + 1])
                        {
                            condition.Wire = left ? i : i + 3;
                            break;
                        }

                        if (i - 1 >= 0 && wires[parameters[1]] == wires[i - 1])
                        {
                            condition.Wire = left ? i - 1 : i + 2;
                            break;
                        }
                    }
                }

            if (condition.Wire <= 0 || condition.Wire > wires.Length)
                condition.Wire = null;

            return condition;
        }

        public static Condition Z(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, 3);
            Condition condition = new Condition();

            switch (parameter)
            {
                case 0:
                    condition.Text = string.Format("シリアルナンバーに、0を除くいずれかの色のワイヤの本数と一致する数字がある場合、個数が最多の色のうち最後のワイヤを切る。");
                    break;

                case 1:
                    condition.Text = string.Format("シリアルナンバーに、最多の色のワイヤの本数またはワイヤの総本数と一致する数字がある場合、個数が最少の色のうち最初のワイヤを切る。");
                    break;

                case 2:
                    condition.Text = string.Format("シリアルナンバーに、存在する値のいずれかと一致する数字がある場合、一致する最初の値に対応する位置にあるワイヤを切る。");
                    break;
            }

            if (!isCorrectIndex)
                return condition;

            IEnumerable<int> serial = Info.GetSerialNumberNumbers();

            if (parameter == 0)
            {
                int[] colors = Algorithms.GetColors(grouped: false, wires: wires);

                for (int i = 0; i < colors.Length; i++)
                    if (colors[i] != 0 && serial.Contains(colors[i]))
                    {
                        int[] maxWires = wires.ToLookup(n => n).ToLookup(l => l.Count(), l => l.Key).OrderBy(l => l.Key).Last().ToArray();
                        int?[] results = new int?[maxWires.Length];

                        for (int j = 0; j < results.Length; j++)
                            results[j] = (int)Algorithms.Find(method: "lastInstanceOfKey", key: ref maxWires[j], wires: wires);

                        condition.Wire = results.Max() == 0 ? null : results.Max();
                        break;
                    }
            }

            else if (parameter == 1)
            {
                int[] colors = Algorithms.GetColors(grouped: false, wires: wires);

                if (serial.Contains(colors.Max()) || serial.Contains(wires.Length))
                {
                    int[] maxWires = wires.ToLookup(n => n).ToLookup(l => l.Count(), l => l.Key).OrderBy(l => l.Key).First().ToArray();
                    int?[] results = new int?[maxWires.Length];

                    for (int j = 0; j < results.Length; j++)
                        results[j] = (int)Algorithms.Find(method: "firstInstanceOfKey", key: ref maxWires[j], wires: wires);

                    condition.Wire = Algorithms.First(results);
                }
            }

            else
            {
                int[] revertedColors = Algorithms.RevertLookup(wires, ref lookup);

                for (int i = 0; i < revertedColors.Length; i++)
                    if (serial.Contains(revertedColors[i]))
                    {
                        condition.Wire = revertedColors[i] == 0 || revertedColors[i] > wires.Length ? (int?)null : revertedColors[i];
                        break;
                    }
            }

            return condition;
        }
        #endregion

        #region Last Conditions
        public static Condition LastA(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = rnd.Next(0, wires.Length);
            Condition condition = new Condition
            {
                Text = string.Format("{0}のワイヤを切る。", Arrays.Ordinals[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            condition.Wire = ++parameter;

            return condition;
        }

        public static Condition LastB(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = wires[rnd.Next(0, wires.Length)];
            bool first = rnd.NextDouble() > 0.5;

            Condition condition = new Condition
            {
                Text = string.Format("{0}の{1}のワイヤを切る。", first ? "最初" : "最後", Arrays.Colors[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            condition.Wire = first
                           ? Algorithms.Find(method: "firstInstanceOfKey", key: ref parameter, wires: wires)
                           : Algorithms.Find(method: "lastInstanceOfKey", key: ref parameter, wires: wires);

            return condition;
        }

        public static Condition LastC(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            bool highest = rnd.NextDouble() > 0.5, firstLast = rnd.NextDouble() > 0.5;
            Condition condition = new Condition
            {
                Text = string.Format("値が{1}である{0}のワイヤを切る。", firstLast ? "最初" : "最後", highest ? "最高" : "最低")
            };

            if (!isCorrectIndex)
                return condition;

            int[] revertedWires = Algorithms.RevertLookup(wires, ref lookup);
            int key = highest ? revertedWires.Max() : revertedWires.Min();

            condition.Wire = firstLast
                           ? Algorithms.Find(method: "firstInstanceOfKey", key: ref key, wires: revertedWires)
                           : Algorithms.Find(method: "lastInstanceOfKey", key: ref key, wires: revertedWires);

            return condition;
        }

        public static Condition LastD(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            int parameter = wires[rnd.Next(0, wires.Length)];
            bool first = rnd.NextDouble() > 0.5, blue = parameter < 5;

            Condition condition = new Condition
            {
                Text = string.Format("{0}の{1}系のワイヤを切る。", first ? "最初" : "最後", Arrays.GroupedColors[parameter])
            };

            if (!isCorrectIndex)
                return condition;

            string method = first ? "firstInstanceOf" : "lastInstanceOf";
            method += blue ? "Blue" : "Purple";

            condition.Wire = Algorithms.Find(method: method, key: ref parameter, wires: wires);

            return condition;
        }

        public static Condition ReturnEmptyCondition(int[] wires, int lookup, KMBombInfo Info, bool isCorrectIndex)
        {
            return new Condition()
            {
                Text = wires.Join("") + '\n' + lookup + '\n' + Info + '\n' + isCorrectIndex
            };
        }
        #endregion
    }
}
