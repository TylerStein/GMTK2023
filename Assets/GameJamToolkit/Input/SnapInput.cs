using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SnapInput
{
    [System.Serializable]
    public enum EInputSnap
    {
        NONE = 0,
        DPAD = 1,
        GEN = 2,
    }

    public static Vector2 GetSnapInput(Vector2 input, EInputSnap inputSnapMode)
    {
        float mag = input.magnitude;
        float inputAngle = Vector2.SignedAngle(Vector2.up, input);
        Vector2 adjustedInput;
        switch (inputSnapMode)
        {
            case EInputSnap.NONE: return input;
            case EInputSnap.DPAD:
                inputAngle = Mathf.Ceil(inputAngle / 22.5f) * 22.5f;
                adjustedInput = new Vector2(-Mathf.Sin(inputAngle * Mathf.Deg2Rad), Mathf.Cos(inputAngle * Mathf.Deg2Rad));
                return adjustedInput * mag;
            case EInputSnap.GEN:
                inputAngle = Mathf.Ceil(inputAngle / 15f) * 15f;
                adjustedInput = new Vector2(-Mathf.Sin(inputAngle * Mathf.Deg2Rad), Mathf.Cos(inputAngle * Mathf.Deg2Rad));
                return adjustedInput * mag;
            default: return input;
        }
    }
}
