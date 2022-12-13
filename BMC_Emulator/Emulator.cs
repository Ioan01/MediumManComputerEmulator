namespace BMC_Emulator;

public class Emulator
{
    public Memory Memory { get; } = new();

    public IO Io { get; } = new();

    public short[] Registers { get; set; } = new short[2];


    public int ProgramCounter { get; set; }

    public int StackPointer { get; set; } = Memory.MemorySize;

    public int LinkRegister { get; set; } = 0;

    public bool Zero { get; set; } // Zero flag in the 4-bit Flag register

    public bool Negative { get; set; } // Negative flag in the 4-bit Flag register

    public bool Carry { get; set; } // Carry flag in the 4-bit Flag register

    public bool Overflow { get; set; } // Overflow flag in the 4-bit Flag register

    private bool stopped;

    private void ReplaceLabels(string[] instructions)
    {
        int index = 0;

        Dictionary<string, int> labelAddress = new();

        // first go through definitions     
        // example: "#OUTPUT OUT R0,2"
        foreach (var instruction in instructions)
        {
            if (instruction.StartsWith('#'))
            {
                labelAddress.Add(instruction.Substring(0,
                    instruction.IndexOf(' ')),index);
                instructions[index] = instruction.Substring(instruction.IndexOf(' ') + 1);
            }
            index++;
        }
        
        // second go through usages     
        // example: "JMS #OUTPUT"
        index = 0;
        foreach (var instruction in instructions)
        {
            if (instructions[index].Contains('#'))
            {
                var label = instructions[index].Substring(instructions[index].IndexOf('#'));
                instructions[index] = instructions[index].Replace(label,
                    labelAddress[label].ToString());
            }
            index++;
        }
    }

    public void LoadProgram(string[] instructions)
    {
        ReplaceLabels(instructions);
        for (int i = 0; i < instructions.Length; i++)
        {
            instructions[i] = instructions[i].ToUpper();
            System.Console.WriteLine("ins: {0}", instructions[i]);
            var instruction = InstructionDecoder.DecodeInstruction(instructions[i]);
            Memory.Load(instruction.ToBinary(),i);
        }
    }


    public void Run()
    {
        while (!stopped)
        {
            var instruction = Memory.Read(ProgramCounter);
            ProgramCounter++;
            var _instruction = InstructionDecoder.DecodeInstructionBinary(instruction);
            _instruction.Execute(this);
            
        }
    }

    public void Stop()
    {
        stopped = true;
    }
}