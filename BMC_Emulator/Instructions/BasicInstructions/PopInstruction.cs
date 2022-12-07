using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions;

public class PopInstruction : Instruction
{
    public PopInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public PopInstruction(short word) : base(word)
    {
    }

    protected override short GetLast10Bits()
    {
        return (short)(RegisterToBinary(Arguments[0]) << 9);
    }

    public override void Execute(Emulator emulator)
    {
        if (emulator.StackPointer == Memory.MemorySize)
            ErrorHandler.HandleBadPop();
        
        ref var targetRegister = ref emulator.Registers[GetBits(10, 10)];
        targetRegister = emulator.Memory.Read(emulator.StackPointer);
        emulator.StackPointer += 1;
        
    }
}