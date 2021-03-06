﻿using EmikBaseModules;
using Netherite;
using System.Collections;
using System.Linq;

public class NetheriteTPScript : TPScript
{
    public NetheriteScript Module;
    public override ModuleScript ModuleScript { get { return Module; } }

#pragma warning disable 414
    new private const string TwitchHelpMessage = @"!{0} ########### | Each # is a digit 1-9, presses in reading order regardless of rules | Example: !{0} 12345678901";
#pragma warning restore 414

    private const float Wait = 0.2f;

    public override IEnumerator ProcessTwitchCommand(string command)
    {
        yield return null;

        int[] inputs = command.ToCharArray().ToNumbers(min: 1, max: 9);
        bool[] stop = { inputs == null, inputs.LengthOrDefault() > 11 - Module.Stage };

        yield return Evaluate(stop[0], SendToChatError("One or more of your characters was not a number."));
        yield return Evaluate(stop[1], SendToChatError("This exceeds the number of stages remaining."));

        if (stop.Any(b => b))
            yield break;

        yield return OnInteractSequence(Module.Buttons, inputs.Select(i => i - 1).ToArray(), Wait, Module.IsStrike);
    }

    public override IEnumerator TwitchHandleForcedSolve()
    {
        int[] answer = Module.Sequence.Select(i => i = Module.ApplyRules(i - 1) - 1).Skip(Module.Stage).ToArray();
        yield return OnInteractSequence(Module.Buttons, answer, Wait);
    }
}
