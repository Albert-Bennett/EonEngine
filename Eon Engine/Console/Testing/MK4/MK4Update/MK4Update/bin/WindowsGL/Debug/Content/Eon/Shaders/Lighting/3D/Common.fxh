half3 DecodeNormal(half2 enc)
{
    half4 nn = half4(enc * 2, 0, 0) + half4(-1, -1, 1, -1);
    half l = dot(nn.xyz, -nn.xyw);
    nn.z = l;
    nn.xy *= sqrt(l);
    return nn.xyz * 2 + half3(0, 0, -1);
}

half2 EncodeNormal(half3 n)
{
    half2 enc = normalize(n.xy) * (sqrt(-n.z * 0.5 + 0.5));
    enc = enc * 0.5 + 0.5;

    return enc;
}
