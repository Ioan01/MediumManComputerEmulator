using System.Collections;

namespace BMC_Emulator;

public class Memory
{
    private readonly short[] words = new short[512];

    public Memory()
    {
        for (int i = 0; i < 512; i++)
        {
            words[i] = 0;
        }
    }

    public void Load(short word, int index)
    {
        words[index] = word;
    }
    
    public short Read(int index)
    {
        return words[index];
    }
}