using System.Text.RegularExpressions;

namespace BMC_Emulator.Instructions;

public abstract class Instruction
{
    public Instruction(GroupCollection matchGroups)
    {
        Alias = matchGroups[1].Value;
        
        Arguments = new string[matchGroups.Count - 2];
        for (int i = 2; i < matchGroups.Count; i++)
        {
            Arguments[i - 2] = matchGroups[i].Value;
        }

        InstructionBinary = (short)((short)(ushort)Opcodes[Alias] | GetLast10Bits());
    }

    public Instruction(short word)
    {
        InstructionBinary = word;
    }
    
    
    public static readonly Dictionary<string, short> Opcodes = new(new []
    {
        new KeyValuePair<string, short>("INP",0x0000),
        new KeyValuePair<string, short>("OUT",0x0001 << 10),
        new KeyValuePair<string, short>("HLT",0x0004 << 10),
        new KeyValuePair<string, short>("LDR",0x0002 << 10),
        new KeyValuePair<string, short>("STR",0x0003 << 10)
        
    });

    public static Dictionary<short, string> OpcodesReverse { get; } = Opcodes.ToDictionary(d => d.Value, d => d.Key);


    private string Alias { get; } = null!;

    protected string[] Arguments { get; } = null!;

    protected short InstructionBinary { get; set; }

    protected short GetBytes(int start, int end)
    {
        short mask = 0;

        for (int i = start; i <= end; i++)
        {
            mask = (short)((ushort)mask | (1 << i-1));
        }
        
        return (short)((mask & InstructionBinary) >> (start  <= 1 ? (Math.Max(0,start-1)) : start-1 ));
    }

    protected short ToShort(string str)
    {
        return Convert.ToInt16(str);
    }
    
    protected abstract short GetLast10Bits();
    
    protected short RegisterToBinary(string register)
    {
        return  (short)(Int16.Parse(register.Substring(1)));
    }
    

    public abstract void Execute(Emulator emulator);

    public short ToBinary() => InstructionBinary;



}