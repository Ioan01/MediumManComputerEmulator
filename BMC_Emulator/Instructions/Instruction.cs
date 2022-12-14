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
        new KeyValuePair<string, short>("INP",0x0),
        new KeyValuePair<string, short>("OUT",0x1 << 10),
        new KeyValuePair<string, short>("HLT",0x4 << 10),
        new KeyValuePair<string, short>("LDR",0x2 << 10),
        new KeyValuePair<string, short>("STR",0x3 << 10),
        new KeyValuePair<string, short>("MOV",0x1C << 10),
        new KeyValuePair<string, short>("JMS",0x5 << 10),
        new KeyValuePair<string, short>("PSH",0x6 << 10),
        new KeyValuePair<string, short>("POP",0x7 << 10),
        new KeyValuePair<string, short>("RET",0x8 << 10),
        new KeyValuePair<string, short>("ADD", 0x11 << 10),
        new KeyValuePair<string, short>("SUB", 0x12 << 10),
        new KeyValuePair<string, short>("MUL", 0x13 << 10),
        new KeyValuePair<string, short>("OR", 0x17 << 10),
        new KeyValuePair<string, short>("AND",0x16 << 10),
        new KeyValuePair<string, short>("XOR",0x18 << 10),
        new KeyValuePair<string, short>("NOT",0x19 << 10),
        new KeyValuePair<string, short>("DIV", 0x14 << 10),
        new KeyValuePair<string, short>("MOD", 0x15 << 10),
        new KeyValuePair<string, short>("CMP",0x9 << 10),
        new KeyValuePair<string, short>("BRA",0xA << 10),
        new KeyValuePair<string, short>("BEQ",0xB << 10),
        new KeyValuePair<string, short>("BRZ",0xC << 10),
        new KeyValuePair<string, short>("BMI",0xD << 10),
        new KeyValuePair<string, short>("BPL",0xE << 10),
        new KeyValuePair<string, short>("BGT",0xF << 10),
        new KeyValuePair<string, short>("BLT",0x10 << 10)
    });

    public static Dictionary<short, string> OpcodesReverse { get; } = Opcodes.ToDictionary(d => d.Value, d => d.Key);


    private string Alias { get; } = null!;

    protected string[] Arguments { get; } = null!;

    protected short InstructionBinary { get; set; }

    protected short GetBits(int start, int end)
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
        return  Int16.Parse(register.Substring(1));
    }
    

    public abstract void Execute(Emulator emulator);

    public short ToBinary() => InstructionBinary;



}