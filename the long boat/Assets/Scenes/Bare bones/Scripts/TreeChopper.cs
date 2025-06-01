using UnityEngine;

public class TreeChopper : MonoBehaviour
{
    public float range = 3f;
    public LayerMask treeLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, range, treeLayer))
            {
                ChoppableTree tree = hit.collider.GetComponentInParent<ChoppableTree>();
                if (tree != null)
                {
                    tree.Chop();
                }
            }
        }
    }
}
