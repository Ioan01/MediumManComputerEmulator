using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions.BasicInstructions;

public class HaltInstruction : Instruction
{
    public HaltInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public HaltInstruction(short word) : base(word)
    {
    }

    protected override short GetLast10Bits()
    {
        return 0;
    }

    public override void Execute(Emulator emulator)
    {
        emulator.Stop();
    }
}