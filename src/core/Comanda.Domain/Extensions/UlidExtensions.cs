namespace Comanda.Domain.Extensions;

public static class UlidExtensions
{
    // Base62 alphabet without 0,O,I,l 
    private const string Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz"; 

    public static string ToBase58(this Ulid ulid)
    {
        byte[] input = ulid.ToByteArray();

        if (input.Length == 0)
            return string.Empty;

        int leadingZeros = 0;
        while (leadingZeros < input.Length && input[leadingZeros] == 0)
        {
            leadingZeros++;
        }

        // Allocate enough space in big-endian base58 representation
        var temp = new byte[input.Length * 2];
        int outputStart = temp.Length;

        for (int inputStart = leadingZeros; inputStart < input.Length;)
        {
            temp[--outputStart] = (byte)(Divmod58(input, inputStart));
            if (input[inputStart] == 0)
            {
                inputStart++;
            }
        }

        // Skip leading zeros in base58 result
        while (outputStart < temp.Length && temp[outputStart] == 0)
        {
            outputStart++;
        }

        // Translate to Base58 alphabet and prepend leading '1's
        var encoded = new char[leadingZeros + (temp.Length - outputStart)];
        for (int i = 0; i < leadingZeros; i++)
        {
            encoded[i] = Alphabet[0];
        }

        int j = leadingZeros;
        while (outputStart < temp.Length)
        {
            encoded[j++] = Alphabet[temp[outputStart++]];
        }

        return new string(encoded);
    }

    private static byte Divmod58(byte[] number, int startAt)
    {
        int remainder = 0;

        for (int i = startAt; i < number.Length; i++)
        {
            int digit256 = number[i] & 0xFF;
            int temp = remainder * 256 + digit256;

            number[i] = (byte)(temp / 58);
            remainder = temp % 58;
        }

        return (byte)remainder;
    }
}






