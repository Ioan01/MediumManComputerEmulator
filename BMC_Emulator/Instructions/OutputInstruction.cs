using System.Text.RegularExpressions;

namespace BMC_Emulator;

public class OutputInstruction : Instruction
{
    public OutputInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
        ArgumentsBinary = (short)((ushort)ArgumentsBinary |  (RegisterToBinary(Arguments[0]) << 9) | (ushort)ToShort(Arguments[1]));

    }

    public OutputInstruction(short word) : base(word)
    {
    }

    public override void Execute(Emulator emulator)
    {
        
        
        short value = emulator.Registers[GetBytes(10,10)];

        switch (GetBytes(0,9))
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


    
}