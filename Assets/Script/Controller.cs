using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Controller : MonoBehaviour
{
    public static Controller Instance;
    public List<SpriteRenderer> ctrLs = new List<SpriteRenderer>();
    public static SpriteRenderer itemCtrl;
    public static bool isMove;

    public Action_IDLE player;

    public void OnClickIndex(int index){
        var sprite= ctrLs[index];
        sprite.color=Color.green;
        player.transform.position = sprite.transform.position;
      
       
    }
    
    private void Awake() {
        Instance=this;
        OnClickIndex(PlayerPrefs.GetInt("location"));
    }

//    private void OnApplicationQuit() {
//          PlayerPrefs.SetInt("location", 53);
//     }

    public void OnCLick(SpriteRenderer sprite){

        player.Move(sprite.transform.position);
       

        for (int i=0; i<ctrLs.Count; i++) {
            if(ctrLs[i] == sprite &&isMove==false)
            {
               
                isMove=true;
                ctrLs[i].color=Color.green;
                itemCtrl= ctrLs[i];
                PlayerPrefs.SetInt("location", i);

                
               
            }
            else{
                ctrLs[i].color=Color.white;
            }
            
        }
    }
   
   
}
