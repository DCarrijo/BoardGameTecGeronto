using System;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

[ExecuteAlways()]
public class MaterialChooser : MonoBehaviour
{
    public Material[] _materials = null;
    public Material[] _materials2 = null;
    public MeshRenderer _mesh;

    [Button]
    public void RandomizeMaterial()
    { 
        var aux = Random.Range(0, 3);
        var list = new List<Material>();
        list.Add(_materials2[aux]);
        list.Add(_materials[aux]);
        _mesh.sharedMaterials =  list.ToArray();
    }

    [Button]
    public void ChooseMaterial(int material)
    {
        if (material >= 3 || material < 0)
        {
            RandomizeMaterial();
        }
        else
        {
            var list = new List<Material>();
            list.Add(_materials2[material]);
            list.Add(_materials[material]);
            _mesh.sharedMaterials = list.ToArray();
        }
    }
}
