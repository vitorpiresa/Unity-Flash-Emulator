using System;

public static class HalfUtils
{
	private static uint select(uint a, uint b, bool c)
	{
		return !c ? a : b;
	}

	private static float asfloat(int x)
	{
		return BitConverter.Int32BitsToSingle(x);
	}

	private static float asfloat(uint x)
	{
		return asfloat((int)x);
	}

	private static uint asuint(float x)
	{
		return (uint)BitConverter.SingleToInt32Bits(x);
	}

	public static float f16tof32(uint x)
	{
		uint num = (x & 0x7FFF) << 13;
		uint num2 = num & 0xF800000u;
		uint num3 = num + 939524096 + select(0u, 939524096u, num2 == 260046848);
		return asfloat(select(num3, asuint(asfloat(num3 + 8388608) - 6.1035156E-05f), num2 == 0) | ((x & 0x8000) << 16));
	}
}
