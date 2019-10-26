using UnityEngine;
 
public class RippleScript : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;
 
    [Range(0,1)]
    public float Friction = .9f;

    public GameObject portal1;
    public GameObject portal2;
    
 
    private float Amount = 0f;
 
    void OnCollisonEntry(Collision c) 
    {
        if (c.collider.name == "portal") {
            this.Amount = this.MaxAmount;
            Vector3 pos = Input.mousePosition;
            this.RippleMaterial.SetFloat("_CenterX", portal1.transform.position.x);
            this.RippleMaterial.SetFloat("_CenterY", portal1.transform.position.y);
        }
            
        

        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }
 
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }
}