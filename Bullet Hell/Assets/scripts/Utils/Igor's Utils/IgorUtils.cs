using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Some helpful functions I came up with
/// </summary>
namespace IgorUtils
{
    public static class Vectors
    {

        private static Vector2 middleOfScreenPixels = new Vector2(Screen.width / 2, Screen.height / 2);
        /// <summary>
        /// returns vector 2 that represents pixel position of middle of the screen
        /// </summary>
        public static Vector2 MiddleOfScreenPixels => middleOfScreenPixels;
        /// <summary>
        /// returns random vector2 with length of 1
        /// </summary>
        public static Vector2 GetRandomNormalizedVector(){
            float x = UnityEngine.Random.Range(-0.5f, 0.5f);
            float y = UnityEngine.Random.Range(-0.5f, 0.5f);
            return new Vector2(x, y).normalized;
        }
        /// <summary>
        /// Returns vector that represents mouse position from the middle of camera with length of 1 
        /// </summary>
        public static Vector2 GetMousePosNormalized(){
            Vector2 vect = middleOfScreenPixels - (Vector2)Input.mousePosition;
            return -vect.normalized;
        }
        /// <summary>
        /// Returns vector that represents mouse position from the middle of camera
        /// </summary>
        public static Vector2 GetMousePos(){
            Vector2 vect = middleOfScreenPixels - (Vector2)Input.mousePosition;
            return -vect;
        }
        /// <summary>
        /// rotates vector by a angle
        /// </summary>
        /// <param name="origin">vector to be rotated</param>
        /// <param name="angle">angle in degres</param>
        /// <returns>rotated vector</returns>
        public static Vector2 RotateVector(Vector2 origin, float angle){
            angle=angle/180*math.PI;
            return new Vector2(origin.x*math.cos(angle)-origin.y*math.sin(angle),origin.x*math.sin(angle)+origin.y*math.cos(angle));
        }
        //TODO
        //TODO Add the same function as above but with option to translate vector
        //TODO So when projectile comes from different direction than center it will still work
        //TODO
    }
    public static class Rotation{
        /// <summary>
        /// Rotate sprite corresponding to it's direction
        /// </summary>
        /// <param name="first">direction in which sprite is facing default (usually vector2.right)</param>
        /// <param name="second">direction where sprite will be going</param>
        /// <returns>that rotation</returns>
        public static Quaternion GetRotation(Vector2 first, Vector2 second){
            return Quaternion.Euler(0, 0, second.y < 0 ? -Vector2.Angle(first, second) : Vector2.Angle(first, second));
        }
    }
}