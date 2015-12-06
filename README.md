# Multi-Bit Twiddling
Simple bit-twiddling hacks for toggling multiple bits at once.

The magic number `8` here is for 8 bits in a byte. So `sizeof(type) * 8` yields the number of bits in a specific type. The `bitCount` is the number of bits you want to twiddle left (inclusive) from the bit index `bitIndex`.

Clear Multiple Bits:
```
type val = x;
val &= ~((type.MaxValue >> (sizeof(type) * 8 - bitCount) << bitIndex);
```
 
Set Multiple Bits:
```
type val = x;
val |= (type.MaxValue >> (sizeof(type) * 8 - bitCount) << bitIndex;
```
 
Toggle Multiple bits:
```
type val = x;
val ^= (type.MaxValue >> (sizeof(type) * 8 - bitCount) << bitIndex;
```
