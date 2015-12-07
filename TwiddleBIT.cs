public static class TwiddleBIT {
    private static int BITSIZE_UINT = (sizeof(uint) * 8);

    public static uint ZeroBits( uint value, int length, int index ) {
        return value & ~((uint.MaxValue >> (BITSIZE_UINT - length)) << index);
    }

    public static uint SetBits( uint value, int length, int index ) {
        return value | ((uint.MaxValue >> (BITSIZE_UINT - length)) << index);
    }

    public static uint FlipBits( uint value, int length, int index ) {
        return value ^ ((uint.MaxValue >> (BITSIZE_UINT - length)) << index);
    }

    public static uint FillBits( uint value, uint bits, int length, int index ) {
        value = ZeroBits( value, length, index );
        return value | ( bits << index );
    }

    public static uint GetBits( uint value, int length, int index ) {
        int shift = ( BITSIZE_UINT - length );
        return ( value >> shift ) & ( uint.MaxValue >> shift );
    }
}
