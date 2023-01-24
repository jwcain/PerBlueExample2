using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an angle in radians. Assist in managing winding order of bounds as well as crossin the 0 degree threshold within a span.
/// </summary>
public struct AngleSpan
{
    public float lower;
    public float upper;

    public AngleSpan(float lower, float upper)
    {
        this.lower = lower;
        this.upper = upper;
    }

    public static AngleSpan ValidAnglesBetweenPoints(Vector3 origin, Vector3 angledPoint, Vector3[] boundingPoints)
    {
        (float theta, float r) armPolar = CartesianToPolar(angledPoint - origin);
        float lowerBound = float.MinValue;
        float upperBound = float.MaxValue;
        for (int i = 0; i < boundingPoints.Length; i++)
        {
            (float theta, float r) pointPolar = CartesianToPolar(boundingPoints[i] - origin);
            //We project some points around the circle depending on what side they fall on so they can be solved for linearly.
            float angleForMinCheck = pointPolar.theta;
            float angleForMaxCheck = pointPolar.theta;
            //If the points polar is less than the arm, we want to project it around the circle for the Max check
            if (pointPolar.theta < armPolar.theta)
                angleForMaxCheck += Mathf.PI * 2;
            //If the points polar is greater than the arm, we want to project it around the circle for the Min check
            if (pointPolar.theta > armPolar.theta)
                angleForMinCheck -= Mathf.PI * 2;

            if (lowerBound < angleForMinCheck)
                lowerBound = angleForMinCheck;
            if (upperBound > angleForMaxCheck)
                upperBound = angleForMaxCheck;
        }

        while (lowerBound < 0.0f)
            lowerBound += Mathf.PI * 2;
        while (upperBound < 0.0f)
            upperBound += Mathf.PI * 2;
        while (lowerBound > Mathf.PI * 2)
            lowerBound -= Mathf.PI * 2;
        while (upperBound > Mathf.PI * 2)
            upperBound -= Mathf.PI * 2;

        return new AngleSpan(lowerBound, upperBound);
    }


    public static (float theta, float r) CartesianToPolar(Vector3 vector)
    {
        return (Mathf.Atan2(vector.y, vector.x), Mathf.Sqrt((vector.x * vector.x) + (vector.y * vector.y)));
    }

    public static Vector3 PolarToCartesian(float theta, float r)
    {
        return new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), 0f);
    }

    public bool Inside(float angle, out float closestBound)
    {
        while (angle > Mathf.PI * 2)
            angle -= Mathf.PI * 2;
        while (angle < 0f)
            angle += Mathf.PI * 2;

        if (upper < lower)
        {
            //Debug.Log($"Cross 0 span 0:{angle * Mathf.Rad2Deg:00.00}");
            bool first = new AngleSpan(lower, Mathf.PI * 2).Inside(angle, out _);
            bool second = new AngleSpan(0f, upper).Inside(angle, out _);
            //We span across the 0 bound, check this in two parts
            float mid = lower + ((upper - lower) / 2);
            //Debug.Log($"Mid = {mid * Mathf.Rad2Deg:00.00}");
            closestBound = (angle < mid) ? upper : lower;
            //DebugTextManager.Draw("AngleSpan_special", Vector3.zero, $"{upper * Mathf.Rad2Deg:00.00}->360*={first} | 0->{lower * Mathf.Rad2Deg:00.00}={second}");
            return first || second;
        }
        else
        {
            if (angle < lower || angle > upper)
            {
                closestBound = angle < lower ? lower : upper;
                return false;
            }
            else
            {
                closestBound = (angle < (lower + (upper / 2f))) ? lower : upper;
                return true;
            }
        }
    }
}
