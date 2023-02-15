using UnityEngine;
using Unity.Mathematics;
/// <summary>
/// Add this script to tilemap so it will repeat itself
/// </summary>
public class mapfollow : MonoBehaviour
{
   /// <summary>
   /// represents size of map remember that map
   /// </summary>
   float mapSize;
   private void Start() {
      Vector2 v = GetComponent<BoxCollider2D>().size;
      if(v.x!=v.y){
         Debug.LogError("Map collider is not gucci x!=y");
         Destroy(gameObject);
      }
      mapSize=v.x;
   }
   void OnTriggerExit2D(Collider2D other)
   {
      Vector2 direction = other.transform.position-transform.position;
      direction = new Vector2(math.abs(direction.x)<=math.abs(direction.y)? 0:direction.x,math.abs(direction.y)<=math.abs(direction.x)? 0:direction.y).normalized;
      transform.position+=(Vector3)(direction*mapSize);
   }
}
