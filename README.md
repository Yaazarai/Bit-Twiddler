# Multi-Bit-Toggles
Simple bit-twiddling hacks for toggling multiple bits at once.

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
