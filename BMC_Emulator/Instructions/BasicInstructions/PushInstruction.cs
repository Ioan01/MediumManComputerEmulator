using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions;

public class PushInstruction : Instruction
{
    public PushInstruction(GroupCollection matchGroups) : base(matchGroups)
    {
    }

    public PushInstruction(short word) : base(word)
    {
    }

    protected override short GetLast10Bits()
    {
        return (short)(RegisterToBinary(Arguments[0]) << 9);
    }

    public override void Execute(Emulator emulator)
    {
        if (emulator.StackPointer == 0)
            ErrorHandler.HandleBadPush();
        
        emulator.StackPointer -= 1;
        emulator.Memory.Load(emulator.Registers[GetBits(10,10)],emulator.StackPointer);
    }
}