// Provides methods for twiddling multiple bits at once.
static class BitTwiddler {
    // Number of bits in an unsigned integer.
    public static int BITSOF_UINT { get; private set; }

    // Number of bits in an unsigned byte.
    public static int BITSOF_BYTE { get; private set; }

    static BitTwiddler() {
        BITSOF_UINT = sizeof(uint) * 8;
        BITSOF_BYTE = sizeof(byte) * 8;
    }

    // Gives a value of x, align that value to a given alignment.
    public static int Align(int iterator, int alignment) {
        return ((iterator + (alignment - 1)) & ~(alignment - 1));
    }

    // Zeros a total of "length" bits from the start "index."
    public static uint ZeroBits(uint val, int len, int ind) {
        return val & ~((uint.MaxValue >> (BITSOF_UINT - len)) << ind);
    }

    // Given the starting bit "bitInd" and byte "byteInd" indices, set to 0 "length" bits across multiple bytes in the byte "stream."
    public static void ZeroBits(byte[] stream, int len, int byteInd, int bitInd) {
        int bytes = Align(bitInd + len, BITSOF_BYTE) / BITSOF_BYTE;
        uint value = GetBits(stream, bytes * BITSOF_BYTE, byteInd, 0);
        value = ZeroBits(value, len, bitInd);

        byteInd = byteInd + Align(bitInd, 8) % 8;
        for(int i = (bytes - 1); i > -1; i--) {
            stream[byteInd + i] = (byte)(value >> (i * BITSOF_BYTE));
        }
    }

    // Sets a total of "length" bits from the start "index," to 1.
    public static uint SetBits(uint val, int len, int ind) {
        return val | ((uint.MaxValue >> (BITSOF_UINT - len)) << ind);
    }

    // Given the starting bit "bitInd" and byte "byteInd" indices, set to 1 "length" bits across multiple bytes in the byte "stream."
    public static void SetBits(byte[] stream, int len, int byteInd, int bitInd) {
        int bytes = Align(bitInd + len, BITSOF_BYTE) / BITSOF_BYTE;
        uint value = GetBits(stream, bytes * BITSOF_BYTE, byteInd, 0);
        value = SetBits(value, len, bitInd);

        byteInd = byteInd + Align(bitInd, 8) % 8;
        for(int i = (bytes - 1); i > -1; i--) {
            stream[byteInd + i] = (byte)(value >> (i * BITSOF_BYTE));
        }
    }

    // Toggles On/Off a total of "length" bits from the start "index."
    public static uint FlipBits(uint val, int len, int ind) {
        return val ^ ((uint.MaxValue >> (BITSOF_UINT - len)) << ind);
    }

    // Given the starting bit "bitInd" and byte "byteInd" indices, toggle/flip "length" bits across multiple bytes in the byte "stream."
    public static void FlipBits(byte[] stream, int len, int byteInd, int bitInd) {
        int bytes = Align(bitInd + len, BITSOF_BYTE) / BITSOF_BYTE;
        uint value = GetBits(stream, bytes * BITSOF_BYTE, byteInd, 0);
        value = FlipBits(value, len, bitInd);

        byteInd = byteInd + Align(bitInd, 8) % 8;
        for(int i = (bytes - 1); i > -1; i--) {
            stream[byteInd + i] = (byte)(value >> (i * BITSOF_BYTE));
        }
    }

    // Given the starting bit "bitInd" and byte "byteInd" indices, overwrite "length" bits across multiple bytes in the byte "stream."
    public static uint FillBits(uint val, uint bits, int len, int ind) {
        val = ZeroBits(val, len, ind);
        return val | (bits << ind);
    }

    // Given the starting bit "bitInd" and byte "byteInd" indices, overwrite "length" bits across multiple bytes in the byte "stream."
    public static void FillBits(byte[] stream, uint val, int len, int byteInd, int bitInd) {
        int bytes = Align(bitInd + len, BITSOF_BYTE) / BITSOF_BYTE;
        uint value = GetBits(stream, bytes * BITSOF_BYTE, byteInd, 0);
        value = FillBits(value, val, len, bitInd);

        byteInd = byteInd + Align(bitInd, 8) % 8;
        for(int i = (bytes - 1); i > -1; i--) {
            stream[byteInd + i] = (byte)(value >> (i * BITSOF_BYTE));
        }
    }

    // Gets a total of "length" bits from the start "index."
    public static uint GetBits(uint val, int len, int ind) {
        return (val >> ind) & (uint.MaxValue >> (BITSOF_UINT - len));
    }

    // Given the starting bit "bitInd" and byte "byteInd" indices, get "length" bits from across multiple bytes in the byte "stream."
    public static uint GetBits(byte[] stream, int len, int byteInd, int bitInd) {
        uint val = 0;
        int bytes = Align(bitInd + len, BITSOF_BYTE) / BITSOF_BYTE;

        for(int i = (bytes - 1); i > -1; i--) {
            val |= (uint)stream[byteInd + i] << (i * BITSOF_BYTE);
        }

        val = val >> bitInd;
        val = val & (uint.MaxValue >> (BITSOF_UINT - len));
        return val;
    }
}
