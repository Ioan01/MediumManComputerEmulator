namespace BMC_Emulator;

public class IO
{
    public short Read()
    {
        var str = Console.ReadLine();
        short num;
        if (Int16.TryParse(str, out num))
            return num;
        else return (short)str[0];
    }
    
    
    public void Output(short word)
    {
        // print binary
        Console.Write(word);
    }

    public void OutputBinary(short word)
    {
        Console.Write(Convert.ToString(word,2));
    }
    
    public void OutputText(short word)
    {
        var c = Convert.ToChar(word);
        Console.Write(c);
    }
}