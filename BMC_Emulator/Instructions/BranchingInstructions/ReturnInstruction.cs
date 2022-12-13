using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions.BranchingInstructions;

public class ReturnInstruction : Instruction
{
    public ReturnInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public ReturnInstruction(short word) : base(word)
    {
    }

    protected override short GetLast10Bits()
    {
        return 0;
    }

    public override void Execute(Emulator emulator)
    {
        emulator.ProgramCounter = emulator.LinkRegister;
    }
}