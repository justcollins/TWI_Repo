 #pragma strict
 
 var EffectTime : float;
 function Update(){
     if(EffectTime>0){
         if(EffectTime < 450 && EffectTime > 400){
             GetComponent.<Renderer>().sharedMaterial.SetVector("_ShieldColor", Vector4(0.7, 1, 1, 0));
         }        
         
         EffectTime-=Time.deltaTime * 1000;
         
         GetComponent.<Renderer>().sharedMaterial.SetFloat("_EffectTime", EffectTime);
     }
 }
     
 function OnCollisionEnter(collision : Collision) {
         for (var contact : ContactPoint in collision.contacts) {
         
             GetComponent.<Renderer>().sharedMaterial.SetVector("_ShieldColor", Vector4(0.7, 1, 1, 0.05));
             
             GetComponent.<Renderer>().sharedMaterial.SetVector("_Position", transform.InverseTransformPoint(contact.point));
             
             EffectTime=500;
         }
 }