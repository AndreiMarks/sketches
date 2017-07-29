using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

using Object = UnityEngine.Object;

public class OrganHandler : MonoBehaviour 
{
    public OrganInfo[] organs;

    [ContextMenu("Generate")]
    private void Generate()
    {
        string assetPath = "Assets/projects/idleheart/textures/organs/{0}.png";
        List<OrganInfo> organs = new List<OrganInfo>();
        foreach( var thing in Enum.GetValues( typeof( Organs ) ) )
        {
            string name = thing.ToString();
            Sprite picture = AssetDatabase.LoadAssetAtPath<Sprite>( string.Format(assetPath, name) );
            Debug.Log( picture );

            OrganInfo newOrgan = new OrganInfo( name, picture );
            organs.Add( newOrgan );
        }

        this.organs = organs.ToArray();
    }
}

[Serializable]
public class OrganInfo
{
    public string name;
    public string displayName;
    public int price;
    public Sprite image;

    public string Name
    {
        get
        {
            return string.IsNullOrEmpty( displayName ) ? name : displayName ;
        }
    }

    public OrganInfo( string name, Sprite image )
    {
        this.name = name;
        this.image = image;
    }
}

public enum Organs
{
    Bladder,
    BloodVessels,
    Bones,
    Esophagus,
    GallBladder,
    Brain,
    Ear,
    Eye,
    Heart,
    Liver,
    Lungs,
    Skin,
    Spleen,
    Teeth,
    Mouth,
    ImmuneSystem,
    LargeIntestine,
    LymphNodes,
    Muscle,
    Touch,
    Nose,
    Pancreas,
    Trachea,
    SmallIntestine,
    Stomach,
    Thyroid,
    Tongue,
    Kidneys
}
