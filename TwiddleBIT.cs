// Provides methods for twiddling multiple bits at once.
public static class BitTwiddle {
    private static int BITSIZE_UINT = (sizeof(uint) * 8);

    // Zeros a total of "length" bits from the start "index."
    public static uint ZeroBits( uint value, int length, int index ) {
        return value & ~((uint.MaxValue >> (BITSIZE_UINT - length)) << index);
    }

    // Sets a total of "length" bits from the start "index," to 1.
    public static uint SetBits( uint value, int length, int index ) {
        return value | ((uint.MaxValue >> (BITSIZE_UINT - length)) << index);
    }

    // Toggles On/Off a total of "length" bits from the start "index."
    public static uint FlipBits( uint value, int length, int index ) {
        return value ^ ((uint.MaxValue >> (BITSIZE_UINT - length)) << index);
    }

    // Fills a total of "length" bits with the new set of "bits" from the start "index."
    public static uint FillBits( uint value, uint bits, int length, int index ) {
        value = ZeroBits( value, length, index );
        return value | ( bits << index );
    }

    // Gets a total of "length" bits from the start "index."
    public static uint GetBits( uint value, int length, int index ) {
        int shift = ( BITSIZE_UINT - length );
        return ( value >> shift ) & ( uint.MaxValue >> shift );
    }
}
