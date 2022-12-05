using System.Text.RegularExpressions;

namespace BMC_Emulator;

public class HaltInstruction : Instruction
{
    public HaltInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public HaltInstruction(short word) : base(word)
    {
    }

    public override void Execute(Emulator emulator)
    {
        emulator.Stop();
    }
}