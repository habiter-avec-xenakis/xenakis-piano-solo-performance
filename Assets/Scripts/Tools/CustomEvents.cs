using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventColor : UnityEvent<Color>
{
}

[System.Serializable]
public class UnityEventColorTwo : UnityEvent<Color, Color>
{
}

[System.Serializable]
public class UnityEventGradient : UnityEvent<Gradient>
{
}

[System.Serializable]
public class UnityEventGradientTwo : UnityEvent<Gradient, Gradient>
{
}

[System.Serializable]
public class UnityEventVector4 : UnityEvent<Vector4>
{
}

[System.Serializable]
public class UnityEventVector4Two : UnityEvent<Vector4, Vector4>
{
}

[System.Serializable]
public class UnityEventFloat : UnityEvent<float>
{
}

[System.Serializable]
public class UnityEventFloatTwo : UnityEvent<float, float>
{
}

[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{
}