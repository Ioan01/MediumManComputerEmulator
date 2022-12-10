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
        if (Arguments[1].Contains('R'))
            return  (short)((RegisterToBinary(Arguments[0]) << 9)  | (ushort)RegisterToBinary(Arguments[1]));
        
        return  (short)((RegisterToBinary(Arguments[0]) << 9) | 1 << 8 | (ushort)ToShort(Arguments[1]));
    }

    public override void Execute(Emulator emulator)
    {
        ref var destinationRegister = ref emulator.Registers[GetBits(10, 10)];

        var address = 0;
        
        if (GetBits(9, 9) == 1)
        {
            address = GetBits(0, 8);
            
        }
        else
        {
            address = emulator.Registers[GetBits(0, 1)];
        }
        
        destinationRegister = emulator.Memory.Read(address);
    }
}