#pragma once
#ifndef BITTWIDDLER_H
#define BITTWIDDLER_H

    #define BITSOF_UINT 0x20
    #define BITSOF_BYTE 0x08
    #define MAXOF_UINT 0xFFFFFFFF

    typedef unsigned char byte;
    typedef unsigned int uint;
    
    static class bittwiddler {
        private:
        static inline uint align ( uint itr, uint align ) {
            uint cache = align - 0x1;
            return ( itr + cache ) & ~cache;
        };

        public:
        static inline byte bitcount ( uint value ) {
            byte bits = 0x1;
            for ( ; ( value = value >> 1 ) != 0x0; bits++ );
            return bits;
        };

        static inline uint getbits ( uint value, int length, int bitindex ) {
            return ( value >> bitindex ) & ( MAXOF_UINT >> ( BITSOF_UINT - length ) );
        };

        static uint getbits ( byte* stream, int length, int byteindex, int bitindex ) {
            uint value = 0x0;
            uint bytes = align ( bitindex + length, BITSOF_BYTE );

            for ( int i = ( bytes - 1 ); i > -1; i-- ) {
                value |= stream[ byteindex + i ] << ( i * BITSOF_BYTE );
            };
        };

        static inline uint fillbits ( uint value, uint bits, int length, int bitindex ) {
            value = zerobits ( value, length, bitindex );
            return value | ( bits << bitindex );
        };

        static void fillbits ( byte* stream, uint bits, int length, int byteindex, int bitindex ) {
            int bytes = align ( bitindex + length, BITSOF_BYTE ) / BITSOF_BYTE;
            uint value = getbits ( stream, bytes * BITSOF_BYTE, byteindex, 0 );
            value = fillbits ( value, bits, length, bitindex );

            byteindex = byteindex + align ( bitindex, BITSOF_BYTE ) % BITSOF_BYTE;

            for ( int i = ( bytes - 1 ); i > -1; i-- ) {
                stream[ byteindex + i ] = value >> ( i * BITSOF_BYTE );
            };
        };

        static inline uint flipbits ( uint value, int length, int bitindex ) {
            return value ^ ( ( MAXOF_UINT >> ( BITSOF_UINT - length ) ) << bitindex );
        };

        static void flipbits ( byte* stream, int length , int byteindex, int bitindex ) {
            int bytes = align ( bitindex + length, BITSOF_BYTE ) / BITSOF_BYTE;
            uint value = getbits ( stream, bytes * BITSOF_BYTE, byteindex, 0 );
            value = flipbits ( value, length, bitindex );

            byteindex = byteindex + align ( bitindex, BITSOF_BYTE ) % BITSOF_BYTE;

            for ( int i = ( bytes - 1 ); i > -1; i-- ) {
                stream[ byteindex + i ] = value >> ( i * BITSOF_BYTE );
            };
        };

        static inline uint setbits ( uint value, int length, int bitindex ) {
            return value | ( ( MAXOF_UINT >> ( BITSOF_UINT - length ) ) << length );
        };

        static void setbits ( byte* stream, int length, int byteindex, int bitindex ) {
            int bytes = align ( bitindex + length, BITSOF_BYTE ) % BITSOF_BYTE;
            uint value = getbits ( stream, bytes * BITSOF_BYTE, byteindex, 0 );
            value = setbits ( value, length, bitindex );

            byteindex = byteindex + align ( bitindex, BITSOF_BYTE ) % BITSOF_BYTE;

            for ( int i = ( bytes - 1 ); i > -1; i-- ) {
                stream[ byteindex + i ] = value >> ( i * BITSOF_BYTE );
            };
        };

        static inline uint zerobits ( uint value, int length, int bitindex ) {
            return value & ~( ( MAXOF_UINT >> ( BITSOF_UINT - length ) ) << bitindex );
        };

        static void zerobits ( byte* stream, int length, int byteindex, int bitindex ) {
            int bytes = align ( bitindex + length, BITSOF_BYTE ) / BITSOF_BYTE;
            uint value = getbits ( stream, bytes * BITSOF_BYTE, byteindex, 0 );
            value = zerobits ( value, length, bitindex );

            byteindex = byteindex + align ( bitindex, BITSOF_BYTE ) % BITSOF_BYTE;
            for ( int i = ( bytes - 1 ); i > -1; i-- ) {
                stream[ byteindex + i ] = (byte)( value >> ( i * BITSOF_BYTE ) );
            };
        };
    };

#endif
