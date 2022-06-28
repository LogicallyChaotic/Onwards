using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileInfo))]
public class TileCustomEditor : Editor
{
    SerializedProperty m_unusedSpriteProp;
    SerializedProperty m_highlightedSpriteProp;
    SerializedProperty m_ColourhighlightProp;
    SerializedProperty m_KeySpriteProp;
    SerializedProperty m_swordSpriteProp;
    SerializedProperty m_keySoundProp;
    SerializedProperty m_swordSoundProp;
    SerializedProperty m_particlesystemProp;
    public TileInfo.tileType tileinfo;

    //draw a custom GUI to display all the overall information needed by the tile, and only the relevant ones when selected
    //from the drop down menu of the enum of tile information
    public override void OnInspectorGUI()
    {
        TileInfo tileinfoScript = (TileInfo)target;

        m_unusedSpriteProp = serializedObject.FindProperty("unused");
        m_highlightedSpriteProp = serializedObject.FindProperty("highlighted");
        m_ColourhighlightProp = serializedObject.FindProperty("HighlightColor");

        m_KeySpriteProp = serializedObject.FindProperty("pickUpobject");
        m_swordSpriteProp = serializedObject.FindProperty("pickUpobject");
        m_keySoundProp = serializedObject.FindProperty("pickUpAudioSource");
        m_swordSoundProp = serializedObject.FindProperty("pickUpAudioSource");
        m_particlesystemProp = serializedObject.FindProperty("sparkleGem");

        EditorGUILayout.PropertyField(m_unusedSpriteProp, new GUIContent("Unused Tile Sprite"));
        EditorGUILayout.PropertyField(m_highlightedSpriteProp, new GUIContent("Highlighted Tile Sprite"));
        EditorGUILayout.PropertyField(m_ColourhighlightProp, new GUIContent("Highlighted tile colour"));

        serializedObject.ApplyModifiedProperties();

        tileinfoScript.typeOfTile = (TileInfo.tileType)EditorGUILayout.EnumPopup(tileinfoScript.typeOfTile);

        switch (tileinfoScript.typeOfTile)
        {

            case TileInfo.tileType.STAIRS:
                EditorGUILayout.LabelField("need key to open door?");
                tileinfoScript.NeedKeyToOpen = EditorGUILayout.Toggle(tileinfoScript.NeedKeyToOpen);
                break;

            case TileInfo.tileType.ENEMY:
                EditorGUILayout.LabelField("Direction of attack?");
                tileinfoScript.attackDir = (TileInfo.attackDirection)EditorGUILayout.EnumPopup(tileinfoScript.attackDir);
                EditorGUILayout.LabelField("Number of moves in a cycle");
                tileinfoScript.movesInCycle = EditorGUILayout.IntField(tileinfoScript.movesInCycle);
                EditorGUILayout.LabelField("Move number where enemy attacks");
                tileinfoScript.attackMoveNum = EditorGUILayout.IntField(tileinfoScript.attackMoveNum);
                EditorGUILayout.LabelField("Move number where enemy defends itself");
                tileinfoScript.defendMoveNum = EditorGUILayout.IntField(tileinfoScript.defendMoveNum);
                EditorGUILayout.LabelField("Move number where enemy prepares to attack");
                tileinfoScript.prepareNum = EditorGUILayout.IntField(tileinfoScript.prepareNum);
                break;

            case TileInfo.tileType.PICKUP:
                EditorGUILayout.LabelField("Key pickup?");
                tileinfoScript.isKey = EditorGUILayout.Toggle(tileinfoScript.isKey);
                if (tileinfoScript.isKey)
                {
                    EditorGUILayout.PropertyField(m_KeySpriteProp, new GUIContent("Key Sprite"));
                }
                EditorGUILayout.LabelField("Sword pickup?");
                tileinfoScript.isSword = EditorGUILayout.Toggle(tileinfoScript.isSword);
                if (tileinfoScript.isSword)
                {
                    EditorGUILayout.PropertyField(m_swordSpriteProp, new GUIContent("Sword Sprite"));
                }
                EditorGUILayout.LabelField("Gem Pickup?");
                tileinfoScript.isGem = EditorGUILayout.Toggle(tileinfoScript.isGem);
                if (tileinfoScript.isGem)
                {
                    EditorGUILayout.PropertyField(m_particlesystemProp, new GUIContent("Gem Got Particle Effect"));
                }
                break;

        }
        serializedObject.ApplyModifiedProperties();
    }
}