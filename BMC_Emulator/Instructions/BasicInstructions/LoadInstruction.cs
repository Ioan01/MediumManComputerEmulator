using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator;

public class LoadInstruction : Instruction
{
    public LoadInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
        //InstructionBinary |=  
    }

    public LoadInstruction(short word) : base(word)
    {
    }

    protected override short GetLast10Bits()
    {
        throw new NotImplementedException();
    }

    public override void Execute(Emulator emulator)
    {
        throw new NotImplementedException();
    }
}