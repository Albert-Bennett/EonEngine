half3 DecodeNormal(half2 enc)
{
    half2 fenc = enc * 4 - 2;
    half f = dot(fenc, fenc);
    half g = sqrt(1 - f / 4);

    half3 n;
    n.xy = fenc * g;
    n.z = 1 - f / 2;
    return n;
}
