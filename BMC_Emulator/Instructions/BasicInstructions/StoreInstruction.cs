using System.Text.RegularExpressions;
using BMC_Emulator.Instructions;

namespace BMC_Emulator;

public class StoreInstruction : Instruction
{
    public StoreInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public StoreInstruction(short word) : base(word)
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
        var regToBeStored = emulator.Registers[GetBits(10, 10)];

        var storeAddress = 0;
        
        if (GetBits(9, 9) == 1)
        {
            storeAddress = GetBits(0, 8);
            
        }
        else
        {
            storeAddress = emulator.Registers[GetBits(0, 1)];
        }
        
        emulator.Memory.Load(regToBeStored,storeAddress);

    }
}