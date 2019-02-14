half2 EncodeNormal(half3 n)
{
    half p = sqrt(n.z * 8 + 8);
    return half2(n.xy / p + 0.5);
}
