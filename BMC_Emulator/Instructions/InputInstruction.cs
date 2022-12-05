using System.Collections;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace BMC_Emulator;

public class InputInstruction : Instruction
{
    public InputInstruction(short word) : base(word)
    {
    }
    public InputInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
        ArgumentsBinary = (short)((ushort)ArgumentsBinary | (RegisterToBinary(Arguments[0]) << 9) | (ushort)ToShort(Arguments[1]));
    }

    public override void Execute(Emulator emulator)
    {
        ref short value = ref emulator.Registers[GetBytes(10,10)];

        value = emulator.Io.Read();

    }




}