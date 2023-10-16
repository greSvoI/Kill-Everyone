using KillEveryone;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataWeapon))]
public class DataWeaponInspector : Editor
{
	public override void OnInspectorGUI()
	{

		DataWeapon data = (DataWeapon)target;
		
		data.WeaponClass = (EnumClass.WeaponClass)EditorGUILayout.EnumPopup("Selected class", data.WeaponClass);
		serializedObject.Update();

		switch (data.WeaponClass)
		{
			case EnumClass.WeaponClass.Pistol:
				data.tracerShootEffect = (TrailRenderer)EditorGUILayout.ObjectField("Tracer effect", data.tracerShootEffect, typeof(TrailRenderer), false);
				AreCommonParam(data);
				break;
			case EnumClass.WeaponClass.Rifle:
				data.tracerShootEffect = (TrailRenderer)EditorGUILayout.ObjectField("Tracer effect", data.tracerShootEffect, typeof(TrailRenderer), false);
				AreCommonParam(data);
				break;
			case EnumClass.WeaponClass.Sniper:
				data.tracerShootEffect = (TrailRenderer)EditorGUILayout.ObjectField("Tracer effect", data.tracerShootEffect, typeof(TrailRenderer), false);
				AreCommonParam(data);
				break;
			case EnumClass.WeaponClass.Rocket:
				data.tracerRocketEffect = (ParticleSystem)EditorGUILayout.ObjectField("Tracer effect",data.tracerRocketEffect, typeof(ParticleSystem), false);
				data.explosionEffect = (ParticleSystem)EditorGUILayout.ObjectField("Explosion effect",data.explosionEffect, typeof(ParticleSystem), false);
				AreCommonParam(data);
				break;
			case EnumClass.WeaponClass.Melee:
				EditorGUILayout.LabelField("To coming soon");
			break;
		}
		data.WeaponHolder = (EnumClass.WeaponHolder)EditorGUILayout.EnumPopup("Select WeaponHolder", data.WeaponHolder);
		data.Damage = EditorGUILayout.FloatField("Damage", data.Damage);

		EditorGUILayout.LabelField("Draw weapon WeaponHolder animation id " + (int)data.WeaponHolder);

		if (GUI.changed)
		{
			EditorUtility.SetDirty(data);
			AssetDatabase.SaveAssetIfDirty(data);
		}
	}
	private void AreCommonParam(DataWeapon data)
	{
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Audio clips");
		data.Fire = (AudioClip)EditorGUILayout.ObjectField("Shoot clip", data.Fire, typeof(AudioClip), false);
		data.EmptyClip = (AudioClip)EditorGUILayout.ObjectField("Empty clip", data.EmptyClip, typeof(AudioClip), false);
		data.Reload = (AudioClip)EditorGUILayout.ObjectField("Reload clip", data.Reload, typeof(AudioClip), false);
		data.Equip = (AudioClip)EditorGUILayout.ObjectField("Equip clip", data.Equip, typeof(AudioClip), false);
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Weapon params");
		
		data.FireRate = EditorGUILayout.FloatField("Fire Rate", data.FireRate);
		data.BulletMax = EditorGUILayout.IntField("Bullet Max", data.BulletMax);
		data.BulletClip = EditorGUILayout.IntField("BulletClip", data.BulletClip);
		data.BulletCount = EditorGUILayout.IntField("BulletCount", data.BulletCount);
		data.WeaponID = EditorGUILayout.IntField("WeaponID", data.WeaponID);
		data.ReloadID = EditorGUILayout.IntField("ReloadID", data.ReloadID);
	}
}
