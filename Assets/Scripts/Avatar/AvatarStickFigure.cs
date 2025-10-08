using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineRendererTree
{
    public LineRenderer lineRenderer;
    public List<Transform> transforms;
}

public class AvatarStickFigure : MonoBehaviour
{
    public Transform avatarRoot;
    public LineRendererTree[] lineRendererTrees;

    private void Start()
    {
        //List<LineRendererTree> lineRendererTrees = new List<LineRendererTree>();
        //SetLineRendererTree(lineRendererTrees, avatarRoot, null);
        foreach(var lrt in lineRendererTrees)
        {
            lrt.lineRenderer.positionCount = lrt.transforms.Count;
        }
    }

    private void Update()
    {
        foreach(var lrt in lineRendererTrees)
        {
            for(int i = 0; i < lrt.transforms.Count; i++)
            {
                lrt.lineRenderer.SetPosition(i, lrt.transforms[i].position);
            }
        }
    }

    //private void SetLineRendererTree(List<LineRendererTree> lineRendererTrees, Transform root, LineRendererTree lineRendererTree)
    //{
    //    if(root.childCount > 1)
    //    {
    //        if(lineRendererTree != null)
    //        {
    //            lineRendererTrees.Add(lineRendererTree);
    //        }
    //        foreach(Transform t in root)
    //        {
    //            SetLineRendererTree(lineRendererTrees, t, null);
    //        }
    //    }
    //    else if (root.childCount == 1)
    //    {
    //        if(lineRendererTree == null)
    //        {
    //            GameObject lineRendererObject = new GameObject();
    //            var lineRendererComponent = lineRendererObject.AddComponent<LineRenderer>();
    //            LineRendererTree lrt = new LineRendererTree();
    //            lrt.lineRenderer = lineRendererComponent;
    //            SetLineRendererTree(lineRendererTrees, root, lineRendererTree);
    //        }
    //        else
    //        {
    //            lineRendererTree.transforms.Add(root.GetChild(0));
    //            SetLineRendererTree(lineRendererTrees, root.GetChild(0), lineRendererTree);
    //        }
    //    }
    //}
}