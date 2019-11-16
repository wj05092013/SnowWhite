using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeImage : MonoBehaviour
{
	public Skill.SkillType Type;

	private SkillManager _skillMgr;
	private SkillActivator _activator;

	private Image _image;
	private int _imageIdx;
	private Sprite[] _sprites;
	private const string _spritePath = "Sprite/UI/icon_skill_sprite_enlarged";


	private void Start()
	{
		Invoke("Init", 0.01f);
	}

	private void Init()
	{
		if(_skillMgr == null)
			_skillMgr = FindObjectOfType<SkillManager>();

		if (!_skillMgr.LoadingCompleted)
		{
			Invoke("Init", 0.01f);
			return;
		}

		_activator = _skillMgr.ActivatorInstances[(int)Type].GetComponent<SkillActivator>();

		_image = GetComponent<Image>();
		_image.color = new Color32(0, 0, 0, 255);

		_sprites = Resources.LoadAll<Sprite>(_spritePath);
	}

	private void Update()
	{
		if(!_activator.Locked)
		{
			CalcImageIndex();

			_image.sprite = _sprites[_imageIdx];
		}		
	}

	private void CalcImageIndex()
	{
		_imageIdx = (int)(14.0f * Mathf.Clamp(_activator.CurrentCoolTime / _activator.MaxCoolTime, 0.0f, 1.0f));
	}
}
