using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator;

public class OutputInstruction : Instruction
{
    protected override short GetLast10Bits()
    {
        return  (short)((RegisterToBinary(Arguments[0]) << 9) | (ushort)ToShort(Arguments[1]));
    }

    public override void Execute(Emulator emulator)
    {
        
        
        short value = emulator.Registers[GetBits(10,10)];

        switch (GetBits(0,9))
        {
            case 2:
                emulator.Io.Output(value);
                break;
            case 3:
                emulator.Io.OutputBinary(value);
                break;
            case 4:
                emulator.Io.OutputText(value);
                break;
        }
    }


    public OutputInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public OutputInstruction(short word) : base(word)
    {
    }
}