// Provides methods for twiddling multiple bits at once.
public static class BitTwiddle {
    private static int BITSIZE_UINT = sizeof(uint) * 8;
    private static int BITSOF_BYTE = sizeof(byte) * 8;

    // Zeros a total of "length" bits from the start "index."
    public static uint ZeroBits( uint value, int length, int index ) {
        return value & ~( ( uint.MaxValue >> ( BITSIZE_UINT - length ) ) << index );
    }

    // Sets a total of "length" bits from the start "index," to 1.
    public static uint SetBits( uint value, int length, int index ) {
        return value | ( ( uint.MaxValue >> ( BITSIZE_UINT - length ) ) << index );
    }

    // Toggles On/Off a total of "length" bits from the start "index."
    public static uint FlipBits( uint value, int length, int index ) {
        return value ^ ( ( uint.MaxValue >> ( BITSIZE_UINT - length ) ) << index );
    }

    // Fills a total of "length" bits with the new set of "bits" from the start "index."
    public static uint FillBits( uint value, uint bits, int length, int index ) {
        value = ZeroBits( value, length, index );
        return value | ( bits << index );
    }
    
    // Given the starting bit "bitInd" and byte "byteInd" indices, overwrite "length" bits across multiple bytes in the byte "stream."
    public static void FillBits( byte[] stream, uint val, int len, int byteInd, int bitInd ) {
        int bytes = Align( bitInd + len, BITSOF_BYTE ) / BITSOF_BYTE;
        uint value = GetBits( stream, bytes * BITSOF_BYTE, byteInd, 0 );
        value = FillBits( value, val, len, bitInd );
            
        byteInd = byteInd + Align( bitInd, 8 ) % 8;
        for( int i = (bytes - 1); i > -1; i -- ) {
            stream[ byteInd + i ] = (byte) ( value >> ( i * BITSOF_BYTE ) );
        }
    }

    // Gets a total of "length" bits from the start "index."
    public static uint GetBits( uint value, int length, int index ) {
        return ( value >> ind ) & ( uint.MaxValue >> ( BITSIZE_UINT - length ) );
    }
    
    // Given the starting bit "bitInd" and byte "byteInd" indices, get "length" bits from across multiple bytes in the byte "stream."
    public static uint GetBits( byte[] stream, int len, int byteInd, int bitInd ) {
        uint val = 0;
        int bytes = Align( bitInd + len, BITSOF_BYTE ) / BITSOF_BYTE;

        for( int i = (bytes - 1); i > -1; i -- ) {
            val |= (uint)stream[ byteInd + i ] << ( i * BITSOF_BYTE );
        }
        
        val = val >> bitInd;
        val = val & ( uint.MaxValue >> ( BITSOF_UINT - len ) );
        return val;
    }
}
