#region

using System.Numerics;

#endregion

namespace RayGame;

public static class CollisionDetection
{
    public static bool CheckCollision(Vector2[] shape1, Vector2[] shape2)
    {
        return IsColliding(shape1, shape2) && IsColliding(shape2, shape1);
    }

    private static bool IsColliding(Vector2[] shape1, Vector2[] shape2)
    {
        for (var i = 0; i < shape1.Length; i++)
        {
            // Get the current vertex and the next vertex
            var vertex1 = shape1[i];
            var vertex2 = shape1[(i + 1) % shape1.Length];

            // Calculate the edge vector
            var edge = vertex2 - vertex1;

            // Calculate the perpendicular axis to the edge
            var axis = new Vector2(-edge.Y, edge.X);

            // Normalize the axis
            axis = Vector2.Normalize(axis);

            // Project both shapes onto the axis
            float minA, maxA, minB, maxB;
            ProjectShape(shape1, axis, out minA, out maxA);
            ProjectShape(shape2, axis, out minB, out maxB);

            // Check for overlap
            if (!(maxB >= minA && maxA >= minB))
                // No overlap on this axis, so no collision
                return false;
        }

        // All axes tested, and overlap found on all, so collision
        return true;
    }

    private static void ProjectShape(Vector2[] shape, Vector2 axis, out float min, out float max)
    {
        // Project the first point
        var dotProduct = Vector2.Dot(shape[0], axis);
        min = dotProduct;
        max = dotProduct;

        // Project the remaining points
        for (var i = 1; i < shape.Length; i++)
        {
            dotProduct = Vector2.Dot(shape[i], axis);
            if (dotProduct < min)
                min = dotProduct;
            else if (dotProduct > max) max = dotProduct;
        }
    }
}