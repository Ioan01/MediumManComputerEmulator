using System.Collections;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator;

public class InputInstruction : Instruction
{


    protected override short GetLast10Bits()
    {
        return (short)((RegisterToBinary(Arguments[0]) << 9) | (ushort)ToShort(Arguments[1]));
    }

    public override void Execute(Emulator emulator)
    {
        ref short value = ref emulator.Registers[GetBits(10,10)];

        value = emulator.Io.Read();

    }


    public InputInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public InputInstruction(short word) : base(word)
    {
    }
}