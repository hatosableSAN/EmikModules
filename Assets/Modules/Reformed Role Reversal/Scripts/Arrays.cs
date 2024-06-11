using KModkit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathManager = KeepCoding.PathManager;

/// <summary>
/// Contains mostly static unchanging information and indexable edgework.
/// </summary>
namespace ReformedRoleReversalModule
{
    internal class Arrays
    {
        internal Arrays(KMBombInfo Info)
        {
            info = Info;
        }

        private readonly KMBombInfo info;

        /// <summary>
        /// The version of the module.
        /// </summary>
        internal static string Version { get { return version; } }

        // When updating, change this string!
        private static readonly string version = PathManager.GetModInfo("EmikModules").Version;

        /// <summary>
        /// Indexable array of indicator edgework in alphabetical order.
        /// </summary>
        internal static Indicator[] Indicators { get { return indicators; } }

        private static readonly Indicator[] indicators = new Indicator[11]
        {
            Indicator.BOB,
            Indicator.CAR,
            Indicator.CLR,
            Indicator.FRK,
            Indicator.FRQ,
            Indicator.IND,
            Indicator.MSA,
            Indicator.NSA,
            Indicator.SIG,
            Indicator.SND,
            Indicator.TRN
        };

        /// <summary>
        /// Indexable array of indicator string names in alphabetical order.
        /// </summary>
        internal static string[] IndicatorNames { get { return indicatorNames; } }

        private static readonly string[] indicatorNames = new string[11]
        {
            "BOB",
            "CAR",
            "CLR",
            "FRK",
            "FRQ",
            "IND",
            "MSA",
            "NSA",
            "SIG",
            "SND",
            "TRN",
        };

        /// <summary>
        /// 62 length array consisting of 0-9, A-Z, a-z
        /// </summary>
        internal static char[] Base62 { get { return base62; } }

        private static readonly char[] base62 = new char[62]
        {
            '0','1','2','3','4','5','6','7','8','9',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
        };

        /// <summary>
        /// Indexable array of edgework string names.
        /// </summary>
        internal static string[] Edgework { get { return edgework; } }

        private static readonly string[] edgework = new string[20]
        {
            "バッテリー",
            "単三バッテリー",
            "単一バッテリー",
            "バッテリーホルダー",
            "インジケーター",
            "点灯インジケーター",
            "消灯インジケーター",
            "「Role」のいずれかの文字を含むインジケーター",
            "ポートプレート",
            "unique ポート",
            "重複するポート",
            "ポート",
            "シリアルナンバーの数字",
            "シリアルナンバーの文字",
            "シリアルナンバーの母音",
            "シリアルナンバーの子音",
            "合計モジュール数 (特殊含まない)",
            "合計モジュール数 (特殊含む)",
            "特殊モジュール",
            "「Role Reversal」のいずれかの文字を含む英名のモジュール"
        };

        /// <summary>
        /// Indexable array of all colors used.
        /// </summary>
        internal static string[] Colors { get { return colors; } }

        private static readonly string[] colors = new string[10]
        {
            "紺色",
            "瑠璃色",
            "青色",
            "空色",
            "青緑色",
            "梅色",
            "菫色",
            "紫色",
            "マゼンタ",
            "藤色"
        };

        /// <summary>
        /// Indexable array of all colors in an estimated format.
        /// </summary>
        internal static string[] GroupedColors { get { return groupedColors; } }

        private static readonly string[] groupedColors = new string[10]
        {
            "青",
            "青",
            "青",
            "青",
            "青",
            "紫",
            "紫",
            "紫",
            "紫",
            "紫"
        };

        /// <summary>
        /// Convert index to equivalent ordinal.
        /// </summary>
        internal static string[] Ordinals { get { return ordinals; } }

        private static readonly string[] ordinals = new string[9]
        {
            "一番目",
            "二番目",
            "三番目",
            "四番目",
            "五番目",
            "六番目",
            "七番目",
            "八番目",
            "九番目",
        };

        /// <summary>
        /// Convert index to equivalent tuplet.
        /// </summary>
        internal static string[] Tuplets { get { return tuplets; } }

        private static readonly string[] tuplets = new string[9]
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"
        };

        /// <summary>
        /// Generates the tutorial based on a few parameters that dictate the rules.
        /// </summary>
        /// <param name="buttonOrder">The indexes used for Interact in reading order.</param>
        /// <param name="baseN">The initial base shown on the module.</param>
        /// <param name="left">Append 0's on the left if true, otherwise right.</param>
        /// <param name="mod">The number to modulo the seed with.</param>
        /// <param name="add">The number to add after modulo.</param>
        /// <param name="leftmost">Take leftmost digits if true, otherwise right.</param>
        /// <param name="offset">The offset to apply for the table.</param>
        /// <param name="discard">Whether or not to discard wires.</param>
        /// <param name="append">Which direction to append wires.</param>
        /// <returns>The formatted condition array for the tutorial.</returns>
        internal Condition[] GetTutorial(List<int> buttonOrder, int baseN, ref bool left, ref int mod, ref int add, ref bool leftmost, ref int offset, ref bool discard, ref bool append)
        {
            string[] buttonText = { "左", "下", "上", "右" };

            return new Condition[]
            {
            new Condition { Text = "モジュール詳細：Reformed Role Reversal\n\n" + (Random.Range(0, 1f) > 0.5 ? buttonText[buttonOrder.IndexOf(2)] + "と" + buttonText[buttonOrder.IndexOf(1)] : buttonText[buttonOrder.IndexOf(1)] + "と " + buttonText[buttonOrder.IndexOf(2)]) + "矢印ボタンを使用して画面内を移動する。" },
            new Condition { Text = "画面の周囲を見回し、シード値を特定する。これを" + baseN + "進数から10進数に変換する。その後、9桁になるまで" + (left ? "左" : "右") + "に0を追加する。" },
            new Condition { Text = "シード値を" + mod + "で割った余りを求め、" + add + "を足す。これをXとする。9桁のシード値から" + (leftmost ? "左端" : "右端") + "X個分の数字を取得する。参照表#" + offset + "を使って各数字を色に変換し、ワイヤの集まりを得る。" },
            new Condition { Text = "下画面にあるワイヤの総本数の条件セットに向かい、表示された条件が偽の場合は" + buttonText[buttonOrder.IndexOf(2)] + "を押す。" },
            new Condition { Text = "ワイヤを「" + (discard ? "取り除く" : "追加する") + "」ように指示された場合、" + (discard ? string.Empty : "ワイヤを" + (append ? "左端" : "右端") + "に追加し、") + "新しいワイヤの総本数の条件セットを参照する。" },
            new Condition { Text = discard ? "「追加する」指示は偽物であり壊れている。「まだ含まれていないワイヤを追加する」指示は「取り除く」指示に置き換えたうえで、その条件を確認すること。" : "「まだ含まれていないワイヤ」とは、10進数のシード値のうち、初期セットに含まれなかったかつ現存するワイヤと色が被らないワイヤのことである。" },
            new Condition { Text = "合致する最初の条件を見つけたら、" + buttonText[buttonOrder.IndexOf(0)] + "または" + buttonText[buttonOrder.IndexOf(3)] + "ボタンを押して、送信モードに入る。" },
            new Condition { Text = "注意：存在しないワイヤを切ったり追加するように指示された場合、その条件は無視すること。頑張って！(" + version + ")" }
            };
        }

        /// <summary>
        /// Gets the edgework from the same index as the strings variable.
        /// </summary>
        /// <param name="i">The index for the numbers array.</param>
        /// <returns>A number representing the edgework.</returns>
        internal int GetEdgework(int i)
        {
            return new int[20]
            {
            info.GetBatteryCount(),
            info.GetBatteryCount(Battery.AA) + info.GetBatteryCount(Battery.AAx3) + info.GetBatteryCount(Battery.AAx4),
            info.GetBatteryCount(Battery.D),
            info.GetBatteryHolderCount(),
            info.GetIndicators().Count(),
            info.GetOnIndicators().Count(),
            info.GetOffIndicators().Count(),
            info.GetIndicators().Count(x => x.ToLowerInvariant().Any(y => new[] { 'r', 'o', 'l', 'e' }.Contains(y))),
            info.GetPortPlateCount(),
            info.GetPorts().Distinct().Count(),
            info.GetPorts().Count() - info.GetPorts().Distinct().Count(),
            info.GetPortCount(),
            info.GetSerialNumberNumbers().Count(),
            info.GetSerialNumberLetters().Count(),
            info.GetSerialNumberLetters().Count(x => new[] { 'a', 'e', 'i', 'o', 'u' }.Contains(x.ToString().ToLowerInvariant().ToCharArray()[0])),
            info.GetSerialNumberLetters().Count(x => !new[] { 'a', 'e', 'i', 'o', 'u', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }.Contains(x.ToString().ToLowerInvariant().ToCharArray()[0])),
            info.GetSolvableModuleNames().Count(),
            info.GetModuleNames().Count(),
            info.GetModuleNames().Count() - info.GetSolvableModuleNames().Count(),
            info.GetModuleNames().Count(s => s == "Role Reversal" || s == "Reformed Role Reversal")
            }[i];
        }
    }
}
